using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Services
{
    public interface IBrokerService
    {
        public  Task<HttpClient> Create(HttpContext httpContext);
        public  Task<HttpResponseMessage> SendJsonPostAsync<T>(HttpClient httpClient, string action, T command);
        public Task<HttpResponseMessage> SendJsonGetAsync<T>(HttpClient httpClient, string action,T command);

    }
}
