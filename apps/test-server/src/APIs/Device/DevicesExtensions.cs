using Test.APIs.Dtos;
using Test.Infrastructure.Models;

namespace Test.APIs.Extensions;

public static class DevicesExtensions
{
    public static Device ToDto(this DeviceDbModel model)
    {
        return new Device
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static DeviceDbModel ToModel(
        this DeviceUpdateInput updateDto,
        DeviceWhereUniqueInput uniqueId
    )
    {
        var device = new DeviceDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            device.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            device.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return device;
    }
}
