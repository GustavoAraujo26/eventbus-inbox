import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import EnumData from "../../../interfaces/enum-data";
import { EnumsService } from "../../../services/enums-service";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import { showBackdrop, closeBackdrop } from "../app-backdrop-slice";
import { showSnackbar } from "../app-snackbar-slice";

interface EnumResponse {
    data: EnumData[]
}

export const fetchEventBusQueueStatusList = createAsyncThunk(
    'eventBusQueueStatusList/fetch',
    async (_, { dispatch }) => {
        const enumService = new EnumsService();

        dispatch(showBackdrop());

        const apiResponse = await enumService.ListQueueStatus();
        
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

const initialState: EnumResponse = {
    data: []
}

const eventBusQueueStatusListSlice = createSlice({
    name: 'eventBusQueueStatusList',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchEventBusQueueStatusList.fulfilled, (state, action) => {
            state.data = action.payload;
        })
    }
});

export default eventBusQueueStatusListSlice.reducer;