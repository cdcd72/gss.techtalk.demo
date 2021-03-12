using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Utility.Validators;

namespace Utility.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AllowedFileExtensionsAttribute : Attribute, IResourceFilter
    {
        private readonly string[] assignedFileExtensions;

        public AllowedFileExtensionsAttribute(params string[] fileExtensions)
        {
            assignedFileExtensions = fileExtensions;
        }

        #region ResourceFilter Implement

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // 實際副檔名白名單
            var realAllowedFileExtensions = GetRealAllowedFileExtensions(context);

            IFormFileCollection formFiles = context.HttpContext.Request.Form.Files;

            foreach (var formFile in formFiles)
            {
                var fileName = formFile.FileName;

                // 驗證副檔名白名單
                if (!realAllowedFileExtensions.Contains(Path.GetExtension(fileName)))
                    throw new ValidationException($"You can't upload this file, because of it violation file white list.");

                using var formFileStream = formFile.OpenReadStream();

                // 驗證是否為"有效"副檔案(被偽造副檔名)
                if (!IsValidFileExtension(formFile.FileName, ConvertStreamToBytes(formFileStream)))
                    throw new ValidationException($"You can't upload this file, because of it faked with other file extension.");
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // Not Implement
        }

        #endregion

        #region Private Method

        /// <summary>
        /// 取得實際允許檔案副檔名(副檔名白名單)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string[] GetRealAllowedFileExtensions(ResourceExecutingContext context)
        {
            var config = context.HttpContext.RequestServices.GetService<IConfiguration>();

            // 分隔符號
            char[] delimiterChars = { ',' };

            // 若已有給定，就不拿預設值..
            var allowedFileExtensions = assignedFileExtensions.Length > 0 ?
                assignedFileExtensions : config.GetValue<string>("BaseAllowedFileExtensions")
                                               .Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            return allowedFileExtensions.Select(x => $".{x}").ToArray();
        }

        /// <summary>
        /// 驗證是否為有效副檔名
        /// </summary>
        /// <param name="fileName">檔案名稱(含副檔名)</param>
        /// <param name="fileData">檔案資料</param>
        /// <param name="allowedChars">允許通過字元</param>
        /// <returns></returns>
        private bool IsValidFileExtension(string fileName, byte[] fileData, byte[] allowedChars = null)
        {
            return FileExtensionValidator.IsValidFileExtension(fileName, fileData, allowedChars);
        }

        /// <summary>
        /// 驗證是否為有效副檔名
        /// </summary>
        /// <param name="fileName">檔案名稱(含副檔名)</param>
        /// <param name="fs">檔案串流</param>
        /// <param name="allowedChars">允許通過字元</param>
        /// <returns></returns>
        private bool IsValidFileExtension(string fileName, FileStream fs, byte[] allowedChars = null)
        {
            return FileExtensionValidator.IsValidFileExtension(fileName, fs, allowedChars);
        }

        /// <summary>
        /// 轉換串流為 Byte Array
        /// </summary>
        /// <param name="stream">串流</param>
        /// <returns></returns>
        private byte[] ConvertStreamToBytes(Stream stream)
        {
            byte[] buffer = new byte[8 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;

                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        #endregion
    }
}
