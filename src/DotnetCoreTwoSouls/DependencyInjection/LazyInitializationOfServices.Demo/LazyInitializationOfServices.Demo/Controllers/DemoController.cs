using LazyInitializationOfServices.Demo.Services;
using LazyInitializationOfServices.Demo.Utility;
using Microsoft.AspNetCore.Mvc;

namespace LazyInitializationOfServices.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILazyFactory<IDemoService> lazyDemoServiceFactory;
        private readonly ILazyFactory<AnotherService> lazyAnotherServiceFactory;

        public DemoController(ILazyFactory<IDemoService> lazyDemoServiceFactory, ILazyFactory<AnotherService> lazyAnotherServiceFactory)
        {
            // 有時候會希望延遲實例初始化，這時就可以善用 Lazy<T> (https://docs.microsoft.com/zh-tw/dotnet/api/system.lazy-1?view=netcore-3.1)
            // PS. 但要注意千萬別在建構子去 lazyFactory.Value 來初始化物件，這樣就失去延遲初始化的本意了(ex. 希望初始化物件在 DemoController 建構階段之後...)
            this.lazyDemoServiceFactory = lazyDemoServiceFactory;
            this.lazyAnotherServiceFactory = lazyAnotherServiceFactory;
        }

        [HttpGet]
        public string Get() =>
            // DemoController 初始化時，IDemoService & AnotherService 皆還沒真正被初始化，直到呼叫 lazyFactory.Value 才初始化物件...
            $"{lazyDemoServiceFactory.Value.GetValue()} {lazyAnotherServiceFactory.Value.GetValue()}".HtmlEncode();
    }
}
