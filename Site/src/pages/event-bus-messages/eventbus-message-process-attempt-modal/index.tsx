import { useEffect, useState } from "react";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import { EventBusMessageService } from "../../../services/eventbus-message-service";
import UpdateEventbusMessageStatusRequest from "../../../interfaces/requests/eventbus-received-message/update-eventbus-message-status-request";
import { Autocomplete, Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from "@mui/material";
import { Close, Save } from "@mui/icons-material";
import EnumData from "../../../interfaces/enum-data";
import { showSnackbar } from "../../../state/slices/app-snackbar-slice";
import { closeBackdrop, showBackdrop } from "../../../state/slices/app-backdrop-slice";
import { useAppDispatch, useAppSelector } from "../../../state/hooks/app-hooks";
import { RootState } from "../../../state/app-store";
import { closeEventBusMessageStatusModal } from "../../../state/slices/eventbus-message/eventbus-message-status-modal-slice";
import { fetchEventBusMessageList } from "../../../state/slices/eventbus-message/eventbus-message-list-slice";
import { fetchHttpStatusCodeList } from "../../../state/slices/enums/http-status-code-list-slice";

const EventBusMessageProcessAttemptModal = () => {
    const dispatch = useAppDispatch();
    const messageService = new EventBusMessageService();

    const modalProps = useAppSelector((state: RootState) => state.eventbusMessageStatusModal);
    const statusListContainer = useAppSelector((state: RootState) => state.httpStatusCodeList);
    const [statusList, setStatusList] = useState<EnumData[]>([]);
    const tableFilterRequest = useAppSelector((state: RootState) => state.eventbusMessageListRequest);

    const [processStatus, setProcessStatus] = useState<number>(0);
    const [resultMessage, setResultMessage] = useState<string>('');

    const updateMessageStatus = async () => {
        dispatch(showBackdrop());

        const request: UpdateEventbusMessageStatusRequest = {
            requestId: modalProps.requestId,
            processStatus: processStatus,
            resultMessage: resultMessage
        };

        const apiResponse = await messageService.UpdateMessageStatus(request);

        dispatch(closeBackdrop());

        if (apiResponse === null){
            return;
        }

        if (apiResponse.isSuccess) {
            dispatch(fetchEventBusMessageList(tableFilterRequest));
            dispatch(closeEventBusMessageStatusModal())
        }
        else {
            const snackbarResponse: AppSnackbarResponse = {
                success: false,
                message: apiResponse.message,
                stackTrace: apiResponse.stackTrace,
                statusCode: apiResponse.status
            };

            dispatch(showSnackbar(snackbarResponse));
        }
    }

    const onSelectStatus = (event: any, newValue: EnumData | null) => {
        if (newValue === null) {
            setProcessStatus(0);
        }
        else {
            setProcessStatus(newValue.intKey);
        }
    }

    useEffect(() => {
        dispatch(fetchHttpStatusCodeList());
    }, []);

    useEffect(() => {
        setStatusList(statusListContainer.data);
    }, [statusListContainer]);

    return (
        <>
            <Dialog open={modalProps.show} maxWidth="md">
                <DialogTitle>Register processing attempt</DialogTitle>
                <DialogContent sx={{ minWidth: '800px' }}>
                    {statusList && statusList.length > 0 && <Autocomplete
                        renderInput={(params) => <TextField {...params} label="Status" />} options={statusList}
                        getOptionLabel={(option) => `[${option.intKey}] ${option.description}`}
                        onChange={onSelectStatus} />}
                    <br />
                    <TextField value={resultMessage}
                        label="Result Message"
                        variant="standard"
                        multiline
                        rows={10}
                        fullWidth
                        required
                        onChange={event => setResultMessage(event.target.value)} />
                </DialogContent>
                <DialogActions>
                    <Button color="primary" onClick={() => updateMessageStatus()}>
                        <Save />
                        Save
                    </Button>
                    <Button color="secondary" onClick={() => dispatch(closeEventBusMessageStatusModal())}>
                        <Close />
                        Close
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}

export default EventBusMessageProcessAttemptModal;