namespace EventBusInbox.Shared.Models
{
    /// <summary>
    /// Container de informações de enumerador
    /// </summary>
    public class EnumData
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public EnumData() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="intKey">Chave no formato de inteiro</param>
        /// <param name="stringKey">Chave no formato de string</param>
        /// <param name="description">Descrição do valor do enumerador</param>
        public EnumData(int? intKey, string? stringKey, string? description)
        {
            IntKey = intKey;
            StringKey = stringKey;
            Description = description;
        }

        /// <summary>
        /// Chave no formato de inteiro
        /// </summary>
        public int? IntKey { get; private set; }

        /// <summary>
        /// Chave no formato de string
        /// </summary>
        public string? StringKey { get; private set; }

        /// <summary>
        /// Descrição do valor do enumerador
        /// </summary>
        public string? Description { get; private set; }
    }
}
