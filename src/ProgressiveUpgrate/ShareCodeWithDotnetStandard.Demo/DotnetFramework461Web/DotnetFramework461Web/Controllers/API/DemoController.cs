using System.Web.Http;
using DotnetStandard20Utility.Factory;

namespace DotnetFramework461Web.Controllers.API
{
    public class DemoController : ApiController
    {
        private IDemoServiceFactory DemoServiceFactory { get; set; }

        public string Get()
        {
            var demoService = DemoServiceFactory.Create();

            return demoService.GetValue();
        }
    }
}
