using System.Net;

namespace EventBusInbox.Shared.Extensions
{
    /// <summary>
    /// Extensões para código de status HTTP
    /// </summary>
    public static class HttpStatusCodeExtensions
    {
        /// <summary>
        /// Verifica se um status HTTP é relacionado a sucesso
        /// </summary>
        /// <param name="statusCode">Código do status HTTP</param>
        /// <returns></returns>
        public static bool IsSuccess(this HttpStatusCode statusCode) =>
            (int)statusCode >= 200 && (int)statusCode <= 299;

        /// <summary>
        /// Verifica se um status HTTP é relacionado a falha temporária
        /// </summary>
        /// <param name="statusCode">Código do status HTTP</param>
        /// <returns></returns>
        public static bool IsTemporaryFailure(this HttpStatusCode statusCode) =>
            (int)statusCode >= 500 && (int)statusCode <= 599;

        /// <summary>
        /// Verifica se um status HTTP é relacionado a falha permanente
        /// </summary>
        /// <param name="statusCode">Código do status HTTP</param>
        /// <returns></returns>
        public static bool IsPermanentFailure(this HttpStatusCode statusCode) =>
            (int)statusCode >= 400 && (int)statusCode <= 499;
    }
}
