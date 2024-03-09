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
    async DeleteMessage(id: string) {
        let response: AxiosResponse<ApiResponse<AppTaskResponse>, any> | null = null;
        let apiResponse: ApiResponse<AppTaskResponse> | null = null;
        
        try{
            response = await this.delete<ApiResponse<AppTaskResponse>>(`/v1/event-bus/received-messages?RequestId=${id}`);
            apiResponse = response.data;
        }
        catch(error){
            console.log(error);

            if (axios.isAxiosError(error)){
                const axiosError = error as AxiosError<ApiResponse<AppTaskResponse>, any>;
                apiResponse = axiosError.response!.data
            }
        }
        
        return apiResponse;
    }

    async GetMessage(id: string) {
        let response: AxiosResponse<ApiResponse<GetEventbusMessageResponse>, any> | null = null;
        let apiResponse: ApiResponse<GetEventbusMessageResponse> | null = null;
        
        try{
            response = await this.get<ApiResponse<GetEventbusMessageResponse>>(`/v1/event-bus/received-messages?RequestId=${id}`);
            apiResponse = response.data;
        }
        catch(error){
            console.log(error);

            if (axios.isAxiosError(error)){
                const axiosError = error as AxiosError<ApiResponse<GetEventbusMessageResponse>, any>;
                apiResponse = axiosError.response!.data
            }
        }
        
        return apiResponse;
    }

    async SaveMessage(request: SaveEventbusMessageRequest) {
        let response: AxiosResponse<ApiResponse<AppTaskResponse>, any> | null = null;
        let apiResponse: ApiResponse<AppTaskResponse> | null = null;
        
        try{
            response = await this.post<ApiResponse<AppTaskResponse>>('/v1/event-bus/received-messages', request);
            apiResponse = response.data;
        }
        catch(error){
            console.log(error);

            if (axios.isAxiosError(error)){
                const axiosError = error as AxiosError<ApiResponse<AppTaskResponse>, any>;
                apiResponse = axiosError.response!.data
            }
        }
        
        return apiResponse;
    }

    async UpdateMessageStatus(request: UpdateEventbusMessageStatusRequest) {
        let response: AxiosResponse<ApiResponse<AppTaskResponse>, any> | null = null;
        let apiResponse: ApiResponse<AppTaskResponse> | null = null;
        
        try{
            response = await this.put<ApiResponse<AppTaskResponse>>('/v1/event-bus/received-messages', request);
            apiResponse = response.data;
        }
        catch(error){
            console.log(error);

            if (axios.isAxiosError(error)){
                const axiosError = error as AxiosError<ApiResponse<AppTaskResponse>, any>;
                apiResponse = axiosError.response!.data
            }
        }
        
        return apiResponse;
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

    async ReactivateMessage(id: string) {
        let response: AxiosResponse<ApiResponse<AppTaskResponse>, any> | null = null;
        let apiResponse: ApiResponse<AppTaskResponse> | null = null;
        
        try{
            response = await this.get<ApiResponse<AppTaskResponse>>(`/v1/event-bus/received-messages/reactivate?RequestId=${id}`);
            apiResponse = response.data;
        }
        catch(error){
            console.log(error);

            if (axios.isAxiosError(error)){
                const axiosError = error as AxiosError<ApiResponse<AppTaskResponse>, any>;
                apiResponse = axiosError.response!.data
            }
        }
        
        return apiResponse;
    }
}