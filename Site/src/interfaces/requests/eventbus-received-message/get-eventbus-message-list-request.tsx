import Period from "../../period";

export default interface GetEventbusMessageListRequest {
    queueId: string,
    creationDateSearch: Period,
    updateDateSearch: Period,
    statusToSearch: number[],
    typeMatch: string,
    page: number,
    pageSize: number
}