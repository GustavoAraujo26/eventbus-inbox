﻿using EventBusInbox.Shared.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusInbox.Shared.Extensions
{
    /// <summary>
    /// Extensões para configuração do ambiente
    /// </summary>
    public static class EnvironmentExtensions
    {
        /// <summary>
        /// Realiza a injeção de dependência das configurações do ambiente
        /// </summary>
        /// <param name="services">Interface do Service Collection</param>
        public static void ConfigureEnvironmentSettings(this IServiceCollection services) =>
            services.AddSingleton(EnvironmentSettings.Instance);
    }
}
