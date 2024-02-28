import EnumData from "../../enum-data";
import GetEventbusQueueResponse from "../eventbus-queue/get-eventbus-queue-response";
import ProcessingHistoryLineResponse from "./processing-history-line-response";

export default interface GetEventbusMessageResponse {
    requestId: string,
    createdAt: Date,
    type: string,
    content: string,
    queue: GetEventbusQueueResponse,
    status: EnumData,
    processingAttempts: number,
    processingHistory: ProcessingHistoryLineResponse[]
}