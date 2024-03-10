import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import GetEventbusQueueResponse from "../../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import { showBackdrop, closeBackdrop } from "../app-backdrop-slice";
import { showSnackbar } from "../app-snackbar-slice";
import { EventBusQueueService } from "../../../services/eventbus-queue-service";
import GetEventBusQueueListRequest from "../../../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";

interface QueueContainer {
    data: GetEventbusQueueResponse[]
}

export const fetchEventBusQueueList = createAsyncThunk(
    'eventbusQueueList/fetch',
    async (data: GetEventBusQueueListRequest | null, { dispatch }) => {
        const queueService = new EventBusQueueService();

        dispatch(showBackdrop());

        let request: GetEventBusQueueListRequest = {
            page: 1,
            pageSize: 1000000,
            nameMatch: null,
            descriptionMatch: null,
            status: null,
            summarizeMessages: false
        };
        if (data !== null){
            request = data;
        }

        const apiResponse = await queueService.ListQueues(request);

        dispatch(closeBackdrop());

        if (apiResponse === null){
            return [];
        }

        if (apiResponse.isSuccess){
            return apiResponse.data;
        }
        else{
            const snackbarResponse: AppSnackbarResponse = {
                success: false,
                statusCode: apiResponse.status,
                stackTrace: apiResponse.stackTrace,
                message: apiResponse.message
            }

            dispatch(showSnackbar(snackbarResponse));

            return [];
        }
    }
);

const initialState: QueueContainer = {
    data: []
}

const eventBusQueueListSlice = createSlice({
    name: 'eventbusQueueList',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchEventBusQueueList.fulfilled, (state, action) => {
            state.data = action.payload;
        })
    }
});

export default eventBusQueueListSlice.reducer;