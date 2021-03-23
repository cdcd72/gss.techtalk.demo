using DotnetStandard20Utility.Factory;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCore21Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IDemoServiceFactory demoServiceFactory;

        public DemoController(IDemoServiceFactory demoServiceFactory) => this.demoServiceFactory = demoServiceFactory;

        [HttpGet]
        public string Get()
        {
            var demoService = demoServiceFactory.Create();

            return demoService.GetValue();
        }
    }
}
