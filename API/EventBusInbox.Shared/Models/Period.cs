namespace EventBusInbox.Shared.Models
{
    /// <summary>
    /// Container para tratamento de períodos de tempo
    /// </summary>
    public class Period
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public Period() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="start">Data de início</param>
        /// <param name="end">Data de término</param>
        public Period(DateTime? start, DateTime? end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Data de início
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// Data de término
        /// </summary>
        public DateTime? End { get; set; }
    }
}
