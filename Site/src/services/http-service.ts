import axios, { AxiosInstance } from "axios";

export class HttpService {
    http: AxiosInstance;

    constructor() {
        this.http = axios.create({
            baseURL: 'http://localhost:9000/api'
            // baseURL: 'http://localhost:44356/api'
        });
    }

    get<Response>(url: string, customHeaders: {} = {}) {
        return this.http.get<Response>(url);
    }

    post<Response>(url: string, data: {}, customHeaders: {} = {}) {
        return this.http.post<Response>(url, data);
    }

    put<Response>(url: string, data: {}, customHeaders: {} = {}) {
        return this.http.put<Response>(url, data);
    }

    delete<Response>(url: string, customHeaders: {} = {}) {
        return this.http.delete<Response>(url);
    }
}