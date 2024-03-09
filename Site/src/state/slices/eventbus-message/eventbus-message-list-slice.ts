import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import GetEventbusMessageListResponse from "../../../interfaces/responses/eventbus-received-message/get-eventbus-message-list-response";
import GetEventbusMessageListRequest from "../../../interfaces/requests/eventbus-received-message/get-eventbus-message-list-request";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import { showBackdrop, closeBackdrop } from "../app-backdrop-slice";
import { showSnackbar } from "../app-snackbar-slice";
import { EventBusMessageService } from "../../../services/eventbus-message-service";
import axios, { AxiosError, AxiosResponse } from "axios";
import { ApiResponse } from "../../../interfaces/api-response";

interface MessageContainer {
    data: GetEventbusMessageListResponse[]
}

export const fetchEventBusMessageList = createAsyncThunk(
    'eventbusMessageList/fetch',
    async (data: GetEventbusMessageListRequest | null, { dispatch }) => {
        const messageService = new EventBusMessageService();

        dispatch(showBackdrop());

        let request: GetEventbusMessageListRequest = {
            page: 1,
            pageSize: 1000000,
            queueId: null,
            statusToSearch: null,
            creationDateSearch: null,
            updateDateSearch: null, 
            typeMatch: null
        };
        if (data !== null){
            request = data;
        }
        
        const apiResponse = await messageService.ListMessage(request);

        dispatch(closeBackdrop());

        if (apiResponse === null){
            return [];
        }

        if (apiResponse.isSuccess){
            return apiResponse!.data;
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

const initialState: MessageContainer = {
    data: []
}

const eventbusMessageListSlice = createSlice({
    name: 'eventbusMessageList',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchEventBusMessageList.fulfilled, (state, action) => {
            state.data = action.payload;
        })
    }
});

export default eventbusMessageListSlice.reducer;