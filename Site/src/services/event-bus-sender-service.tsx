import AppTaskResponse from "../interfaces/app-task-response";
import SendEventbusMessageRequest from "../interfaces/requests/eventbus-sender/send-eventbus-message-request";
import { HttpService } from "./http-service";

export class EventBusSenderService extends HttpService {
    
    async SendMessage(request: SendEventbusMessageRequest){
        return this.post<AppTaskResponse>('/v1/event-bus/sender', request);
    }
}