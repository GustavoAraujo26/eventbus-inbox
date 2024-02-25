using EventBusInbox.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBusInbox.Api.Controllers.Base
{
    /// <summary>
    /// Controlador base
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Constrói resposta para método do controlador
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de retorno</typeparam>
        /// <param name="appResponse">Resposta da aplicação</param>
        /// <returns></returns>
        public ActionResult BuildResponse<T>(AppResponse<T> appResponse) where T : class
        {
            return StatusCode((int)appResponse.Status, appResponse);
        }
    }
}
