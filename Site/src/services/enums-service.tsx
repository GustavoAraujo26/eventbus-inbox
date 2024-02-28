import ApiResponse from "../interfaces/api-response";
import EnumData from "../interfaces/enum-data";
import { HttpService } from "./http-service";

export class EnumsService extends HttpService {
    
    async ListQueueStatus() {
        return this.get<EnumData>('/v2/enums/queue-status');
    }

    async ListMessageStatus() {
        return this.get<EnumData>('/v2/enums/event-bus-message-status');
    }
}