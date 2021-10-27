using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Middlewares
{
    [Obsolete("Wyłączone")]
    public class PreRequestModifications
    {
        private RequestDelegate _next;


        public PreRequestModifications(RequestDelegate next)
        {
            _next = next;

        }
        public async Task Invoke(HttpContext httpContext)
        {
            
            ModifyRequest(httpContext);            
             await _next(httpContext);

        }

        private void ModifyRequest(HttpContext httpContext)
        {

        }
    }
}
