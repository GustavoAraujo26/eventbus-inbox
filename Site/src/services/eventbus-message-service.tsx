import AppTaskResponse from "../interfaces/app-task-response";
import GetEventbusMessageListRequest from "../interfaces/requests/eventbus-received-message/get-eventbus-message-list-request";
import SaveEventbusMessageRequest from "../interfaces/requests/eventbus-received-message/save-eventbus-message-request";
import UpdateEventbusMessageStatusRequest from "../interfaces/requests/eventbus-received-message/update-eventbus-message-status-request";
import GetEventbusQueueResponse from "../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import GetEventbusMessageListResponse from "../interfaces/responses/eventbus-received-message/get-eventbus-message-list-response";
import GetEventbusMessageResponse from "../interfaces/responses/eventbus-received-message/get-eventbus-message-response";
import { HttpService } from "./http-service";

export class EventBusMessageService extends HttpService {
    async DeleteMessage(id: string) {
        return this.delete(`/v1/event-bus/received-messages?RequestId=${id}`);
    }

    async GetMessage(id: string) {
        return this.get<GetEventbusMessageResponse>(`/v1/event-bus/received-messages?RequestId=${id}`);
    }

    async SaveMessage(request: SaveEventbusMessageRequest) {
        return this.post<AppTaskResponse>('/v1/event-bus/received-messages', request);
    }

    async UpdateMessageStatus(request: UpdateEventbusMessageStatusRequest) {
        return this.put<AppTaskResponse>('/v1/event-bus/received-messages', request);
    }

    async ListMessage(request: GetEventbusMessageListRequest) {
        return this.post<GetEventbusMessageListResponse>('/v1/event-bus/received-messages', request);
    }

    async ReactivateMessage(id: string) {
        return this.get<AppTaskResponse>(`/v1/event-bus/received-messages?RequestId=${id}`);
    }
}