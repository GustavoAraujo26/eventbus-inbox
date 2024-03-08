import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import EnumData from "../../../interfaces/enum-data";
import { EnumsService } from "../../../services/enums-service";
import { closeBackdrop, showBackdrop } from "../app-backdrop-slice";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import { showSnackbar } from "../app-snackbar-slice";

interface EnumResponse {
    data: EnumData[]
}

export const fetchEventBusMessageStatusList = createAsyncThunk(
    'eventBusMessageStatusList/fetch',
    async (_, { dispatch }) => {
        const enumService = new EnumsService();

        dispatch(showBackdrop());

        const response = await enumService.ListMessageStatus();
        const apiResponse = response.data;

        dispatch(closeBackdrop());

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

const eventBusMessageStatusListSlice = createSlice({
    name: 'eventBusMessageStatusList',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchEventBusMessageStatusList.fulfilled, (state, action) => {
            state.data = action.payload;
        })
    }
});

export default eventBusMessageStatusListSlice.reducer;