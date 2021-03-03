using System;
using CreatingInstancesFromIServiceProvider.Demo.Services;
using CreatingInstancesFromIServiceProvider.Demo.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CreatingInstancesFromIServiceProvider.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService demoService;
        private readonly AnotherService anotherService;

        public DemoController(IServiceProvider serviceProvider, IDemoService demoService)
        {
            this.demoService = demoService;
            this.anotherService = new DependencyFactory<AnotherService>(serviceProvider).GetInstance();
        }

        [HttpGet]
        public string Get() => $"{demoService.GetValue()} {anotherService.GetValue()}".HtmlEncode();
    }
}
