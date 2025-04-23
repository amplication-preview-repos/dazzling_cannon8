using Test.Infrastructure;

namespace Test.APIs;

public class DevicesService : DevicesServiceBase
{
    public DevicesService(TestDbContext context)
        : base(context) { }
}
