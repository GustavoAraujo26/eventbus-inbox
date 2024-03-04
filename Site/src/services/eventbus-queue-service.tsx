import GetEventBusQueueRequest from "../interfaces/requests/eventbus-queue/get-eventbus-queue-request";
import { HttpService } from "./http-service";
import SaveEventbusQueueRequest from "../interfaces/requests/eventbus-queue/save-eventbus-queue-request";
import GetEventBusQueueListRequest from "../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";
import UpdateEventbusQueueStatusRequest from "../interfaces/requests/eventbus-queue/update-eventbus-queue-status-request";
import GetEventbusQueueResponse from "../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import { ApiResponse } from "../interfaces/api-response";
import AppTaskResponse from "../interfaces/app-task-response";

export class EventBusQueueService extends HttpService {

    GetQueue(request: GetEventBusQueueRequest) {
        let propertyName = 'Name';
        let propertyValue = request.name;
        if (request.id) {
            propertyName = 'Id';
            propertyValue = request.id;
        }

        const url = `/v1/event-bus/queue?${propertyName}=${propertyValue}&SummarizeMessages=${request.summarizeMessages}`;

        return this.get<ApiResponse<GetEventbusQueueResponse>>(url);
    }

    DeleteQueue(id: string) {
        const url = `/v1/event-bus/queue?Id=${id}`;

        return this.delete<ApiResponse<AppTaskResponse>>(url);
    }

    SaveQueue(request: SaveEventbusQueueRequest) {
        return this.post<ApiResponse<AppTaskResponse>>('/v1/event-bus/queue', request);
    }

    ListQueues(request: GetEventBusQueueListRequest) {
        return this.post<ApiResponse<GetEventbusQueueResponse>>('/v1/event-bus/queue/list', request);
    }

    UpdateStatus(request: UpdateEventbusQueueStatusRequest) {
        const url = `/v1/event-bus/queue/update/status?Id=${request.id}&Status=${request.status}`;

        return this.get<ApiResponse<AppTaskResponse>>(url);
    }
}