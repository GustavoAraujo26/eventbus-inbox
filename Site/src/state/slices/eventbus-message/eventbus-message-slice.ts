import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import GetEventbusMessageResponse from "../../../interfaces/responses/eventbus-received-message/get-eventbus-message-response";
import { closeBackdrop, showBackdrop } from "../app-backdrop-slice";
import { EventBusMessageService } from "../../../services/eventbus-message-service";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import { showSnackbar } from "../app-snackbar-slice";

interface MessageData {
    data: GetEventbusMessageResponse | null
}

export const fetchEventBusMessage = createAsyncThunk(
    'eventbusMessage/fetch',
    async (requestId: string, { dispatch }) => {
        const messageService = new EventBusMessageService();

        dispatch(showBackdrop());

        const apiResponse = await messageService.GetMessage(requestId);

        dispatch(closeBackdrop());

        if (apiResponse === null){
            return null;
        }

        if (apiResponse.isSuccess){
            return apiResponse!.object;
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

const initialState: MessageData = {
    data: null
}

const eventbusMessageSlice = createSlice({
    name: 'eventbusMessage',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchEventBusMessage.fulfilled, (state, action) => {
            state.data = action.payload;
        })
    }
});

export default eventbusMessageSlice.reducer;