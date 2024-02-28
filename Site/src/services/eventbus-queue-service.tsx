import GetEventBusQueueRequest from "../interfaces/requests/eventbus-queue/get-eventbus-queue-request";
import { HttpService } from "./http-service";
import SaveEventbusQueueRequest from "../interfaces/requests/eventbus-queue/save-eventbus-queue-request";
import GetEventBusQueueListRequest from "../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";
import UpdateEventbusQueueStatusRequest from "../interfaces/requests/eventbus-queue/update-eventbus-queue-status-request";
import GetEventbusQueueResponse from "../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import AppTaskResponse from "../interfaces/app-task-response";
import GetEventbusMessageListResponse from "../interfaces/responses/eventbus-received-message/get-eventbus-message-list-response";

export class EventBusQueueService extends HttpService {
    
    async GetQueue(request: GetEventBusQueueRequest) {
        let propertyName = 'Name';
        if (request.id){
            propertyName = 'Id';
        }

        const url = `/v1/event-bus/queue?${propertyName}&SummarizeMessages=${request.summarizeMessages}`;

        return this.get<GetEventbusQueueResponse>(url);
    }

    async DeleteQueue(id: string) {
        const url = `/v1/event-bus/queue?Id=${id}`;

        return this.delete<AppTaskResponse>(url);
    }

    async SaveQueue(request: SaveEventbusQueueRequest) {
        return this.post<AppTaskResponse>('/v1/event-bus/queue', request);
    }

    async ListQueues(request: GetEventBusQueueListRequest){
        return this.post<GetEventbusMessageListResponse>('/v1/event-bus/queue/list', request);
    }

    async UpdateStatus(request: UpdateEventbusQueueStatusRequest){
        const url = `/v1/event-bus/queue/update/status?Id=${request.id}&Status=${request.status}`;

        return this.get<AppTaskResponse>(url);
    }
}