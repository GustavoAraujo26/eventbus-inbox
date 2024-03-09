import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import GetEventbusQueueResponse from "../../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import { EventBusQueueService } from "../../../services/eventbus-queue-service";
import GetEventBusQueueRequest from "../../../interfaces/requests/eventbus-queue/get-eventbus-queue-request";
import { closeBackdrop, showBackdrop } from "../app-backdrop-slice";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import { showSnackbar } from "../app-snackbar-slice";

interface QueueContainer {
    data: GetEventbusQueueResponse | null
}

export const fetchEventBusQueue = createAsyncThunk(
    'eventbusQueue/fetch',
    async (request: GetEventBusQueueRequest | null, { dispatch }) => {
        const queueService = new EventBusQueueService();

        if (request == null){
            const snackbarResponse: AppSnackbarResponse = {
                success: false,
                statusCode: 400,
                stackTrace: null,
                message: 'Inform the id or name of the queue!'
            }

            dispatch(showSnackbar(snackbarResponse));

            return null;
        }

        dispatch(showBackdrop());

        const apiResponse = await queueService.GetQueue(request);

        dispatch(closeBackdrop());

        if (apiResponse === null){
            return null;
        }

        if (apiResponse.isSuccess){
            return apiResponse.object;
        }
        else{
            const snackbarResponse: AppSnackbarResponse = {
                success: false,
                statusCode: apiResponse.status,
                stackTrace: apiResponse.stackTrace,
                message: apiResponse.message
            }

            dispatch(showSnackbar(snackbarResponse));

            return null;
        }
    }
);

const initialState: QueueContainer = {
    data: null
}

const eventBusQueueSlice = createSlice({
    name: 'eventbusQueue',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchEventBusQueue.fulfilled, (state, action) => {
            state.data = action.payload;
        })
    }
});

export default eventBusQueueSlice.reducer;