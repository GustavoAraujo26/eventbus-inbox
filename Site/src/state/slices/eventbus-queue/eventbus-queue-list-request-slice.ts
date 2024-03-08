import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import GetEventBusQueueListRequest from "../../../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";

const initialState: GetEventBusQueueListRequest = {
    page: 1,
    pageSize: 1000000,
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
            state = payload;
        },
        setEventBusQueueListPagination: (state, { payload }: PayloadAction<{page: number, pageSize: number}>) => {
            state.page = payload.page;
            state.pageSize = payload.pageSize;
        },
        setEventBusQueueListSummarization: (state, { payload }: PayloadAction<boolean>) => {
            state.summarizeMessages = payload;
        },
        setEventBusQueueListStateMatch: (state, { payload }: PayloadAction<number>) => {
            state.status = payload;
        },
        setEventBusQueueListNameMatch: (state, { payload }: PayloadAction<string>) => {
            state.nameMatch = payload;
        },
        setEventBusQueueListDescriptionMatch: (state, { payload }: PayloadAction<string>) => {
            state.descriptionMatch = payload;
        }
    }
});

export const { setEventBusQueueListRequest, setEventBusQueueListPagination, 
    setEventBusQueueListSummarization, setEventBusQueueListStateMatch, setEventBusQueueListNameMatch, 
    setEventBusQueueListDescriptionMatch } = eventbusQueueListRequestSlice.actions;

export default eventbusQueueListRequestSlice.reducer;