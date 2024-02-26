using EventBusInbox.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            return StatusCode((int)appResponse.Status, JsonConvert.SerializeObject(appResponse));
        }
    }
}
