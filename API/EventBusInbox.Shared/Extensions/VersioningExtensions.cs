using Asp.Versioning;
using EventBusInbox.Shared.Models;
using EventBusInbox.Shared.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusInbox.Shared.Extensions
{
    /// <summary>
    /// Extensões para versionamento do sistema
    /// </summary>
    public static class VersioningExtensions
    {
        /// <summary>
        /// Configura o versionamento da API
        /// </summary>
        /// <param name="services">Interface da coleção de serviços</param>
        public static void ConfigureAppVersioning(this IServiceCollection services)
        {
            var envSettings = services.GetService<EnvironmentSettings>();
            if (envSettings is null)
                return;

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version"));
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.ConfigureOptions<ConfigureSwaggerOptions>();
        }
    }
}
