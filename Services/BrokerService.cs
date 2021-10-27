using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProgLibrary.Infrastructure.Settings.JwtToken;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Services
{
    public class BrokerService : IBrokerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IJwtHandler _jwtHandler;
        private readonly ILogger<BrokerService> _logger;

        public BrokerService(IHttpClientFactory httpClientFactory, IJwtHandler jwthandler, ILogger<BrokerService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _jwtHandler = jwthandler;
            _logger = logger;
        }
        /// <summary>
        /// Create HttpClient with JWT Token
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task<HttpClient> Create(HttpContext httpContext)
        {
            
            var client = _httpClientFactory.CreateClient("api");
                client = await _jwtHandler.AddTokenToHeader(client, httpContext);
            
            _logger.LogInformation($"Client: {client.DefaultRequestHeaders}");
            return client;
        }
        /// <summary>
        /// Send Json to <paramref name="action"/> with <paramref name="command"/> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="action"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendJsonAsync<T>(HttpClient httpClient, string action,T command)
        {
            var response = await httpClient.PostAsJsonAsync(action, command);
            response.EnsureSuccessStatusCode();
            _logger.LogInformation($"Response: {await response.Content.ReadAsStringAsync()}");
            return response;
        }
    }
}