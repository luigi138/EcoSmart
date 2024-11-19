using EcoSmart.Core.DTOs;
using EcoSmart.Core.Interfaces;
using EcoSmart.Core.Exceptions;
using EcoSmart.Domain.Enums;
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeviceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeviceDto>> GetById(string id)
        {
            try
            {
                var device = await _deviceService.GetByIdAsync(id);
                if (device == null)
                    return NotFound($"Device with ID {id} not found.");

                return Ok(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving device: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

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
                _logger.LogError(ex, "Error retrieving user devices: {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }

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
                _logger.LogError(ex, "Error creating device");
                return StatusCode(500, "Internal server error");
            }
        }

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
                _logger.LogError(ex, "Error updating device status: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}