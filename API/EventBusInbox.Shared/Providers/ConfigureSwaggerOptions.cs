using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EventBusInbox.Shared.Providers
{
    /// <summary>
    /// Configuração das opções do Swagger
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="provider"></param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Configura as opções do Swagger (herdado da interface)
        /// </summary>
        /// <param name="name">Nome</param>
        /// <param name="options">opções</param>
        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        /// <summary>
        /// Configura cada API descoberta pelo Swagger
        /// </summary>
        /// <param name="options">Opções</param>
        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerGeneratorOptions.SwaggerDocs.Clear();
            
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }

        /// <summary>
        /// Cria a informação sobre a versão da API
        /// </summary>
        /// <param name="description">Informação sobre a API</param>
        /// <returns></returns>
        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Event Bus Inbox API",
                Version = description.ApiVersion.ToString(),
                Description = "An .NET Core API to act like a inbox for event bus, on microsservices environment."
            };

            if (description.IsDeprecated)
                info.Description += " This API version has been deprecated. Please use one of the new APIS available from explorer.";

            return info;
        }
    }
}
