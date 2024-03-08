import { PayloadAction, createSlice } from "@reduxjs/toolkit";

const initialState: boolean = false;

const appBackdropSlice = createSlice({
    name: 'appBackdrop',
    initialState,
    reducers: {
        showBackdrop: (state) => state = true,
        closeBackdrop: (state) => state = false
    }
});

export const { showBackdrop, closeBackdrop } = appBackdropSlice.actions;

export default appBackdropSlice.reducer;