using EventBusInbox.Shared.Models;
using MongoDB.Driver;

namespace EventBusInbox.Domain.Repositories.Base
{
    /// <summary>
    /// Inteface básica para os repositórios
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Salva no banco de dados o item
        /// </summary>
        /// <param name="obj">Item a ser salvo</param>
        /// <returns></returns>
        Task<AppResponse<object>> Save(T obj);

        /// <summary>
        /// Retorna um item com base no seu ID
        /// </summary>
        /// <param name="id">Identificador do item</param>
        /// <returns></returns>
        Task<T> GetById(Guid id);

        /// <summary>
        /// Apaga um item com base no seu ID
        /// </summary>
        /// <param name="id">Identificador do item</param>
        /// <returns></returns>
        Task<AppResponse<object>> Delete(Guid id);
    }
}
