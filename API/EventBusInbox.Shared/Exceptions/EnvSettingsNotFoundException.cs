namespace EventBusInbox.Shared.Exceptions
{
    /// <summary>
    /// Exceção de configuração da classe de "configurações do ambiente"
    /// </summary>
    public class EnvSettingsNotFoundException : Exception
    {
        /// <summary>
        /// Construtor para inicializar a exceção
        /// </summary>
        public EnvSettingsNotFoundException() : base("Environment settings not configured properly!") { }
    }
}
