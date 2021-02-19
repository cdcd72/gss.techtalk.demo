using GenericTypeAsAFactory.Demo.Services;
using GenericTypeAsAFactory.Demo.Utility;
using Microsoft.AspNetCore.Mvc;

namespace GenericTypeAsAFactory.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService demoService;
        private readonly AnotherService anotherService;

        public DemoController(IServiceFactory<IDemoService> demoServiceFactory, IServiceFactory<AnotherService> anotherServiceFactory)
        {
            demoService = demoServiceFactory.Service;
            anotherService = anotherServiceFactory.Service;
        }

        [HttpGet]
        public string Get() => $"{demoService.GetValue()} {anotherService.GetValue()}".HtmlEncode();
    }
}
