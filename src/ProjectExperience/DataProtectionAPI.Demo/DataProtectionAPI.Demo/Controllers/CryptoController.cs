using DataProtectionAPI.Demo.Configuration;
using DataProtectionAPI.Demo.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace DataProtectionAPI.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly IDbSettingsResolved dbSettingsResolved;
        private readonly IDataProtector protector;

        public CryptoController(IDbSettingsResolved dbSettingsResolved, IDataProtectionProvider dataProtectionProvider)
        {
            this.dbSettingsResolved = dbSettingsResolved;

            protector = dataProtectionProvider.CreateProtector(DataProtectionPurposeStrings.DbPassword);
        }

        [HttpGet]
        public IDbSettingsResolved Get() => dbSettingsResolved;

        [HttpPost]
        [Route("[action]")]
        public string Encrypt(string text) => protector.Protect(text);

        [HttpPost]
        [Route("[action]")]
        public string Decrypt(string text) => protector.Unprotect(text);
    }
}
