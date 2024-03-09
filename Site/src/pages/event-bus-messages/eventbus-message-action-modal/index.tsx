import { Backdrop, Button, CircularProgress, Dialog, DialogActions, DialogContent, DialogTitle } from "@mui/material";
import { AppActionType } from "../../../enums/app-action-type";
import GetEventbusMessageListResponse from "../../../interfaces/responses/eventbus-received-message/get-eventbus-message-list-response";
import { EventBusMessageService } from "../../../services/eventbus-message-service";
import { useEffect, useState } from "react";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import EventBusMessageCard from "../eventbus-message-card";
import { Save, Close, RestartAlt } from "@mui/icons-material";
import { AxiosResponse } from "axios";
import { ApiResponse } from "../../../interfaces/api-response";
import AppTaskResponse from "../../../interfaces/app-task-response";
import { useDispatch } from "react-redux";
import { showSnackbar } from "../../../state/slices/app-snackbar-slice";
import { closeBackdrop, showBackdrop } from "../../../state/slices/app-backdrop-slice";
import { useAppDispatch } from "../../../state/hooks/app-hooks";

interface ActionModalProps {
    selectedMessage: GetEventbusMessageListResponse,
    onClose: () => void,
    updateList: () => void,
    showModal: boolean,
    actionType: AppActionType
}

const EventBusMessageActionModal = ({ selectedMessage, onClose, updateList, showModal, actionType }: ActionModalProps) => {
    const dispatch = useAppDispatch();
    const messageService = new EventBusMessageService();

    const [title, setTitle] = useState('');

    useEffect(() => {
        if (actionType === AppActionType.Update) {
            setTitle("Reactivating event bus message");
        }
        else {
            setTitle("Deleting event bus message");
        }
    }, [actionType]);

    const executeAction = () => {
        dispatch(showBackdrop());

        let serverCall: Promise<AxiosResponse<ApiResponse<AppTaskResponse>, any>> | null = null;
        if (actionType === AppActionType.Update){
            serverCall = messageService.ReactivateMessage(selectedMessage.requestId);
        }
        else if (actionType === AppActionType.Delete){
            serverCall = messageService.DeleteMessage(selectedMessage.requestId);
        }
        else{
            dispatch(closeBackdrop());
        }

        if (serverCall !== null){
            serverCall.then(response => {
                dispatch(closeBackdrop());

                const apiResponse = response.data;
                if (apiResponse.isSuccess){
                    updateList();
                    onClose();
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
    }

    return (
        <>
            <Dialog open={showModal}>
                <DialogTitle sx={{textAlign: 'center'}}>{title}</DialogTitle>
                <DialogContent>
                    <EventBusMessageCard requestId={selectedMessage.requestId} type={selectedMessage.type}
                        creationDate={selectedMessage.createdAt} processingAttempts={selectedMessage.processingAttempts}
                        status={selectedMessage.status} queueName={selectedMessage.queue.name}
                        queueProcessingAttempts={selectedMessage.queue.processingAttempts} />
                </DialogContent>
                <DialogActions>
                    {actionType === AppActionType.Update && <Button color="success" onClick={() => executeAction()}>
                        <RestartAlt />
                        Reactivate
                    </Button>}
                    {actionType === AppActionType.Delete && <Button color="error" onClick={() => executeAction()}>
                        <Save />
                        Delete
                    </Button>}

                    <Button color="secondary" onClick={() => onClose()}>
                        <Close />
                        Close
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}

export default EventBusMessageActionModal;