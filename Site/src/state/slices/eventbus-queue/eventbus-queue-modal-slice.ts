import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { AppActionType } from "../../../enums/app-action-type";
import GetEventbusQueueResponse from "../../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";

interface ModalProps {
    show: boolean,
    queue: GetEventbusQueueResponse | null,
    action: AppActionType | null
}

const initialState: ModalProps = {
    show: false,
    queue: null,
    action: null
}

const eventbusQueueModalSlice = createSlice({
    name: 'eventbusQueueModal',
    initialState,
    reducers: {
        openEventBusQueueModal: (state, { payload }: PayloadAction<{queue: GetEventbusQueueResponse, action: AppActionType}>) => {
            state.show = true;
            state.queue = payload.queue;
            state.action = payload.action;
        },
        closeEventBusQueueModal: (state) => {
            state.show = false;
            state.queue = null;
            state.action = null
        }
    }
});

export const { openEventBusQueueModal, closeEventBusQueueModal } = eventbusQueueModalSlice.actions;

export default eventbusQueueModalSlice.reducer;