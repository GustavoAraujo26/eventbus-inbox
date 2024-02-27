using Asp.Versioning;
using EventBusInbox.Api.Controllers.Base;
using EventBusInbox.Domain.Requests;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventBusInbox.Api.Controllers
{
    /// <summary>
    /// Controlador para envio de mensagens para o barramento de eventos
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/event-bus/sender")]
    [ApiController]
    [SwaggerTag("Remetente de mensagens para o barramento de eventos")]
    public class EventBusSenderController : BaseController
    {
        /// <summary>
        /// Envia mensagem para o barramento de eventos
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Mensagem a ser enviada</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AppResponse<AppTaskResponse>>> Send([FromServices] IMediator mediator,
            [FromBody] SendMessageRequest command) =>
            BuildResponse(await mediator.Send(command));
    }
}
