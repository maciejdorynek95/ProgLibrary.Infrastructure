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
            //var a =  _headers.Authorization = new AuthenticationHeaderValue("Bearer", httpContext.Session.GetString("Token"));

            //httpContext.Request.Headers.Add("Authorization", "Bearer " + HttpContext.Session.GetString("Token"));
            //var items = httpContext.Items.Values;
            ////httpContext.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));



        }
    }
}
