using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Infrastructure.Middlewares
{
    //this middleware Block IE by checking user-agent header
    public class BlockIEMiddleware
    {
        private RequestDelegate _next;
        public BlockIEMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Headers["Agent"] == "IE")
                await httpContext.Response.WriteAsync("IE Not Supported");
            else
                await _next.Invoke(httpContext);
        }
    }
}
