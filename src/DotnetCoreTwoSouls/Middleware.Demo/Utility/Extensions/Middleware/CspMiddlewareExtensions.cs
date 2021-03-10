using Microsoft.AspNetCore.Builder;
using System;
using Utility.Middleware;

namespace Utility.Extensions.Middleware
{
    public static class CspMiddlewareExtensions
    {
        /// <summary>
        /// 使用 Content Security Policy 中介程序
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCsp(this IApplicationBuilder app, CspOptions options)
        {
            return app.UseMiddleware<CspMiddleware>(options);
        }

        /// <summary>
        /// 使用 Content Security Policy 中介程序
        /// </summary>
        /// <param name="app"></param>
        /// <param name="setup"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCsp(this IApplicationBuilder app, Action<CspOptions> setup)
        {
            var options = new CspOptions();

            if (setup != null)
                setup(options);

            return app.UseMiddleware<CspMiddleware>(options);
        }
    }
}
