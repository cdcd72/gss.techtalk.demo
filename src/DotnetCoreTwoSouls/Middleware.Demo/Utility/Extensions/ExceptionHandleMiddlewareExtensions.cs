using Microsoft.AspNetCore.Builder;
using Utility.Middleware;

namespace Utility.Extensions
{
    public static class ExceptionHandleMiddlewareExtensions
    {
        /// <summary>
        /// 使用例外處理中介程序
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandleMiddleware>();
        }
    }
}
