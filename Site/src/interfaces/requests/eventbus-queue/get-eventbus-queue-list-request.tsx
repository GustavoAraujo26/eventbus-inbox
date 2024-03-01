export default interface GetEventBusQueueListRequest {
    nameMatch: string | null,
    descriptionMatch: string | null,
    status: number | null,
    page: number,
    pageSize: number,
    summarizeMessages: boolean
}