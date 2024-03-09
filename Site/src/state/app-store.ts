import { configureStore } from "@reduxjs/toolkit";
import appSnackbarSlice from "./slices/app-snackbar-slice";
import appBackdropSlice from "./slices/app-backdrop-slice";
import eventbusMessageListRequestSlice from "./slices/eventbus-message/eventbus-message-list-request-slice";
import eventbusMessageListSlice from "./slices/eventbus-message/eventbus-message-list-slice";
import eventbusQueueListRequestSlice from "./slices/eventbus-queue/eventbus-queue-list-request-slice";
import httpStatusCodeListSlice from "./slices/enums/http-status-code-list-slice";
import eventBusMessageStatusListSlice from "./slices/enums/eventbus-message-status-list-slice";
import eventBusQueueStatusListSlice from "./slices/enums/eventbus-queue-status-list-slice";
import eventBusQueueListSlice from "./slices/eventbus-queue/eventbus-queue-list-slice";
import eventbusMessageStatusModalSlice from "./slices/eventbus-message/eventbus-message-status-modal-slice";

const appStore = configureStore({
    reducer: {
        appSnackbar: appSnackbarSlice,
        appBackdrop: appBackdropSlice,
        eventbusMessageListRequest: eventbusMessageListRequestSlice,
        eventbusMessageList: eventbusMessageListSlice,
        eventbusQueueListRequest: eventbusQueueListRequestSlice,
        eventbusQueueList: eventBusQueueListSlice,
        eventBusMessageStatusList: eventBusMessageStatusListSlice,
        eventBusQueueStatusList: eventBusQueueStatusListSlice,
        httpStatusCodeList: httpStatusCodeListSlice,
        eventbusMessageStatusModal: eventbusMessageStatusModalSlice
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware({
        serializableCheck: false
    })
});

export default appStore;

export type AppDispatch = typeof appStore.dispatch;

export type RootState = ReturnType<typeof appStore.getState>;