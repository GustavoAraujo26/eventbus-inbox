import EnumData from "../../enum-data";
import SummarizeEventbusMessageResponse from "../eventbus-received-message/summarize-eventbus-message-response";

export default interface GetEventbusQueueResponse {
    id: string,
    name: string,
    description: string,
    status: EnumData,
    processingAttempts: number,
    messagesSummarization: SummarizeEventbusMessageResponse[]
}