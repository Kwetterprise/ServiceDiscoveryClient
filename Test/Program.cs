using System;

namespace Test
{
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Kwetterprise.ServiceDiscovery.Client.Models;
    using Kwetterprise.ServiceDiscovery.Client.Worker;
    using Microsoft.Extensions.Logging;

    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceConfiguration = new ServiceConfiguration("SomeService", "https://google.com");
            var apiGatewayConfiguration = new ServiceDiscoveryConfiguration("http://localhost:6221");

            var myLogger = new MyLogger<ServiceDiscoveryHttpClient>();

            var httpClient = new ServiceDiscoveryHttpClient(myLogger, serviceConfiguration, apiGatewayConfiguration);

            await httpClient.Register();

            Console.ReadKey();

            await httpClient.Unregister();

            Console.WriteLine("Hello World!");
        }
    }

    internal class MyLogger<T> : ILogger<T>
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine($"{logLevel} : {formatter(state, exception)}");
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
