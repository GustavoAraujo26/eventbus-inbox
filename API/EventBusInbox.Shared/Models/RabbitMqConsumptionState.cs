namespace EventBusInbox.Shared.Models
{
    /// <summary>
    /// Estado do consumo de mensagens do RabbitMQ
    /// </summary>
    public class RabbitMqConsumptionState
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public RabbitMqConsumptionState()
        {
            LastUpdate = DateTime.Now;
        }

        /// <summary>
        /// Última atualização
        /// </summary>
        public DateTime LastUpdate { get; private set; }

        /// <summary>
        /// Verifica se o serviço de leitura de mensagens pode ser finalizado
        /// </summary>
        public bool CanFinish
        {
            get
            {
                var diff = (DateTime.Now - LastUpdate).TotalSeconds;
                return diff > 60;
            }
        }

        /// <summary>
        /// Adiciona atualização na data
        /// </summary>
        public void Update() =>
            LastUpdate = DateTime.Now;
    }
}
