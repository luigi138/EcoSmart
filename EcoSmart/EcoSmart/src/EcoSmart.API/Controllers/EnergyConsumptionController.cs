using EcoSmart.Core.DTOs;
using EcoSmart.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcoSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnergyConsumptionController : ControllerBase
    {
        private readonly IEnergyConsumptionService _consumptionService;
        private readonly ILogger<EnergyConsumptionController> _logger;

        public EnergyConsumptionController(
            IEnergyConsumptionService consumptionService,
            ILogger<EnergyConsumptionController> logger)
        {
            _consumptionService = consumptionService;
            _logger = logger;
        }

        /// <summary>
        /// Registra um novo consumo de energia
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(EnergyConsumptionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EnergyConsumptionDto>> RecordConsumption(
            [FromBody] RecordConsumptionRequest request)
        {
            try
            {
                var consumption = await _consumptionService.RecordConsumptionAsync(request);
                return Created(string.Empty, consumption);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar consumo de energia");
                return StatusCode(500, "Erro interno ao processar a requisição.");
            }
        }

        /// <summary>
        /// Obtém o histórico de consumo de um dispositivo
        /// </summary>
        [HttpGet("device/{deviceId}")]
        [ProducesResponseType(typeof(IEnumerable<EnergyConsumptionDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnergyConsumptionDto>>> GetDeviceConsumption(
            string deviceId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var history = await _consumptionService.GetDeviceConsumptionHistoryAsync(
                    deviceId, startDate, endDate);
                return Ok(history);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar histórico de consumo: {DeviceId}", deviceId);
                return StatusCode(500, "Erro interno ao processar a requisição.");
            }
        }
    }
}