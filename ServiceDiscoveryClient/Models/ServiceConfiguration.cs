namespace Kwetterprise.ServiceDiscovery.Client.Models
{
    using System;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class ServiceConfiguration
    {
        public ServiceConfiguration(string serviceName, string baseUrl)
        {
            this.Guid = Guid.NewGuid();
            this.ServiceName = serviceName;
            this.BaseUrl = baseUrl;
        }

        [JsonPropertyName("guid")]
        public Guid Guid { get; }

        [JsonPropertyName("serviceName")]
        public string ServiceName { get; }

        [JsonPropertyName("baseUrl")]
        public string BaseUrl { get; }

        internal HttpContent ToRegisterContent()
        {
            return new StringContent(JsonSerializer.Serialize(this), Encoding.UTF8, MediaTypeNames.Application.Json);
        }
    }
}