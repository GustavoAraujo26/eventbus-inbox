export default interface AppSnackbarResponse {
    success: boolean,
    message: string,
    stackTrace?: string,
    statusCode?: number
}