import EnumData from "../../enum-data";
import GetEventbusQueueResponse from "../eventbus-queue/get-eventbus-queue-response";
import ProcessingHistoryLineResponse from "./processing-history-line-response";

export default interface GetEventbusMessageListResponse {
    requestId: string,
    createdAt: Date,
    type: string,
    queue: GetEventbusQueueResponse,
    status: EnumData,
    processingAttempts: number,
    lastUpdate: ProcessingHistoryLineResponse
}