import AppTaskResponse from "./app-task-response";
import EnumData from "./enum-data";
import GetEventbusQueueResponse from "./responses/eventbus-queue/get-eventbus-queue-response";
import GetEventbusMessageListResponse from "./responses/eventbus-received-message/get-eventbus-message-list-response"
import GetEventbusMessageResponse from "./responses/eventbus-received-message/get-eventbus-message-response";

export interface ApiResponse<T> {
    status: number,
    message: string,
    stackTrace: string,
    isSuccess: boolean,
    data: T[],
    object: T
}