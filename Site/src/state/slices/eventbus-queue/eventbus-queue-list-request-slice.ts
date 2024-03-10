import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import GetEventBusQueueListRequest from "../../../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";

const initialState: GetEventBusQueueListRequest = {
    page: 1,
    pageSize: 10,
    summarizeMessages: false,
    status: null,
    nameMatch: null,
    descriptionMatch: null
}

const eventbusQueueListRequestSlice = createSlice({
    name: 'eventbusQueueListRequest',
    initialState,
    reducers: {
        setEventBusQueueListRequest: (state, { payload }: PayloadAction<GetEventBusQueueListRequest>) => {
            state.page = payload.page;
            state.pageSize = payload.pageSize;
            state.nameMatch = payload.nameMatch;
            state.descriptionMatch = payload.descriptionMatch;
            state.summarizeMessages = payload.summarizeMessages;
        },
        setEventBusQueueListPagination: (state, { payload }: PayloadAction<{page: number, pageSize: number}>) => {
            state.page = payload.page;
            state.pageSize = payload.pageSize;
        },
        setEventBusQueueListSummarization: (state, { payload }: PayloadAction<boolean>) => {
            state.summarizeMessages = payload;
        },
        setEventBusQueueListStatusMatch: (state, { payload }: PayloadAction<number | null>) => {
            state.status = payload;
        },
        setEventBusQueueListNameMatch: (state, { payload }: PayloadAction<string>) => {
            state.nameMatch = payload;
        },
        setEventBusQueueListDescriptionMatch: (state, { payload }: PayloadAction<string>) => {
            state.descriptionMatch = payload;
        },
        cleanEventBusQueueListRequest: (state) => {
            state.page = 1;
            state.pageSize = 10;
            state.nameMatch = null;
            state.descriptionMatch = null;
            state.summarizeMessages = false;
            state.status = null;
        }
    }
});

export const { setEventBusQueueListRequest, setEventBusQueueListPagination, 
    setEventBusQueueListSummarization, setEventBusQueueListStatusMatch, setEventBusQueueListNameMatch, 
    setEventBusQueueListDescriptionMatch, cleanEventBusQueueListRequest } = eventbusQueueListRequestSlice.actions;

export default eventbusQueueListRequestSlice.reducer;