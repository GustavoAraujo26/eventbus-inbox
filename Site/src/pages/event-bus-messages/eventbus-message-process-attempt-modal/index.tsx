import { ReactNode, useEffect, useState } from "react";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import { EnumsService } from "../../../services/enums-service";
import { EventBusMessageService } from "../../../services/eventbus-message-service";
import { useNavigate } from "react-router-dom";
import UpdateEventbusMessageStatusRequest from "../../../interfaces/requests/eventbus-received-message/update-eventbus-message-status-request";
import { Autocomplete, AutocompleteRenderInputParams, Backdrop, Button, CircularProgress, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from "@mui/material";
import AppSnackBar from "../../../components/app-snackbar";
import { Close, Save } from "@mui/icons-material";
import EnumData from "../../../interfaces/enum-data";

interface ProcessAttemptModalProps {
    requestId: string,
    showModal: boolean,
    updateList: () => void,
    closeModal: () => void
}

const EventBusMessageProcessAttemptModal = ({ requestId, showModal, closeModal, updateList }: ProcessAttemptModalProps) => {
    const enumService = new EnumsService();
    const messageService = new EventBusMessageService();

    const [statusList, setStatusList] = useState<EnumData[]>([]);

    const [snackbarResponse, setSnackbarResponse] = useState<AppSnackbarResponse>();
    const [isLoading, setLoading] = useState(false);

    const [processStatus, setProcessStatus] = useState<number>(0);
    const [resultMessage, setResultMessage] = useState<string>('');

    const loadingStatus = () => {
        setLoading(true);

        enumService.ListHttpStatusCode().then(response => {
            setLoading(false);

            const apiResponse = response.data;

            if (apiResponse.isSuccess) {
                const filteredList = apiResponse.data.filter((value, index, self) => 
                    index === self.findIndex((t) => (
                        t.intKey === value.intKey && t.stringKey === value.stringKey && t.description === value.description
                    ))
                );
                setStatusList(filteredList);
            }
            else {
                const response: AppSnackbarResponse = {
                    success: false,
                    message: apiResponse.message,
                    stackTrace: apiResponse.stackTrace,
                    statusCode: apiResponse.status
                }

                setSnackbarResponse(response);
            }
        }).catch(error => {
            setLoading(false);

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

            setSnackbarResponse(response);
        });
    }

    const updateStatus = () => {
        setLoading(true);

        const request: UpdateEventbusMessageStatusRequest = {
            requestId: requestId,
            processStatus: processStatus,
            resultMessage: resultMessage
        };

        messageService.UpdateMessageStatus(request).then(response => {
            setLoading(false);

            const apiResponse = response.data;

            if (apiResponse.isSuccess) {
                updateList();
                closeModal();
            }
            else {
                const response: AppSnackbarResponse = {
                    success: false,
                    message: apiResponse.message,
                    stackTrace: apiResponse.stackTrace,
                    statusCode: apiResponse.status
                }

                setSnackbarResponse(response);
            }
        }).catch(error => {
            setLoading(false);

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

            setSnackbarResponse(response);
        });
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
        loadingStatus();
    }, []);

    return (
        <>
            <Backdrop open={isLoading}>
                <CircularProgress color="inherit" />
            </Backdrop>
            <Dialog open={showModal} maxWidth="md">
                <DialogTitle>Register processing attempt</DialogTitle>
                <DialogContent sx={{ minWidth: '800px' }}>
                    {statusList && statusList.length > 0 && <Autocomplete 
                        renderInput={(params) => <TextField {...params} label="Status" />} options={statusList}
                        getOptionLabel={(option) => `[${option.intKey}] ${option.description}`}
                        onChange={onSelectStatus}/>}
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
                    <Button color="primary" onClick={() => updateStatus()}>
                        <Save />
                        Save
                    </Button>
                    <Button color="secondary" onClick={() => closeModal()}>
                        <Close />
                        Close
                    </Button>
                </DialogActions>
            </Dialog>
            <AppSnackBar response={snackbarResponse} />
        </>
    );
}

export default EventBusMessageProcessAttemptModal;