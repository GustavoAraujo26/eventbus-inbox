using EventBusInbox.Api.Controllers.Base;
using EventBusInbox.Domain.Requests;
using EventBusInbox.Domain.Responses;
using EventBusInbox.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventBusInbox.Api.Controllers
{
    [Route("api/event-bus/sender")]
    [ApiController]
    public class EventBusSenderController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<AppResponse<AppTaskResponse>>> Send([FromServices] IMediator mediator,
            [FromBody] SendMessageRequest command) =>
            BuildResponse(await mediator.Send(command));
    }
}
