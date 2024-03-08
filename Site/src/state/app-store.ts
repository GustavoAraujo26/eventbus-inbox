import { configureStore } from "@reduxjs/toolkit";
import appSnackbarSlice from "./slices/app-snackbar-slice";
import appBackdropSlice from "./slices/app-backdrop-slice";

const appStore = configureStore({
    reducer: {
        appSnackbar: appSnackbarSlice,
        appBackdrop: appBackdropSlice
    }
});

export default appStore;

export type RootState = ReturnType<typeof appStore.getState>;