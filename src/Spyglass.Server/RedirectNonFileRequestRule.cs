using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;

namespace Spyglass.Server
{
    public class RedirectNonFileRequestRule : IRule
    {
        protected string BaseUrl { get; }

        public RedirectNonFileRequestRule(string baseUrl = "/")
        {
            this.BaseUrl = baseUrl;
        }

        public void ApplyRule(RewriteContext context)
        {
            var env = context.HttpContext.RequestServices.GetService<IHostingEnvironment>();
            var request = context.HttpContext.Request;

            var osPath = request.Path.Value.Replace('/', Path.DirectorySeparatorChar);
            var fullPath = Path.Join(env.WebRootPath, osPath);

            if (!File.Exists(fullPath))
            {
                request.Path = this.BaseUrl;
            }
        }
    }
}