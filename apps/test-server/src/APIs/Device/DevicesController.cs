using Microsoft.AspNetCore.Mvc;

namespace Test.APIs;

[ApiController()]
public class DevicesController : DevicesControllerBase
{
    public DevicesController(IDevicesService service)
        : base(service) { }
}
