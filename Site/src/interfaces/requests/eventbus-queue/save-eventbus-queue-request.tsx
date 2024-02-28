export default interface SaveEventbusQueueRequest {
    id: string,
    name: string,
    description: string,
    status: number,
    processingAttempts: number
}