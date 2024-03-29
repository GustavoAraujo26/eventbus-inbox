﻿using Asp.Versioning;
using EventBusInbox.Api.Controllers.Base;
using EventBusInbox.Domain.Enums;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace EventBusInbox.Api.Controllers
{
    /// <summary>
    /// Controlador responsável por disponibilizar as opções de enumeradores do sistema
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/enums")]
    [ApiController]
    [SwaggerTag("Enumeradores utilizados no sistema")]
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

        /// <summary>
        /// Lista todas as opções do enumerador de código de status HTTP
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("http-status-code")]
        public ActionResult<AppResponse<EnumData>> ListHttpStatus()
        {
            var enumValueList = HttpStatusCode.OK.List<HttpStatusCode>();
            return BuildResponse(AppResponse<EnumData>.Success(enumValueList));
        }
    }
}
