using DataProtectionAPI.Demo.Dto;
using DataProtectionAPI.Demo.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace DataProtectionAPI.Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly IDataProtector protector;

        public CryptoController(IDataProtectionProvider dataProtectionProvider) =>
            protector = dataProtectionProvider.CreateProtector(DataProtectionPurposeStrings.DbPassword);

        [HttpPost]
        [Route("[action]")]
        public ActionResult Encrypt([FromBody] CommonRequest commonRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(new EncryptResult()
            {
                Text = protector.Protect(commonRequest.Text)
            });
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult Decrypt([FromBody] CommonRequest commonRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(new DecryptResult()
            {
                Text = protector.Unprotect(commonRequest.Text)
            });
        }
    }
}
