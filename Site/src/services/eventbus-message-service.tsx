import { ApiEventBusMessageListResponse, ApiEventBusMessageResponse, ApiTaskResponse } from "../interfaces/api-response";
import GetEventbusMessageListRequest from "../interfaces/requests/eventbus-received-message/get-eventbus-message-list-request";
import SaveEventbusMessageRequest from "../interfaces/requests/eventbus-received-message/save-eventbus-message-request";
import UpdateEventbusMessageStatusRequest from "../interfaces/requests/eventbus-received-message/update-eventbus-message-status-request";
import { HttpService } from "./http-service";

export class EventBusMessageService extends HttpService {
    DeleteMessage(id: string) {
        return this.delete<ApiTaskResponse>(`/v1/event-bus/received-messages?RequestId=${id}`);
    }

    GetMessage(id: string) {
        return this.get<ApiEventBusMessageResponse>(`/v1/event-bus/received-messages?RequestId=${id}`);
    }

    SaveMessage(request: SaveEventbusMessageRequest) {
        return this.post<ApiTaskResponse>('/v1/event-bus/received-messages', request);
    }

    UpdateMessageStatus(request: UpdateEventbusMessageStatusRequest) {
        return this.put<ApiTaskResponse>('/v1/event-bus/received-messages', request);
    }

    ListMessage(request: GetEventbusMessageListRequest) {
        return this.post<ApiEventBusMessageListResponse>('/v1/event-bus/received-messages', request);
    }

    ReactivateMessage(id: string) {
        return this.get<ApiTaskResponse>(`/v1/event-bus/received-messages?RequestId=${id}`);
    }
}