using EcoSmart.Core.DTOs;
using EcoSmart.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcoSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly ILogger<DevicesController> _logger;

        public DevicesController(
            IDeviceService deviceService,
            ILogger<DevicesController> logger)
        {
            _deviceService = deviceService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém um dispositivo pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeviceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeviceDto>> GetById(string id)
        {
            try
            {
                var device = await _deviceService.GetByIdAsync(id);
                if (device == null)
                    return NotFound($"Dispositivo com ID {id} não encontrado.");

                return Ok(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar dispositivo: {Id}", id);
                return StatusCode(500, "Erro interno ao processar a requisição.");
            }
        }

        /// <summary>
        /// Obtém todos os dispositivos de um usuário
        /// </summary>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<DeviceDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetUserDevices(Guid userId)
        {
            try
            {
                var devices = await _deviceService.GetUserDevicesAsync(userId);
                return Ok(devices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar dispositivos do usuário: {UserId}", userId);
                return StatusCode(500, "Erro interno ao processar a requisição.");
            }
        }

        /// <summary>
        /// Registra um novo dispositivo
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(DeviceDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeviceDto>> Create([FromBody] CreateDeviceRequest request)
        {
            try
            {
                var device = await _deviceService.CreateAsync(request);
                return CreatedAtAction(
                    nameof(GetById), 
                    new { id = device.Id }, 
                    device);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar dispositivo");
                return StatusCode(500, "Erro interno ao processar a requisição.");
            }
        }

        /// <summary>
        /// Atualiza o status de um dispositivo
        /// </summary>
        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus(
            string id, 
            [FromBody] UpdateDeviceStatusRequest request)
        {
            try
            {
                await _deviceService.UpdateStatusAsync(id, request.Status);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar status do dispositivo: {Id}", id);
                return StatusCode(500, "Erro interno ao processar a requisição.");
            }
        }
    }
}