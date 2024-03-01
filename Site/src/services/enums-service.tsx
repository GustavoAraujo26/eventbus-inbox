import { ApiEnumData } from "../interfaces/api-response";
import { HttpService } from "./http-service";

export class EnumsService extends HttpService {

    ListQueueStatus() {
        return this.get<ApiEnumData>('/v2/enums/queue-status');
    }

    ListMessageStatus() {
        return this.get<ApiEnumData>('/v2/enums/event-bus-message-status');
    }
}