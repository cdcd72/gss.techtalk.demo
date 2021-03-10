using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Middleware
{
    /// <summary>
    /// Content Security Policy 中介程序
    /// </summary>
    public class CspMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CspOptions _options;

        private string Header => _options.ReadOnly
            ? "Content-Security-Policy-Report-Only" : "Content-Security-Policy";

        private string HeaderValue
        {
            get
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(_options.Defaults);
                stringBuilder.Append(_options.Connects);
                stringBuilder.Append(_options.Fonts);
                stringBuilder.Append(_options.Frames);
                stringBuilder.Append(_options.Images);
                stringBuilder.Append(_options.Media);
                stringBuilder.Append(_options.Objects);
                stringBuilder.Append(_options.Scripts);
                stringBuilder.Append(_options.Styles);
                if (!string.IsNullOrEmpty(_options.ReportURL))
                    stringBuilder.Append($"report-uri {_options.ReportURL};");
                stringBuilder.Append(_options.FrameAncestors);
                return stringBuilder.ToString();
            }
        }

        public CspMiddleware(RequestDelegate next, CspOptions options)
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
    /// Content Security Policy Options
    /// https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy
    /// </summary>
    public class CspOptions
    {
        public bool ReadOnly { get; set; }

        public CspDirective Defaults { get; set; } = new CspDirective("default-src");

        public CspDirective Connects { get; set; } = new CspDirective("connect-src");

        public CspDirective Fonts { get; set; } = new CspDirective("font-src");

        public CspDirective Frames { get; set; } = new CspDirective("frame-src");

        public CspDirective Images { get; set; } = new CspDirective("img-src");

        public CspDirective Media { get; set; } = new CspDirective("media-src");

        public CspDirective Objects { get; set; } = new CspDirective("object-src");

        public CspDirective Scripts { get; set; } = new CspDirective("script-src");

        public CspDirective Styles { get; set; } = new CspDirective("style-src");

        public string ReportURL { get; set; }

        public CspDirective FrameAncestors { get; set; } = new CspDirective("frame-ancestors");
    }

    /// <summary>
    /// Content Security Policy Directive
    /// </summary>
    public class CspDirective
    {
        private readonly string _directive;
        private readonly List<string> _sources;

        internal CspDirective(string directive)
        {
            _directive = directive;
            _sources = new List<string>();
        }

        public virtual CspDirective AllowAny() => Allow("*");

        public virtual CspDirective AllowSelf() => Allow("'self'");

        public virtual CspDirective Disallow() => Allow("'none'");

        public virtual CspDirective Allow(string source)
        {
            _sources.Add(source);
            return this;
        }

        public override string ToString() => 
            _sources.Count > 0 ? $"{_directive} {string.Join(" ", _sources)}; " : string.Empty;
    }
}
