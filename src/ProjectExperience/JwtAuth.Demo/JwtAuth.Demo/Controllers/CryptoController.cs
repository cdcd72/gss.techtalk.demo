using JwtAuth.Demo.Configuration;
using JwtAuth.Demo.Model;
using JwtAuth.Demo.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Demo.Controllers
{
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
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
