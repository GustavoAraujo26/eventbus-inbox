import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import GetEventbusMessageListResponse from "../../../interfaces/responses/eventbus-received-message/get-eventbus-message-list-response";
import { AppActionType } from "../../../enums/app-action-type";

interface ActionProps {
    show: boolean,
    action: AppActionType | null,
    data: GetEventbusMessageListResponse | null
}

const initialState: ActionProps = {
    show: false,
    action: null,
    data: null
}

const eventbusMessageActionModalSlice = createSlice({
    name: 'eventbusMessageActionModal',
    initialState,
    reducers: {
        openEventBusMessageActionModal: (state, { payload }: PayloadAction<{ action: AppActionType, data: GetEventbusMessageListResponse }>) => {
            state.show = true;
            state.action = payload.action;
            state.data = payload.data;
        },
        closeEventBusMessageActionModal: (state) => {
            state.show = false;
            state.action = null;
            state.data = null;
        }
    }
});

export const { openEventBusMessageActionModal, closeEventBusMessageActionModal } = eventbusMessageActionModalSlice.actions;

export default eventbusMessageActionModalSlice.reducer;