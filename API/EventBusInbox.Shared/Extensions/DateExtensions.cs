namespace EventBusInbox.Shared.Extensions
{
    /// <summary>
    /// Extensões para datas
    /// </summary>
    public static class DateExtensions
    {
        /// <summary>
        /// Retorna data com horas "23:59:59" (final do dia)
        /// </summary>
        /// <param name="date">Data a ser formatada</param>
        /// <returns></returns>
        public static DateTime ToEndOfDay(this DateTime date) =>
            date.Date.AddDays(1).AddMilliseconds(-1);
    }
}
