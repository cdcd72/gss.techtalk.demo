using LazyInitializationOfServices.Demo.Services;
using LazyInitializationOfServices.Demo.Utility;
using Microsoft.AspNetCore.Mvc;

namespace LazyInitializationOfServices.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILazy<IDemoService> demoService;
        private readonly ILazy<AnotherService> anotherService;

        public DemoController(ILazy<IDemoService> demoService, ILazy<AnotherService> anotherService)
        {
            // 有時候會希望延遲實例初始化，這時就可以善用 Lazy<T> (https://docs.microsoft.com/zh-tw/dotnet/api/system.lazy-1?view=netcore-3.1)
			// PS. 但要注意千萬別在建構子去 demoService.Value 初始化物件，這樣相當於是注入 ServiceProvider 並 GetService<demoService>() 的行為，被視為是 Anti Pattern...
            this.demoService = demoService;
            this.anotherService = anotherService;
        }

        [HttpGet]
        public string Get() =>
            // DemoController 初始化時，demoService & anotherService 皆還沒真正被初始化...
            $"{demoService.Value.GetValue()} {anotherService.Value.GetValue()}".HtmlEncode();
    }
}
