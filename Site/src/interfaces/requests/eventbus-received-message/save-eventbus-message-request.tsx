export default interface SaveEventbusMessageRequest {
    requestId: string,
    createdAt: Date,
    type: string,
    content: string,
    queueId: string
}