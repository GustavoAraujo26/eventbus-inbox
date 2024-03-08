import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import GetEventbusMessageListRequest from "../../../interfaces/requests/eventbus-received-message/get-eventbus-message-list-request";
import Period from "../../../interfaces/period";

const initialState: GetEventbusMessageListRequest = {
    queueId: null,
    creationDateSearch: null,
    updateDateSearch: null,
    statusToSearch: null,
    typeMatch: null,
    page: 1,
    pageSize: 10
}

const eventbusMessageListRequestSlice = createSlice({
    name: 'eventbusMessageListRequest',
    initialState,
    reducers: {
        changeEventBusMessagePagination: (state, {payload}: PayloadAction<{page: number, pageSize: number}>) => {
            state.page = payload.page;
            state.pageSize = payload.pageSize;
        },
        setEventBusMessageListQueue: (state, { payload }: PayloadAction<string | null>) => {
            state.queueId = payload;
        },
        setEventBusMessageListCreatedAtSearch: (state, { payload }: PayloadAction<Period | null>) => {
            state.creationDateSearch = payload;
        },
        setEventBusMessageListUpdatedAtSearch: (state, { payload }: PayloadAction<Period | null>) => {
            state.updateDateSearch = payload;
        },
        setEventBusMessageListTypeMatch: (state, { payload }: PayloadAction<string | null>) => {
            state.typeMatch = payload;
        },
        addStatusToEventBusMessageListSearch: (state, { payload }: PayloadAction<number>) => {
            if (state.statusToSearch === null){
                state.statusToSearch = [payload];
            }
            else{
                state.statusToSearch = [...state.statusToSearch, payload];
            }
        },
        removeStatusFromEventBusMessageListSearch: (state, { payload }: PayloadAction<number>) => {
            if (state.statusToSearch === null) return;

            state.statusToSearch = state.statusToSearch.filter(item => item !== payload);
        },
        setEventBusMessageListRequest: (state, { payload }: PayloadAction<GetEventbusMessageListRequest>) => {
            state = payload;
        },
        setCleanEventBusListRequest: (state) => {
            state.queueId = null;
            state.creationDateSearch = null;
            state.updateDateSearch = null,
            state.statusToSearch = null;
            state.typeMatch = null;
            state.page = 1;
            state.pageSize = 10;
        }
    }
});

export const { changeEventBusMessagePagination, setEventBusMessageListQueue, setEventBusMessageListCreatedAtSearch, 
    setEventBusMessageListUpdatedAtSearch, setEventBusMessageListTypeMatch, addStatusToEventBusMessageListSearch, 
    removeStatusFromEventBusMessageListSearch, setEventBusMessageListRequest, setCleanEventBusListRequest } = eventbusMessageListRequestSlice.actions;

export default eventbusMessageListRequestSlice.reducer;