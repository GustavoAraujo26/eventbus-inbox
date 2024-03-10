import axios, { AxiosError, AxiosResponse } from "axios";
import { ApiResponse } from "../interfaces/api-response";
import AppTaskResponse from "../interfaces/app-task-response";
import SendEventbusMessageRequest from "../interfaces/requests/eventbus-sender/send-eventbus-message-request";
import { HttpService } from "./http-service";

export class EventBusSenderService extends HttpService {

    async SendMessage(request: SendEventbusMessageRequest) {
        let response: AxiosResponse<ApiResponse<AppTaskResponse>, any> | null = null;
        let apiResponse: ApiResponse<AppTaskResponse> | null = null;
        
        try{
            response = await this.post<ApiResponse<AppTaskResponse>>('/v1/event-bus/sender', request);
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