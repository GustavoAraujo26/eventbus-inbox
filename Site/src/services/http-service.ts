import axios, { AxiosInstance } from "axios";

export class HttpService {
    http: AxiosInstance;

    constructor() {
        console.log(process.env.REACT_APP_API_URL);
        this.http = axios.create({
            baseURL: process.env.REACT_APP_API_URL
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