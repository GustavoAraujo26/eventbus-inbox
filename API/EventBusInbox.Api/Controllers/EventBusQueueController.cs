using EventBusInbox.Api.Controllers.Base;
using EventBusInbox.Domain.Requests.EventBusQueues;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Domain.Responses.EventBusQueues;
using EventBusInbox.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventBusInbox.Api.Controllers
{
    /// <summary>
    /// Controlador de fila do barramento de eventos
    /// </summary>
    [Route("api/event-bus/queue")]
    [ApiController]
    public class EventBusQueueController : BaseController
    {
        /// <summary>
        /// Endpoint para retorno de uma fila específica
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Comando para execução do método</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<AppResponse<GetEventBusQueueResponse>>> Get([FromServices] IMediator mediator, 
            [FromQuery] GetEventBusQueueRequest command) =>
            BuildResponse(await mediator.Send(command));

        /// <summary>
        /// Endpoint para retorno de uma lista de filas
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Comando para execução do método</param>
        /// <returns></returns>
        [HttpPost]
        [Route("list")]
        public async Task<ActionResult<AppResponse<GetEventBusQueueResponse>>> List([FromServices] IMediator mediator,
            [FromBody] GetEventBusQueueListRequest command) =>
            BuildResponse(await mediator.Send(command));

        /// <summary>
        /// Endpoint para apagar uma fila específica
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Comando para execução do método</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<AppResponse<AppTaskResponse>>> Delete([FromServices] IMediator mediator,
            [FromQuery] DeleteEventBusQueueRequest command) => 
            BuildResponse(await mediator.Send(command));

        /// <summary>
        /// Endpoint para salvar uma fila específica
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Comando para execução do método</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AppResponse<AppTaskResponse>>> Save([FromServices] IMediator mediator,
            [FromBody] SaveEventBusQueueRequest command) => 
            BuildResponse(await mediator.Send(command));

        /// <summary>
        /// Endpoint para atualizar o status de uma fila específica
        /// </summary>
        /// <param name="mediator">Interface do mediator</param>
        /// <param name="command">Comando para execução do método</param>
        /// <returns></returns>
        [HttpGet]
        [Route("status")]
        public async Task<ActionResult<AppResponse<AppTaskResponse>>> UpdateSatus([FromServices] IMediator mediator,
            [FromQuery] UpdateEventBusQueueStatusRequest command) => 
            BuildResponse(await mediator.Send(command));
    }
}
