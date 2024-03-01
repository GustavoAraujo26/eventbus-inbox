import { ApiTaskResponse } from "../interfaces/api-response";
import SendEventbusMessageRequest from "../interfaces/requests/eventbus-sender/send-eventbus-message-request";
import { HttpService } from "./http-service";

export class EventBusSenderService extends HttpService {

    SendMessage(request: SendEventbusMessageRequest) {
        return this.post<ApiTaskResponse>('/v1/event-bus/sender', request);
    }
}