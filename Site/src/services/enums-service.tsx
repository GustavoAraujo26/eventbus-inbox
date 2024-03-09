import axios, { Axios, AxiosError, AxiosResponse } from "axios";
import { ApiResponse } from "../interfaces/api-response";
import EnumData from "../interfaces/enum-data";
import { HttpService } from "./http-service";

export class EnumsService extends HttpService {

    async ListQueueStatus() {
        return await get(this.http, '/v2/enums/queue-status');
    }

    async ListMessageStatus() {
        return await get(this.http, '/v2/enums/event-bus-message-status');
    }

    async ListHttpStatusCode() {
        return await get(this.http, '/v2/enums/http-status-code');
    }
}

const get = async (http: Axios, url: string) => {
    let response: AxiosResponse<ApiResponse<EnumData>, any> | null = null;
    let apiResponse: ApiResponse<EnumData> | null = null;

    try {
        response = await http.get<ApiResponse<EnumData>>(url);
        apiResponse = response.data;
    }
    catch (error) {
        console.log(error);

        if (axios.isAxiosError(error)) {
            const axiosError = error as AxiosError<ApiResponse<EnumData>, any>;
            apiResponse = axiosError.response!.data
        }
    }

    return apiResponse;
}