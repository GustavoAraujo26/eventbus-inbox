export default interface GetEventBusQueueListRequest {
    nameMatch: string,
    descriptionMatch: string,
    status: number,
    page: number,
    pageSize: number,
    summarizeMessages: boolean
}