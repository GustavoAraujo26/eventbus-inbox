import Period from "../../period";

export default interface GetEventbusMessageListRequest {
    queueId: string | null,
    creationDateSearch: Period | null,
    updateDateSearch: Period | null,
    statusToSearch: number[] | null,
    typeMatch: string | null,
    page: number,
    pageSize: number
}