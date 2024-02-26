namespace EventBusInbox.Shared.Models
{
    /// <summary>
    /// Classe de configurações com base no ambiente
    /// </summary>
    public class EnvironmentSettings
    {
        static EnvironmentSettings _instance;

        private EnvironmentSettings()
        {
            MongoDbCredentials = new KeyValuePair<string, string>(
                Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME"),
                Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING")
            );
            RabbitMqConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTION_STRING");
        }

        /// <summary>
        /// Obtém a instância a classe
        /// </summary>
        public static EnvironmentSettings Instance
        {
            get
            {
                return _instance ?? (_instance = new EnvironmentSettings());
            }
        }

        /// <summary>
        /// Configuração de string de conexão para o MongoDB
        /// </summary>
        public KeyValuePair<string, string> MongoDbCredentials { get; private set; }

        /// <summary>
        /// String de conexão do RabbitMQ
        /// </summary>
        public string RabbitMqConnectionString { get; private set; }

        /// <summary>
        /// Constrói string de conexão do MongoDB, especificando o banco de dados
        /// </summary>
        /// <returns></returns>
        public string GetMongoDbDatabaseUrl() =>
            $"{MongoDbCredentials.Value}/{MongoDbCredentials.Key}";
    }
}
