using Microsoft.AspNetCore.Mvc;
using DataProtectionAPI.Demo.Configuration;

namespace DataProtectionAPI.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IDbSettingsResolved dbSettingsResolved;

        public DemoController(IDbSettingsResolved dbSettingsResolved) => this.dbSettingsResolved = dbSettingsResolved;

        [HttpGet]
        public string Get() => $"{dbSettingsResolved.ConnectionType}: {dbSettingsResolved.ConnectionString}";
    }
}
