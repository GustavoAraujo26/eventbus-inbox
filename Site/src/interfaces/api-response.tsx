export default interface ApiResponse<T> {
    status: number,
    message: string,
    stackTrace: string,
    data: T[],
    isSuccess: boolean,
    object: T
}