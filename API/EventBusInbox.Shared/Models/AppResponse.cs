using EventBusInbox.Shared.Extensions;
using FluentValidation.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;

namespace EventBusInbox.Shared.Models
{
    /// <summary>
    /// Classe de resposta da aplicação
    /// </summary>
    /// <typeparam name="T">Tipo dos dados de retorno</typeparam>
    public class AppResponse<T> where T : class
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public AppResponse() { }

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="status">Status</param>
        /// <param name="message">Mensagem de retorno</param>
        /// <param name="stackTrace">Caminho na aplicação</param>
        /// <param name="data">Dados retornados</param>
        private AppResponse(HttpStatusCode status, string? message = null, 
            string? stackTrace = null, IList<T>? data = null)
        {
            Status = status;
            Message = message;
            StackTrace = stackTrace;
            Data = data;
        }

        /// <summary>
        /// Status do processamento
        /// </summary>
        public HttpStatusCode Status { get; private set; }

        /// <summary>
        /// Mensagem de retorno
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Caminho na aplicação
        /// </summary>
        public string StackTrace { get; private set; }

        /// <summary>
        /// Dados retornados
        /// </summary>
        public IList<T> Data { get; private set; }

        /// <summary>
        /// Retorna se a resposta é de sucesso
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return Status.IsSuccess();
            }
        }

        /// <summary>
        /// Retorna um único objeto da lista de objetos retornados (sempre o primeiro)
        /// </summary>
        public T Object
        {
            get
            {
                if (Data is null || !Data.Any())
                    return null;

                if (Data.Count > 1)
                    return null;
                else
                    return Data.FirstOrDefault();
            }
        }

        /// <summary>
        /// Retorna JSON
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            JsonConvert.SerializeObject(this);

        /// <summary>
        /// Realiza cópia da resposta atual, porém, sem os objetos
        /// </summary>
        /// <typeparam name="Origin">Tipo do objeto de origem</typeparam>
        /// <param name="origin">Objeto a ser copiado</param>
        /// <param name="data">Dados a serem enviados</param>
        /// <returns></returns>
        public static AppResponse<T> Copy<Origin>(AppResponse<Origin> origin, List<T>? data = null) 
            where Origin : class =>
            new AppResponse<T>(origin.Status, origin.Message, origin.StackTrace, data);

        /// <summary>
        /// Retorna resposta de sucesso com mensagem
        /// </summary>
        /// <param name="message">Mensagem a ser retornada</param>
        /// <returns></returns>
        public static AppResponse<T> Success(string message) =>
            new AppResponse<T>(HttpStatusCode.OK, message);

        /// <summary>
        /// Retorna resposta de sucesso, com um único objeto
        /// </summary>
        /// <param name="obj">Objeto a ser retornado</param>
        /// <returns></returns>
        public static AppResponse<T> Success(T obj) =>
            new AppResponse<T>(HttpStatusCode.OK, null, null, new List<T>
            {
                obj
            });

        /// <summary>
        /// Retorna resposta de sucesso, com retorno de lista de objetos
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static AppResponse<T> Success(IList<T> lst) =>
            new AppResponse<T>(HttpStatusCode.OK, null, null, lst);

        /// <summary>
        /// Retorna resposta de erro interno, com base em uma exceção
        /// </summary>
        /// <param name="ex">Exceção</param>
        /// <returns></returns>
        public static AppResponse<T> Error(Exception ex) =>
            new AppResponse<T>(HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace);

        /// <summary>
        /// Retorna resposta customizada
        /// </summary>
        /// <param name="status">Status</param>
        /// <param name="message">Mensagem</param>
        /// <param name="stackTrace">Caminho do sistema</param>
        /// <param name="data">Dados a serem retornados</param>
        /// <returns></returns>
        public static AppResponse<T> Custom(HttpStatusCode status, string? message = null,
            string? stackTrace = null, IList<T>? data = null) =>
            new AppResponse<T>(status, message, stackTrace, data);

        /// <summary>
        /// Retorna resposta negativa com base em uma validação realizada, através do FluentValidation
        /// </summary>
        /// <param name="validation">Resultado de validação</param>
        /// <returns></returns>
        public static AppResponse<T> ValidationResponse(ValidationResult validation)
        {
            if (validation is null || validation.IsValid)
                return new AppResponse<T>(HttpStatusCode.OK, "Validated successfully!");

            List<string> validationErrors = new List<string>();

            foreach(var error in validation.Errors)
            {
                if (string.IsNullOrEmpty(error.PropertyName))
                    validationErrors.Add($"[{error.ErrorMessage}]");
                else
                    validationErrors.Add($"[{error.PropertyName}: {error.ErrorMessage}]");
            }
            
            var errorJoin =  string.Join(", ", validationErrors);
            var errorMessage = $"Invalid data! {errorJoin}";

            return new AppResponse<T>(HttpStatusCode.UnprocessableEntity, errorJoin);
        }
    }
}
