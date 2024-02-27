using Asp.Versioning;
using EventBusInbox.Api.Controllers.Base;
using EventBusInbox.Domain.Requests.EventBusReceivedMessage;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Domain.Responses.EventBusReceivedMessage;
using EventBusInbox.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventBusInbox.Api.Controllers
{
    /// <summary>
    /// Controlador para mensagens recebidas do barramento de eventos
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/event-bus/received-messages")]
    [ApiController]
    [SwaggerTag("Controle de mensagens recebidas do barramento de eventos")]
    public class EventBusReceivedMessageController : BaseController
    {
        /// <summary>
        /// Apaga mensagem
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Requisição para execução</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<AppResponse<AppTaskResponse>>> Delete([FromServices] IMediator mediator,
            [FromQuery] DeleteEventBusReceivedMessageRequest command) =>
            BuildResponse(await mediator.Send(command));

        /// <summary>
        /// Retorna mensagem
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Requisição para execução</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<AppResponse<GetEventBusReceivedMessageResponse>>> Get([FromServices] IMediator mediator,
            [FromQuery] GetEventBusReceivedMessageRequest command) =>
            BuildResponse(await mediator.Send(command));

        /// <summary>
        /// Retorna listagem de mensagens
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Requisição para execução</param>
        /// <returns></returns>
        [HttpPost]
        [Route("list")]
        public async Task<ActionResult<AppResponse<GetEventBusReceivedMessageListResponse>>> List([FromServices] IMediator mediator,
            [FromBody] GetEventBusReceivedMessageListRequest command) =>
            BuildResponse(await mediator.Send(command));

        /// <summary>
        /// Retorna listagem de mensagens habilitadas para processamento
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Requisição para execução</param>
        /// <returns></returns>
        [HttpPost]
        [Route("to-process")]
        public async Task<ActionResult<AppResponse<GetEventBusReceivedMessageToProcessResponse>>> GetToProcess([FromServices] IMediator mediator,
            [FromBody] GetEventBusReceivedMessageToProcessRequest command) =>
            BuildResponse(await mediator.Send(command));

        /// <summary>
        /// Reabilita mensagem para ser processada
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Requisição para execução</param>
        /// <returns></returns>
        [HttpGet]
        [Route("reactivate")]
        public async Task<ActionResult<AppResponse<AppTaskResponse>>> Reactivate([FromServices] IMediator mediator,
            [FromQuery] ReactivateEventBusReceivedMessageRequest command) =>
            BuildResponse(await mediator.Send(command));

        /// <summary>
        /// Salva mensagem
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Requisição para execução</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AppResponse<AppTaskResponse>>> Save([FromServices] IMediator mediator,
            [FromBody] SaveEventBusReceivedMessageRequest command) =>
            BuildResponse(await mediator.Send(command));

        /// <summary>
        /// Atualiza status
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Requisição para execução</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<AppResponse<AppTaskResponse>>> UpdateStatus([FromServices] IMediator mediator,
            [FromBody] UpdateEventBusReceivedMessageStatusRequest command) =>
            BuildResponse(await mediator.Send(command));
    }
}
