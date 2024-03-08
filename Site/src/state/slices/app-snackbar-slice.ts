import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import AppSnackbarResponse from "../../interfaces/requests/app-snackbar-response";

export interface AppSnackbarControl {
    show: boolean,
    response: AppSnackbarResponse | null
}

const initialState: AppSnackbarControl = {
    show: false,
    response: null
}

const appSnackbarSlice = createSlice({
    name: 'appSnackbar',
    initialState,
    reducers: {
        showSnackbar: (state, { payload }: PayloadAction<AppSnackbarResponse>) => {
            state.show = true;
            state.response = payload;
        },
        closeSnackbar: (state) => {
            state.show = false;
            state.response = null;
        }
    }
});

export const { showSnackbar, closeSnackbar } = appSnackbarSlice.actions;

export default appSnackbarSlice.reducer;