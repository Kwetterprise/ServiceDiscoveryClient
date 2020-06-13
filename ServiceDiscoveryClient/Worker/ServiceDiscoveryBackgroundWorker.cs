namespace Kwetterprise.ServiceDiscovery.Client.Worker
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class ApiGatewayWorker : IHostedService, IDisposable
    {
        private readonly ServiceDiscoveryHttpClient apiGatewayHttpClient;
        private readonly ILogger<ApiGatewayWorker> logger;
        private Timer timer = null!;
        private Task registerTask = null!;

        public ApiGatewayWorker(ServiceDiscoveryHttpClient apiGatewayHttpClient, ILogger<ApiGatewayWorker> logger)
        {
            this.apiGatewayHttpClient = apiGatewayHttpClient;
            this.logger = logger;
        }

        private void DoWork(object? state)
        {
            try
            {
                this.logger.LogDebug("Registering service.");
                this.registerTask = this.apiGatewayHttpClient.Register();
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Exception during registration.");
            }

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(this.DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));

            return Task.CompletedTask;
        }


        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Stopping API register worker.");

            this.timer?.Change(Timeout.Infinite, 0);

            if (this.registerTask != null)
            {
                await this.registerTask;
            }

            try
            {
                await this.apiGatewayHttpClient.Unregister();
            }
            catch (Exception e)
            {
                this.logger.LogDebug(e, "Unable to unregister.");
            }
        }

        public void Dispose()
        {
            this.timer?.Dispose();
            this.registerTask?.Dispose();
        }
    }
}
