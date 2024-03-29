﻿using Asp.Versioning.ApiExplorer;
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
                Description = "API construída em .NET 6, que tem como foco principal atuar como uma caixa de entrada " +
                "para uma mensageria (no sistema está sendo utilizado RabbitMQ), visando simplificar/facilitar o tratamento " +
                "de deadletters por parte dos microsserviços. <br/> Principais funcionalidades da API: <br/> " +
                "<ul><li>Controle automatizado de filas a serem lidas, através de um cadastro</li>" +
                "<li>Persistência de mensagens recebidas do barramento em um banco de dados (MongoDB)</li>" +
                "<li>Controle do estado/status das mensagens</li><li>Endpoint para envio de mensagens para filas já previamente cadastradas.</li></ul>",
                Contact = new OpenApiContact
                {
                    Email = "gustavo.dearaujo26@gmail.com",
                    Url = new Uri("https://github.com/GustavoAraujo26/eventbus-inbox")
                }
            };

            if (description.IsDeprecated)
                info.Description += " This API version has been deprecated. Please use one of the new APIS available from explorer.";

            return info;
        }
    }
}
