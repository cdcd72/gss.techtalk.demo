using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Utility.Middleware
{
    /// <summary>
    /// X-Frame-Options 中介程序
    /// </summary>
    public class XFrameMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly XFrameOptions _options;

        private string Header => "X-Frame-Options";

        private string HeaderValue => _options.XFrame.XFrameOptions;

        public XFrameMiddleware(RequestDelegate next, XFrameOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add(Header, HeaderValue);
            await _next(context);
        }
    }

    /// <summary>
    /// X-Frame-Options Options
    /// </summary>
    public class XFrameOptions
    {
        public XFrameDirective XFrame { get; set; } = new XFrameDirective();
    }

    /// <summary>
    /// X-Frame-Options Directive
    /// </summary>
    public class XFrameDirective
    {
        public string XFrameOptions { get; set; }

        public void AllowAny()
        {
            XFrameOptions = string.Empty;
        }

        public void Deny()
        {
            XFrameOptions = "DENY";
        }

        public void SameOrigin()
        {
            XFrameOptions = "SAMEORIGIN";
        }

        public void Allow(string source)
        {
            XFrameOptions = $"ALLOW-FROM {source}";
        }
    }
}
