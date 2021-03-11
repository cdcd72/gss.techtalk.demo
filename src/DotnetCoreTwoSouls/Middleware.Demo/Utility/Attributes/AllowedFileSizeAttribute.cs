using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.ComponentModel.DataAnnotations;

namespace Utility.Attributes
{
    public class AllowedFileSizeAttribute : Attribute, IResourceFilter
    {
        private readonly long maxSize;

        public AllowedFileSizeAttribute(long maxSize)
        {
            this.maxSize = maxSize;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var contentLength = context.HttpContext.Request.ContentLength;

            if (contentLength > maxSize)
                throw new ValidationException($"You can't upload file size over {maxSize / 1024} KB.");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // Not implemented
        }
    }
}
