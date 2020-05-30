namespace Kwetterprise.ServiceDiscovery.Client.Worker
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Kwetterprise.ServiceDiscovery.Client.Models;
    using Microsoft.Extensions.Logging;

    public class ServiceDiscoveryHttpClient
    {
        private readonly ILogger<ServiceDiscoveryHttpClient> logger;
        private readonly ServiceConfiguration serviceConfiguration;
        private readonly ServiceDiscoveryConfiguration apiGatewayConfiguration;
        private readonly HttpClient client = new HttpClient();

        public ServiceDiscoveryHttpClient(
            ILogger<ServiceDiscoveryHttpClient> logger,
            ServiceConfiguration serviceConfiguration,
            ServiceDiscoveryConfiguration apiGatewayConfiguration)
        {
            this.logger = logger;
            this.serviceConfiguration = serviceConfiguration;
            this.apiGatewayConfiguration = apiGatewayConfiguration;
        }

        public async Task Register()
        {
            try
            {
                var response = await this.client.PostAsync(
                    this.apiGatewayConfiguration.RegisterUrl,
                    this.serviceConfiguration.ToRegisterContent());
                
                this.logger.LogDebug($"Register response code: {response.StatusCode}");
            }
            catch (Exception exception)
            {
                this.logger.LogDebug("Unable to register.", exception);
            }
        }

        public async Task Unregister()
        {
            try
            {
                var response = await this.client.DeleteAsync(
                     this.apiGatewayConfiguration.CreateUnregisterUrl(this.serviceConfiguration.Guid));

                this.logger.LogDebug($"Unregister response code: {response.StatusCode}");
            }
            catch (Exception exception)
            {
                this.logger.LogDebug("Unable to unregister.", exception);
            }
        }
    }
}
