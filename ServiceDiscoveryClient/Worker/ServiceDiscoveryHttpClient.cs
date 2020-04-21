namespace Kwetterprise.ServiceDiscovery.Client.Worker
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Kwetterprise.ServiceDiscovery.Client.Models;
    using Microsoft.Extensions.Logging;

    public class ServiceDiscoveryHttpClient
    {
        private readonly ServiceConfiguration serviceConfiguration;
        private readonly ServiceDiscoveryConfiguration apiGatewayConfiguration;
        private readonly HttpClient client = new HttpClient();

        public ServiceDiscoveryHttpClient(
            ServiceConfiguration serviceConfiguration,
            ServiceDiscoveryConfiguration apiGatewayConfiguration)
        {
            this.serviceConfiguration = serviceConfiguration;
            this.apiGatewayConfiguration = apiGatewayConfiguration;
        }

        public async Task Register()
        {
            await this.client.PostAsync(
                this.apiGatewayConfiguration.RegisterUrl,
                this.serviceConfiguration.ToRegisterContent());
        }

        public async Task Unregister()
        {
           await this.client.DeleteAsync(
                this.apiGatewayConfiguration.CreateUnregisterUrl(this.serviceConfiguration.Guid));
        }
    }
}
