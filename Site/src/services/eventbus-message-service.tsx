import axios, { AxiosError, AxiosResponse } from "axios";
import { ApiResponse } from "../interfaces/api-response";
import AppTaskResponse from "../interfaces/app-task-response";
import GetEventbusMessageListRequest from "../interfaces/requests/eventbus-received-message/get-eventbus-message-list-request";
import SaveEventbusMessageRequest from "../interfaces/requests/eventbus-received-message/save-eventbus-message-request";
import UpdateEventbusMessageStatusRequest from "../interfaces/requests/eventbus-received-message/update-eventbus-message-status-request";
import GetEventbusMessageListResponse from "../interfaces/responses/eventbus-received-message/get-eventbus-message-list-response";
import GetEventbusMessageResponse from "../interfaces/responses/eventbus-received-message/get-eventbus-message-response";
import { HttpService } from "./http-service";

export class EventBusMessageService extends HttpService {
    DeleteMessage(id: string) {
        return this.delete<ApiResponse<AppTaskResponse>>(`/v1/event-bus/received-messages?RequestId=${id}`);
    }

    GetMessage(id: string) {
        return this.get<ApiResponse<GetEventbusMessageResponse>>(`/v1/event-bus/received-messages?RequestId=${id}`);
    }

    SaveMessage(request: SaveEventbusMessageRequest) {
        return this.post<ApiResponse<AppTaskResponse>>('/v1/event-bus/received-messages', request);
    }

    UpdateMessageStatus(request: UpdateEventbusMessageStatusRequest) {
        return this.put<ApiResponse<AppTaskResponse>>('/v1/event-bus/received-messages', request);
    }

    async ListMessage(request: GetEventbusMessageListRequest) {
        let response: AxiosResponse<ApiResponse<GetEventbusMessageListResponse>, any> | null = null;
        let apiResponse: ApiResponse<GetEventbusMessageListResponse> | null = null;
        
        try{
            response = await this.post<ApiResponse<GetEventbusMessageListResponse>>('/v1/event-bus/received-messages/list', request);
            apiResponse = response.data;
        }
        catch(error){
            console.log(error);

            if (axios.isAxiosError(error)){
                const axiosError = error as AxiosError<ApiResponse<GetEventbusMessageListResponse>, any>;
                apiResponse = axiosError.response!.data
            }
        }
        
        return apiResponse;
    }

    ReactivateMessage(id: string) {
        return this.get<ApiResponse<AppTaskResponse>>(`/v1/event-bus/received-messages/reactivate?RequestId=${id}`);
    }
}