import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import EnumData from "../../../interfaces/enum-data";
import { EnumsService } from "../../../services/enums-service";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import { showBackdrop, closeBackdrop } from "../app-backdrop-slice";
import { showSnackbar } from "../app-snackbar-slice";

interface EnumResponse {
    data: EnumData[]
}

export const fetchHttpStatusCodeList = createAsyncThunk(
    'httpStatusCodeList/fetch',
    async (_, { dispatch }) => {
        const enumService = new EnumsService();

        dispatch(showBackdrop());

        const apiResponse = await enumService.ListHttpStatusCode();

        dispatch(closeBackdrop());

        if (apiResponse === null){
            return [];
        }

        if (apiResponse.isSuccess){
            const filteredList = apiResponse.data.filter((value, index, self) => 
                    index === self.findIndex((t) => (
                        t.intKey === value.intKey && t.stringKey === value.stringKey && t.description === value.description
                    ))
                );
            return filteredList;
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

const httpStatusCodeListSlice = createSlice({
    name: 'httpStatusCodeList',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchHttpStatusCodeList.fulfilled, (state, action) => {
            state.data = action.payload;
        })
    }
});

export default httpStatusCodeListSlice.reducer;