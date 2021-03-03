using Microsoft.AspNetCore.Mvc;
using StronglyTypedSettingsWithIOptions.Demo.Configuration;

namespace StronglyTypedSettingsWithIOptions.Demo.Controllers
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
