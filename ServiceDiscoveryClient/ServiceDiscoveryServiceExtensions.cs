namespace Kwetterprise.ServiceDiscovery.Client
{
    using Kwetterprise.ServiceDiscovery.Client.Middleware;
    using Kwetterprise.ServiceDiscovery.Client.Models;
    using Kwetterprise.ServiceDiscovery.Client.Worker;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceDiscoveryServiceExtensions
    {
        public static IServiceCollection AddServiceDiscoveryClientWorker(
            this IServiceCollection services,
            ServiceConfiguration serviceConfiguration,
            ServiceDiscoveryConfiguration apiGatewayConfiguration)
        {
            services.AddSingleton(serviceConfiguration);
            services.AddSingleton(apiGatewayConfiguration);
            
            services.AddTransient<ServiceDiscoveryHttpClient>();
            services.AddHostedService<ApiGatewayWorker>();

            return services;
        }

        public static IApplicationBuilder AddServiceDiscoveryPingMiddleware(
            this IApplicationBuilder app)
        {
            app.UseMiddleware<ServiceDiscoveryMiddleware>();
            return app;
        }
    }
}