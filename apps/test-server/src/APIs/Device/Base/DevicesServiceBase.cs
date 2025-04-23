using Microsoft.EntityFrameworkCore;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;
using Test.APIs.Extensions;
using Test.Infrastructure;
using Test.Infrastructure.Models;

namespace Test.APIs;

public abstract class DevicesServiceBase : IDevicesService
{
    protected readonly TestDbContext _context;

    public DevicesServiceBase(TestDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one device
    /// </summary>
    public async Task<Device> CreateDevice(DeviceCreateInput createDto)
    {
        var device = new DeviceDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            device.Id = createDto.Id;
        }

        _context.Devices.Add(device);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<DeviceDbModel>(device.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one device
    /// </summary>
    public async Task DeleteDevice(DeviceWhereUniqueInput uniqueId)
    {
        var device = await _context.Devices.FindAsync(uniqueId.Id);
        if (device == null)
        {
            throw new NotFoundException();
        }

        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many devices
    /// </summary>
    public async Task<List<Device>> Devices(DeviceFindManyArgs findManyArgs)
    {
        var devices = await _context
            .Devices.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return devices.ConvertAll(device => device.ToDto());
    }

    /// <summary>
    /// Meta data about device records
    /// </summary>
    public async Task<MetadataDto> DevicesMeta(DeviceFindManyArgs findManyArgs)
    {
        var count = await _context.Devices.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one device
    /// </summary>
    public async Task<Device> Device(DeviceWhereUniqueInput uniqueId)
    {
        var devices = await this.Devices(
            new DeviceFindManyArgs { Where = new DeviceWhereInput { Id = uniqueId.Id } }
        );
        var device = devices.FirstOrDefault();
        if (device == null)
        {
            throw new NotFoundException();
        }

        return device;
    }

    /// <summary>
    /// Update one device
    /// </summary>
    public async Task UpdateDevice(DeviceWhereUniqueInput uniqueId, DeviceUpdateInput updateDto)
    {
        var device = updateDto.ToModel(uniqueId);

        _context.Entry(device).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Devices.Any(e => e.Id == device.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
