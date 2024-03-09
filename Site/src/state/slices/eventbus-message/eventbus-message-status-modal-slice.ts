import { PayloadAction, createSlice } from "@reduxjs/toolkit"

interface StatusModalProps {
    show: boolean,
    requestId: string
}

const initialState: StatusModalProps = {
    show: false,
    requestId: ''
}

const eventbusMessageStatusModalSlice = createSlice({
    name: 'eventbusMessageStatusModal',
    initialState,
    reducers: {
        openEventBusMessageStatusModal: (state, { payload }: PayloadAction<string>) => {
            state.show = true;
            state.requestId = payload;
        },
        closeEventBusMessageStatusModal: (state) => {
            state.show = false;
            state.requestId = '';
        }
    }
});

export const { openEventBusMessageStatusModal, closeEventBusMessageStatusModal } = eventbusMessageStatusModalSlice.actions;

export default eventbusMessageStatusModalSlice.reducer;