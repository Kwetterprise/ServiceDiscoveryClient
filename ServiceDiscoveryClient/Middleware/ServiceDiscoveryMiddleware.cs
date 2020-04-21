namespace Kwetterprise.ServiceDiscovery.Client.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class ServiceDiscoveryMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ServiceDiscoveryMiddleware> logger;

        public ServiceDiscoveryMiddleware(RequestDelegate next, ILogger<ServiceDiscoveryMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value.ToLower() == "/ping")
            {
                this.logger.LogDebug("Processed API gateway ping.");
                httpContext.Response.StatusCode = 200;
                return Task.CompletedTask;
            }

            return this.next(httpContext);
        }
    }
}
