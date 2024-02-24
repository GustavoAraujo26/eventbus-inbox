using EventBusInbox.TypeConverters.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusInbox.TypeConverters.Extensions
{
    /// <summary>
    /// Extensões para configuração do AutoMapper na aplicação
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// Configura a injeção de dependência para o AutoMapper
        /// </summary>
        /// <param name="services">Interface do service collection</param>
        public static void AddAppAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ModelProfile), typeof(EntityProfile));
        }
    }
}
