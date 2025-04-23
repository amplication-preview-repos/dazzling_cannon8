using Test.APIs.Common;
using Test.APIs.Dtos;

namespace Test.APIs;

public interface IDevicesService
{
    /// <summary>
    /// Create one device
    /// </summary>
    public Task<Device> CreateDevice(DeviceCreateInput device);

    /// <summary>
    /// Delete one device
    /// </summary>
    public Task DeleteDevice(DeviceWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many devices
    /// </summary>
    public Task<List<Device>> Devices(DeviceFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about device records
    /// </summary>
    public Task<MetadataDto> DevicesMeta(DeviceFindManyArgs findManyArgs);

    /// <summary>
    /// Get one device
    /// </summary>
    public Task<Device> Device(DeviceWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one device
    /// </summary>
    public Task UpdateDevice(DeviceWhereUniqueInput uniqueId, DeviceUpdateInput updateDto);
}
