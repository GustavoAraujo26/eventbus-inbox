namespace EventBusInbox.Shared.Models
{
    /// <summary>
    /// Classe de configurações com base no ambiente
    /// </summary>
    public class EnvironmentSettings
    {
        static EnvironmentSettings _instance;

        private EnvironmentSettings() { }

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
        /// Adiciona as credenciais de conexão ao MongoDb
        /// </summary>
        /// <param name="connectionString">String de conexão</param>
        /// <param name="databaseName">Nome do banco de dados</param>
        public void AddMongoDbCredentials(string connectionString, string databaseName) =>
            MongoDbCredentials = new KeyValuePair<string, string>(databaseName, connectionString);

        /// <summary>
        /// Adiciona a string de conexão do RabbitMQ
        /// </summary>
        /// <param name="connectionString"></param>
        public void AddRabbitMqConnectionString(string connectionString) => 
            RabbitMqConnectionString = connectionString;
    }
}
