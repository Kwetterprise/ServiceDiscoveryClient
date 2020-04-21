namespace Kwetterprise.ServiceDiscovery.Client.Models
{
    using System;

    public class ServiceDiscoveryConfiguration
    {
        public ServiceDiscoveryConfiguration(string url)
        {
            this.Url = url;
        }

        public string Url { get; }

        internal string RegisterUrl => $"{this.Url}/register";

        internal string CreateUnregisterUrl(Guid guid) => $"{this.Url}/unregister?guid={guid}";
    }
}