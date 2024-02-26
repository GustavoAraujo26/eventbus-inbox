using EventBusInbox.Shared.Models;
using System.ComponentModel;

namespace EventBusInbox.TypeConverters.Extensions
{
    /// <summary>
    /// Extensões para enumeradores
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Retorna o valor do atributo de descrição do enumerador
        /// </summary>
        /// <param name="enumValue">Valor do enumerador</param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(description);

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs is not null && attrs.Length > 0) 
                    description = ((DescriptionAttribute)attrs[0]).Description;
            }

            return description;
        }

        /// <summary>
        /// Retorna container de dados do enuemrador
        /// </summary>
        /// <param name="enumValue">Valor do enumerador</param>
        /// <returns></returns>
        public static EnumData GetData(this Enum enumValue) =>
            new EnumData((int)(object)enumValue, enumValue.ToString(), enumValue.GetDescription());

        /// <summary>
        /// Lista todos os valores do enumerador
        /// </summary>
        /// <typeparam name="T">Tipo do enumerador</typeparam>
        /// <param name="enumValue">Valor do enumerador</param>
        /// <returns></returns>
        public static List<EnumData> List<T>(this Enum enumValue) where T : Enum =>
            Enum.GetValues(typeof(T)).Cast<T>().Select(x => x.GetData()).ToList();
    }
}
