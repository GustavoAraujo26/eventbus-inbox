import { ApiResponse } from "../interfaces/api-response";
import EnumData from "../interfaces/enum-data";
import { HttpService } from "./http-service";

export class EnumsService extends HttpService {

    ListQueueStatus() {
        return this.get<ApiResponse<EnumData>>('/v2/enums/queue-status');
    }

    ListMessageStatus() {
        return this.get<ApiResponse<EnumData>>('/v2/enums/event-bus-message-status');
    }

    ListHttpStatusCode() {
        return this.get<ApiResponse<EnumData>>('/v2/enums/http-status-code');
    }
}