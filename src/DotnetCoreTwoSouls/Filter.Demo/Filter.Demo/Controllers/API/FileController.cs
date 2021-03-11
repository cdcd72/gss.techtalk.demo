using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Utility.Attributes;

namespace Filter.Demo.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        #region Properties

        private readonly IConfiguration config;

        #endregion

        #region Constructor

        public FileController(IConfiguration config) => this.config = config;

        #endregion

        [HttpPost]
        [Route("[action]")]
        [AllowedFileSize(1024)]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var storedFilesPath = config["StoredFilesPath"];

            if (!Directory.Exists(storedFilesPath))
                Directory.CreateDirectory(storedFilesPath);

            var size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(storedFilesPath, Path.GetRandomFileName());

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok((count: files.Count, size));
        }
    }
}
