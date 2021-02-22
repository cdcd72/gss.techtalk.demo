using Microsoft.AspNetCore.Mvc;
using SingleImplementationMultipleInterfaces.Demo.Utility;

namespace SingleImplementationMultipleInterfaces.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ISecurityTool securityTool;
        private readonly IHashTool hashTool;

        public DemoController(ISecurityTool securityTool, IHashTool hashTool)
        {
            this.securityTool = securityTool;
            this.hashTool = hashTool;
        }

        [HttpGet]
        public string Get() => "哈哈~ 是我啦! _(:3 」∠ )_";

        [HttpGet]
        [Route("IsSecurity")]
        public bool IsSecurity() => securityTool.IsSecurity();

        [HttpGet]
        [Route("Hash")]
        public string Hash() => hashTool.Hash();
    }
}
