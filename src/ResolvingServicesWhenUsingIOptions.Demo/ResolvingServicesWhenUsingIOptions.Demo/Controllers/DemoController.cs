using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ResolvingServicesWhenUsingIOptions.Demo.Model;
using ResolvingServicesWhenUsingIOptions.Demo.Utility;

namespace ResolvingServicesWhenUsingIOptions.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly SomeOptions someOptions;

        public DemoController(IOptions<SomeOptions> someOptions) =>
            this.someOptions = someOptions.Value;

        [HttpGet]
        public string Get() =>
            $"{someOptions.Setting} {someOptions.AnotherSetting}".HtmlEncode();
    }
}
