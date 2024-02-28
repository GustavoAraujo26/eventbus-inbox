export default interface SendEventbusMessageRequest {
    requestId: string,
    createdAt: Date,
    type: string,
    content: string,
    queueId: string
}