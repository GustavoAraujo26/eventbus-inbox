using EventBusInbox.Api.Controllers.Base;
using EventBusInbox.Domain.Enums;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventBusInbox.Api.Controllers
{
    /// <summary>
    /// Controlador responsável por disponibilizar as opções de enumeradores do sistema
    /// </summary>
    [Route("api/enums")]
    [ApiController]
    public class EnumsController : BaseController
    {
        /// <summary>
        /// Lista todas as opções do enumerador de status de fila
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("queue-status")]
        public ActionResult<AppResponse<EnumData>> ListQueueStatus()
        {
            var enumValueList = QueueStatus.Enabled.List<QueueStatus>();
            return BuildResponse(AppResponse<EnumData>.Success(enumValueList));
        }

        /// <summary>
        /// Lista todas as opções do enumerador de status da mensagem do barramento de eventos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("event-bus-message-status")]
        public ActionResult<AppResponse<EnumData>> ListEventBusMessageStatus()
        {
            var enumValueList = EventBusMessageStatus.Pending.List<EventBusMessageStatus>();
            return BuildResponse(AppResponse<EnumData>.Success(enumValueList));
        }
    }
}
