import { Button, Dialog, DialogActions, DialogContent, DialogTitle } from "@mui/material";
import { AppActionType } from "../../../enums/app-action-type";
import GetEventbusMessageListResponse from "../../../interfaces/responses/eventbus-received-message/get-eventbus-message-list-response";
import { EventBusMessageService } from "../../../services/eventbus-message-service";
import { useEffect, useState } from "react";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import EventBusMessageCard from "../eventbus-message-card";
import { Save, Close, RestartAlt } from "@mui/icons-material";
import { ApiResponse } from "../../../interfaces/api-response";
import AppTaskResponse from "../../../interfaces/app-task-response";
import { showSnackbar } from "../../../state/slices/app-snackbar-slice";
import { closeBackdrop, showBackdrop } from "../../../state/slices/app-backdrop-slice";
import { useAppDispatch, useAppSelector } from "../../../state/hooks/app-hooks";
import { RootState } from "../../../state/app-store";
import { closeEventBusMessageActionModal } from "../../../state/slices/eventbus-message/eventbus-message-action-modal-slice";
import { fetchEventBusMessageList } from "../../../state/slices/eventbus-message/eventbus-message-list-slice";

const EventBusMessageActionModal = () => {
    const dispatch = useAppDispatch();
    const messageService = new EventBusMessageService();

    const modalDataContainer = useAppSelector((state: RootState) => state.eventbusMessageActionModal);
    const tableFilterRequest = useAppSelector((state: RootState) => state.eventbusMessageListRequest);

    const [selectedMessage, setSelectedMessage] = useState<GetEventbusMessageListResponse | null>();
    const [title, setTitle] = useState('');

    useEffect(() => {
        if (modalDataContainer.action === null) {
            return;
        }

        if (modalDataContainer.action === AppActionType.Update) {
            setTitle("Reactivating event bus message");
        }
        else {
            setTitle("Deleting event bus message");
        }
    }, [modalDataContainer.action]);

    useEffect(() => {
        setSelectedMessage(modalDataContainer.data);
    }, [modalDataContainer]);

    const executeAction = async () => {
        dispatch(showBackdrop());

        let apiResponse: ApiResponse<AppTaskResponse> | null = null;
        if (modalDataContainer.action === AppActionType.Update) {
            apiResponse = await messageService.ReactivateMessage(selectedMessage!.requestId);
        }
        else if (modalDataContainer.action === AppActionType.Delete) {
            apiResponse = await messageService.DeleteMessage(selectedMessage!.requestId);
        }

        dispatch(closeBackdrop());

        if (apiResponse === null) {
            return;
        }

        if (apiResponse.isSuccess) {
            dispatch(fetchEventBusMessageList(tableFilterRequest));
            dispatch(closeEventBusMessageActionModal());
        }
        else {
            const response: AppSnackbarResponse = {
                success: false,
                message: apiResponse.message,
                stackTrace: apiResponse.stackTrace,
                statusCode: apiResponse.status
            }

            dispatch(showSnackbar(response));
        }
    }

    return (
        <>
            {selectedMessage && <Dialog open={modalDataContainer.show}>
                <DialogTitle sx={{ textAlign: 'center' }}>{title}</DialogTitle>
                <DialogContent>
                    <EventBusMessageCard requestId={selectedMessage.requestId} type={selectedMessage.type}
                        creationDate={selectedMessage.createdAt} processingAttempts={selectedMessage.processingAttempts}
                        status={selectedMessage.status} queueName={selectedMessage.queue.name}
                        queueProcessingAttempts={selectedMessage!.queue.processingAttempts} />
                </DialogContent>
                <DialogActions>
                    {modalDataContainer.action === AppActionType.Update && <Button color="success" onClick={() => executeAction()}>
                        <RestartAlt />
                        Reactivate
                    </Button>}
                    {modalDataContainer.action === AppActionType.Delete && <Button color="error" onClick={() => executeAction()}>
                        <Save />
                        Delete
                    </Button>}

                    <Button color="secondary" onClick={() => dispatch(closeEventBusMessageActionModal())}>
                        <Close />
                        Close
                    </Button>
                </DialogActions>
            </Dialog>}
        </>
    );
}

export default EventBusMessageActionModal;