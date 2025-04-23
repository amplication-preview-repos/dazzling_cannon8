using Microsoft.EntityFrameworkCore;
using Test.Infrastructure.Models;

namespace Test.Infrastructure;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options) { }

    public DbSet<DeviceDbModel> Devices { get; set; }
}
