using Microsoft.AspNetCore.Builder;
using System;
using Utility.Middleware;

namespace Utility.Extensions.Middleware
{
    public static class XFrameMiddlewareExtensions
    {
        /// <summary>
        /// 使用 X-Frame-Options 中介程序
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseXFrame(this IApplicationBuilder app, XFrameOptions options)
        {
            return app.UseMiddleware<XFrameMiddleware>(options);
        }

        /// <summary>
        /// 使用 X-Frame-Options 中介程序
        /// </summary>
        /// <param name="app"></param>
        /// <param name="setup"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseXFrame(this IApplicationBuilder app, Action<XFrameOptions> setup)
        {
            var options = new XFrameOptions();

            if (setup != null)
                setup(options);

            return app.UseMiddleware<XFrameMiddleware>(options);
        }
    }
}
