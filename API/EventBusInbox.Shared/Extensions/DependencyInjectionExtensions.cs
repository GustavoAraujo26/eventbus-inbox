using Microsoft.Extensions.DependencyInjection;

namespace EventBusInbox.Shared.Extensions
{
    /// <summary>
    /// Extensão para a coleção de serviços
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Obtém um serviço dentro da coleção de serviços
        /// </summary>
        /// <typeparam name="T">Tipo do serviço</typeparam>
        /// <param name="services">Interface da coleção de serviços</param>
        /// <returns></returns>
        public static T GetService<T>(this IServiceCollection services) where T : class =>
            services.BuildServiceProvider().GetService<T>();

        /// <summary>
        /// Obtém um serviço dentro do provedor de serviços
        /// </summary>
        /// <typeparam name="T">Tipo da classe do serviço</typeparam>
        /// <param name="serviceProvider">Provedor de serviço</param>
        /// <returns></returns>
        public static T GetAppService<T>(this IServiceProvider serviceProvider) where T : class => 
            serviceProvider.GetRequiredService<T>();
    }
}
