import EnumData from "../../enum-data";

export default interface ProcessingHistoryLineResponse {
    occurredAt: Date,
    status: EnumData,
    resultMessage: string
}