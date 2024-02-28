import EnumData from "../../enum-data";

export default interface SummarizeEventbusMessageResponse {
    status: EnumData,
    messageCount: number
}