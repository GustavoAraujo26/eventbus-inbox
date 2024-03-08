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

interface StatusModalProps {
    queue: GetEventbusQueueResponse | undefined,
    showModal: boolean,
    closeModal: () => void,
    updateList: () => void,
    action: AppActionType
}

const EventBusQueueModal = ({ queue, showModal, closeModal, updateList, action }: StatusModalProps) => {
    const dispatch = useDispatch();
    const queueService = new EventBusQueueService();

    const [title, setTitle] = useState('');

    useEffect(() => {
        if (action === AppActionType.Update) {
            setTitle('Change event bus queue status?');
        }
        else {
            setTitle('Delete event bus queue?');
        }
    }, [action]);

    const updateStatus = () => {
        dispatch(showBackdrop());
        
        let newStatus: number = 1;
        if (queue!.status.intKey === 1) {
            newStatus = 2;
        }

        const request: UpdateEventbusQueueStatusRequest = {
            id: queue!.id,
            status: newStatus
        };

        queueService.UpdateStatus(request).then(response => {
            dispatch(closeBackdrop());

            const apiResponse = response.data;

            if (apiResponse.isSuccess){
                updateList();
                closeModal();
            }
            else{
                const response: AppSnackbarResponse = {
                    success: false,
                    message: apiResponse.message,
                    stackTrace: apiResponse.stackTrace,
                    statusCode: apiResponse.status
                }
    
                dispatch(showSnackbar(response));
            }
        }).catch(error => {
            dispatch(closeBackdrop());

            console.log(error);
            let response: AppSnackbarResponse = {
                success: false,
                message: error.toString().substring(0, 50)
            }

            const apiResponse = error.response.data;
            if (typeof apiResponse !== 'undefined') {
                response.message = apiResponse.message;
                response.stackTrace = apiResponse.stackTrace;
                response.statusCode = apiResponse.status;
            }

            dispatch(showSnackbar(response));
        });
    }

    const deleteQueue = () => {
        dispatch(showBackdrop());

        queueService.DeleteQueue(queue!.id).then(response => {
            dispatch(closeBackdrop());
            
            const apiResponse = response.data;

            if (apiResponse.isSuccess){
                updateList();
                closeModal();
            }
            else{
                const response: AppSnackbarResponse = {
                    success: false,
                    message: apiResponse.message,
                    stackTrace: apiResponse.stackTrace,
                    statusCode: apiResponse.status
                }
    
                dispatch(showSnackbar(response));
            }
        }).catch(error => {
            dispatch(closeBackdrop());

            console.log(error);
            let response: AppSnackbarResponse = {
                success: false,
                message: error.toString().substring(0, 50)
            }

            const apiResponse = error.response.data;
            if (typeof apiResponse !== 'undefined') {
                response.message = apiResponse.message;
                response.stackTrace = apiResponse.stackTrace;
                response.statusCode = apiResponse.status;
            }

            dispatch(showSnackbar(response));
        });
    }

    return (
        <>
            {queue && <Dialog open={showModal}>
                <DialogTitle>{title}</DialogTitle>
                <DialogContent>
                    <EventBusQueueCard queue={queue} showDescription={false} showSummarization={false} showNavigation={false} />
                </DialogContent>
                <DialogActions>
                    {action === AppActionType.Delete ? <Button color="error" onClick={() => deleteQueue()}>
                        <Delete />
                        Delete
                    </Button> : null}
                    {action === AppActionType.Update ? <Button color="warning" onClick={() => updateStatus()}>
                        {queue.status.intKey === 1 ? <Lock /> : <LockOpen />}
                        {queue.status.intKey === 1 ? <Typography>Disable</Typography> : <Typography>Enable</Typography>}
                    </Button> : null}
                    <Button color="secondary" onClick={() => closeModal()}>
                        <Close />
                        Close
                    </Button>
                </DialogActions>
            </Dialog>}
        </>
    );
}

export default EventBusQueueModal;