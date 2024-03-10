import { Backdrop, Button, CircularProgress, Dialog, DialogActions, DialogContent, DialogTitle, Typography } from "@mui/material";
import GetEventbusQueueResponse from "../../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import EventBusQueueCard from "../eventbus-queue-card";
import { Close, Delete, Lock, LockOpen } from "@mui/icons-material";
import { AppActionType } from "../../../enums/app-action-type";
import { useEffect, useState } from "react";
import { EventBusQueueService } from "../../../services/eventbus-queue-service";
import UpdateEventbusQueueStatusRequest from "../../../interfaces/requests/eventbus-queue/update-eventbus-queue-status-request";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import AppSnackBar from "../../../components/app-snackbar";
import { useDispatch } from "react-redux";
import { showSnackbar } from "../../../state/slices/app-snackbar-slice";
import { closeBackdrop, showBackdrop } from "../../../state/slices/app-backdrop-slice";
import { useAppDispatch, useAppSelector } from "../../../state/hooks/app-hooks";
import { RootState } from "../../../state/app-store";
import { closeEventBusQueueModal } from "../../../state/slices/eventbus-queue/eventbus-queue-modal-slice";
import { fetchEventBusQueueList } from "../../../state/slices/eventbus-queue/eventbus-queue-list-slice";
import { ApiResponse } from "../../../interfaces/api-response";
import AppTaskResponse from "../../../interfaces/app-task-response";

const EventBusQueueModal = () => {
    const dispatch = useAppDispatch();
    const queueService = new EventBusQueueService();

    const [title, setTitle] = useState('');
    const [queue, setQueue] = useState<GetEventbusQueueResponse | null>();

    const modalContainer = useAppSelector((state: RootState) => state.eventbusQueueModal);
    const queueListRequest = useAppSelector((state: RootState) => state.eventbusQueueListRequest);

    const configureTitle = (action: AppActionType | null) => {
        if (action === null) {
            return;
        }

        if (action === AppActionType.Update) {
            setTitle('Change event bus queue status?');
        }
        else {
            setTitle('Delete event bus queue?');
        }
    }

    useEffect(() => {
        if (modalContainer.queue) {
            configureTitle(modalContainer.action);
            setQueue(modalContainer.queue);
        }
    }, [modalContainer]);

    const executeAction = async () => {
        dispatch(showBackdrop());

        let apiResponse: ApiResponse<AppTaskResponse> | null = null;

        if (modalContainer.action === AppActionType.Update) {
            let newStatus: number = 1;
            if (queue!.status.intKey === 1) {
                newStatus = 2;
            }

            const request: UpdateEventbusQueueStatusRequest = {
                id: queue!.id,
                status: newStatus
            };

            apiResponse = await queueService.UpdateStatus(request);
        }
        else if (modalContainer.action === AppActionType.Delete) {
            apiResponse = await queueService.DeleteQueue(queue!.id);
        }
        
        dispatch(showBackdrop());

        if (apiResponse === null) {
            return;
        }

        if (apiResponse.isSuccess) {
            dispatch(fetchEventBusQueueList(queueListRequest));
            dispatch(closeEventBusQueueModal());
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
            {queue && <Dialog open={modalContainer.show}>
                <DialogTitle>{title}</DialogTitle>
                <DialogContent>
                    <EventBusQueueCard queue={queue} showDescription={false} showSummarization={false} showNavigation={false} />
                </DialogContent>
                <DialogActions>
                    {modalContainer.action === AppActionType.Delete ? <Button color="error" onClick={() => executeAction()}>
                        <Delete />
                        Delete
                    </Button> : null}
                    {modalContainer.action === AppActionType.Update ? <Button color="warning" onClick={() => executeAction()}>
                        {queue.status.intKey === 1 ? <Lock /> : <LockOpen />}
                        {queue.status.intKey === 1 ? <Typography>Disable</Typography> : <Typography>Enable</Typography>}
                    </Button> : null}
                    <Button color="secondary" onClick={() => dispatch(closeEventBusQueueModal())}>
                        <Close />
                        Close
                    </Button>
                </DialogActions>
            </Dialog>}
        </>
    );
}

export default EventBusQueueModal;