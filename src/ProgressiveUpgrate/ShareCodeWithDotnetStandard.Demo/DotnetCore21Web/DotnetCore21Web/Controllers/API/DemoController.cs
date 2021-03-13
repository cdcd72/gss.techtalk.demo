using DotnetStandard20Utility.Factory;
using DotnetStandard20Utility.Service;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCore21Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService demoService;

        public DemoController(IDemoServiceFactory demoServiceFactory) => demoService = demoServiceFactory.Create();

        [HttpGet]
        public string Get() => demoService.GetValue();
    }
}
