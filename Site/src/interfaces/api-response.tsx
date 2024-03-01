import AppTaskResponse from "./app-task-response";
import EnumData from "./enum-data";
import GetEventbusQueueResponse from "./responses/eventbus-queue/get-eventbus-queue-response";
import GetEventbusMessageListResponse from "./responses/eventbus-received-message/get-eventbus-message-list-response"
import GetEventbusMessageResponse from "./responses/eventbus-received-message/get-eventbus-message-response";

interface BasicApiResponse {
    status: number,
    message: string,
    stackTrace: string,
    isSuccess: boolean
}

export interface ApiEventBusQueueResponse extends BasicApiResponse {
    data: GetEventbusQueueResponse[],
    object: GetEventbusQueueResponse
}

export interface ApiEventBusMessageListResponse extends BasicApiResponse {
    data: GetEventbusMessageListResponse[],
    object: GetEventbusMessageListResponse
}

export interface ApiEventBusMessageResponse extends BasicApiResponse {
    data: GetEventbusMessageResponse[],
    object: GetEventbusMessageResponse
}

export interface ApiTaskResponse extends BasicApiResponse {
    data: AppTaskResponse[],
    object: AppTaskResponse
}

export interface ApiEnumData extends BasicApiResponse {
    data: EnumData[],
    object: EnumData
}