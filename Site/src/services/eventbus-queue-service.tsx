import GetEventBusQueueRequest from "../interfaces/requests/eventbus-queue/get-eventbus-queue-request";
import { HttpService } from "./http-service";
import SaveEventbusQueueRequest from "../interfaces/requests/eventbus-queue/save-eventbus-queue-request";
import GetEventBusQueueListRequest from "../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";
import UpdateEventbusQueueStatusRequest from "../interfaces/requests/eventbus-queue/update-eventbus-queue-status-request";
import GetEventbusQueueResponse from "../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import { ApiResponse } from "../interfaces/api-response";
import AppTaskResponse from "../interfaces/app-task-response";
import axios, { AxiosError, AxiosResponse } from "axios";

export class EventBusQueueService extends HttpService {

    async GetQueue(request: GetEventBusQueueRequest) {
        let propertyName = 'Name';
        let propertyValue = request.name;
        if (request.id) {
            propertyName = 'Id';
            propertyValue = request.id;
        }

        const url = `/v1/event-bus/queue?${propertyName}=${propertyValue}&SummarizeMessages=${request.summarizeMessages}`;

        let response: AxiosResponse<ApiResponse<GetEventbusQueueResponse>, any> | null = null;
        let apiResponse: ApiResponse<GetEventbusQueueResponse> | null = null;
        
        try{
            response = await this.get<ApiResponse<GetEventbusQueueResponse>>(url);
            apiResponse = response.data;
        }
        catch(error){
            console.log(error);

            if (axios.isAxiosError(error)){
                const axiosError = error as AxiosError<ApiResponse<GetEventbusQueueResponse>, any>;
                apiResponse = axiosError.response!.data
            }
        }
        
        return apiResponse;
    }

    async DeleteQueue(id: string) {
        let response: AxiosResponse<ApiResponse<AppTaskResponse>, any> | null = null;
        let apiResponse: ApiResponse<AppTaskResponse> | null = null;
        
        try{
            response = await this.delete<ApiResponse<AppTaskResponse>>(`/v1/event-bus/queue?Id=${id}`);
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

    async SaveQueue(request: SaveEventbusQueueRequest) {
        let response: AxiosResponse<ApiResponse<AppTaskResponse>, any> | null = null;
        let apiResponse: ApiResponse<AppTaskResponse> | null = null;
        
        try{
            response = await this.post<ApiResponse<AppTaskResponse>>('/v1/event-bus/queue', request);
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

    async ListQueues(request: GetEventBusQueueListRequest) {
        let response: AxiosResponse<ApiResponse<GetEventbusQueueResponse>, any> | null = null;
        let apiResponse: ApiResponse<GetEventbusQueueResponse> | null = null;
        
        try{
            response = await this.post<ApiResponse<GetEventbusQueueResponse>>('/v1/event-bus/queue/list', request);
            apiResponse = response.data;
        }
        catch(error){
            console.log(error);

            if (axios.isAxiosError(error)){
                const axiosError = error as AxiosError<ApiResponse<GetEventbusQueueResponse>, any>;
                apiResponse = axiosError.response!.data
            }
        }
        
        return apiResponse;
    }

    async UpdateStatus(request: UpdateEventbusQueueStatusRequest) {
        let response: AxiosResponse<ApiResponse<AppTaskResponse>, any> | null = null;
        let apiResponse: ApiResponse<AppTaskResponse> | null = null;
        
        try{
            response = await this.get<ApiResponse<AppTaskResponse>>(`/v1/event-bus/queue/update/status?Id=${request.id}&Status=${request.status}`);
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