import axios, { AxiosInstance } from "axios";
import ApiResponse from "../interfaces/api-response";

export class HttpService {
    http: AxiosInstance;

    constructor(){
        this.http = axios.create({
            baseURL: process.env.API_URL
        });
    }

    get defaultHeaders() {
        return {
            'Content-Type': 'application/json'
        }
    }

    async request<Response>(method: string, url: string, data: {} | null = null, customHeaders: {} = {}){
        const headers = { ...this.defaultHeaders, ...customHeaders };
        const source = axios.CancelToken.source();

        let config = {
            method, 
            url, 
            headers, 
            data,
            cancelToken: source.token
        };

        return {
            request: this.http<any, ApiResponse<Response>>(config),
            cancel: source.cancel
        }
    }

    get<Response>(url: string, customHeaders: {} = {}){
        return this.request<Response>('get', url, null, customHeaders);
    }

    post<Response>(url: string, data: {}, customHeaders: {} = {}){
        return this.request<Response>('post', url, data, customHeaders);
    }

    put<Response>(url: string, data: {}, customHeaders: {} = {}){
        return this.request<Response>('put', url, data, customHeaders);
    }

    delete<Response>(url: string, customHeaders: {} = {}){
        return this.request<Response>('delete', url, null, customHeaders);
    }
}