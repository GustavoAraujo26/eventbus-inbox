export default interface AppSnackbarResponse {
    success: boolean,
    message: string,
    stackTrace?: string | null,
    statusCode?: number | null
}