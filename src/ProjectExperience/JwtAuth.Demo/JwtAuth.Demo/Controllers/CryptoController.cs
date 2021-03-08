using JwtAuth.Demo.Configuration;
using JwtAuth.Demo.Dto;
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
        private readonly IDataProtector protector;

        public CryptoController(IDataProtectionProvider dataProtectionProvider) =>
            protector = dataProtectionProvider.CreateProtector(DataProtectionPurposeStrings.DbPassword);

        [HttpPost]
        [Route("[action]")]
        public ActionResult Encrypt([FromBody] EncryptRequest encryptRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(new EncryptResult()
            {
                Text = protector.Protect(encryptRequest.Text)
            });
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult Decrypt([FromBody] DecryptRequest decryptRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(new DecryptResult()
            {
                Text = protector.Unprotect(decryptRequest.Text)
            });
        }
    }
}
