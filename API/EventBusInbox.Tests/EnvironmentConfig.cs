using Microsoft.Extensions.DependencyInjection;
using EventBusInbox.TypeConverters.Extensions;
using EventBusInbox.Handlers.Extensions;

namespace EventBusInbox.Tests
{
    internal static class EnvironmentConfig
    {
        public static IServiceCollection BuildServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.ConfigureAppAutoMapper();
            services.ConfigureAppMediator();

            return services;
        }
    }
}
