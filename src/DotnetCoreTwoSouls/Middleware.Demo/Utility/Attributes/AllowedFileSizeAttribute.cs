using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace Utility.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AllowedFileSizeAttribute : Attribute, IResourceFilter
    {
        private readonly long assignedMaxSize;

        public AllowedFileSizeAttribute(long maxSize = 0)
        {
            assignedMaxSize = maxSize;
        }

        #region ResourceFilter Implement

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var realMaxSize = GetRealAllowedFileSize(context);

            var contentLength = context.HttpContext.Request.ContentLength;

            if (contentLength > realMaxSize)
                throw new ValidationException($"You can't upload file size over {realMaxSize / 1024} KB.");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // Not Implement
        }

        #endregion

        /// <summary>
        /// 取得實際允許檔案大小 (Byte)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private long GetRealAllowedFileSize(ResourceExecutingContext context)
        {
            var config = context.HttpContext.RequestServices.GetService<IConfiguration>();

            // 若已有給定，就不拿預設值..
            return assignedMaxSize != 0 ? assignedMaxSize : config.GetValue<long>("BaseAllowedFileSize");
        }
    }
}
