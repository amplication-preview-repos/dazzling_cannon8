using Microsoft.AspNetCore.Mvc;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;

namespace Test.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class DevicesControllerBase : ControllerBase
{
    protected readonly IDevicesService _service;

    public DevicesControllerBase(IDevicesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one device
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Device>> CreateDevice(DeviceCreateInput input)
    {
        var device = await _service.CreateDevice(input);

        return CreatedAtAction(nameof(Device), new { id = device.Id }, device);
    }

    /// <summary>
    /// Delete one device
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteDevice([FromRoute()] DeviceWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteDevice(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many devices
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Device>>> Devices([FromQuery()] DeviceFindManyArgs filter)
    {
        return Ok(await _service.Devices(filter));
    }

    /// <summary>
    /// Meta data about device records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> DevicesMeta(
        [FromQuery()] DeviceFindManyArgs filter
    )
    {
        return Ok(await _service.DevicesMeta(filter));
    }

    /// <summary>
    /// Get one device
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Device>> Device([FromRoute()] DeviceWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Device(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one device
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateDevice(
        [FromRoute()] DeviceWhereUniqueInput uniqueId,
        [FromQuery()] DeviceUpdateInput deviceUpdateDto
    )
    {
        try
        {
            await _service.UpdateDevice(uniqueId, deviceUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
