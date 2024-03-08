import { useNavigate, useParams } from "react-router-dom";
import { EventBusMessageService } from "../../services/eventbus-message-service";
import { useEffect, useState } from "react";
import AppSnackbarResponse from "../../interfaces/requests/app-snackbar-response";
import { HomeOutlined, Apps, Edit, Add, ArrowBack, Send, Save } from "@mui/icons-material";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import GetEventbusMessageResponse from "../../interfaces/responses/eventbus-received-message/get-eventbus-message-response";
import { Backdrop, Box, Button, Card, CardContent, CircularProgress, FormControl, Grid, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import AppSnackBar from "../../components/app-snackbar";
import GetEventbusQueueResponse from "../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import GetEventBusQueueListRequest from "../../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";
import { EventBusQueueService } from "../../services/eventbus-queue-service";
import SendEventbusMessageRequest from "../../interfaces/requests/eventbus-sender/send-eventbus-message-request";
import SaveEventbusMessageRequest from "../../interfaces/requests/eventbus-received-message/save-eventbus-message-request";
import { useDispatch } from "react-redux";
import { showSnackbar } from "../../state/slices/app-snackbar-slice";
import { closeBackdrop, showBackdrop } from "../../state/slices/app-backdrop-slice";

const EventBusMessageForm = () => {
    const dispatch = useDispatch();
    const queueService = new EventBusQueueService();
    const messageService = new EventBusMessageService();
    const parameters = useParams();
    const navigateTo = useNavigate();

    const [breadcrumbItems, setBreadcrumbItems] = useState<AppBreadcrumbItem[]>([]);
    const [eventbusMessage, setEventBusMessage] = useState<GetEventbusMessageResponse>();
    const [queueList, setQueueList] = useState<GetEventbusQueueResponse[]>([]);

    const [queueId, setQueueId] = useState('');
    const [messageType, setMessageType] = useState('');
    const [messageContent, setMessageContent] = useState('');

    const buildbreadcrumb = () => {
        const home: AppBreadcrumbItem = {
            id: 1,
            icon: <HomeOutlined sx={{ mr: 0.5 }} />,
            text: 'Home',
            goTo: '/',
            isPage: false
        };

        const messageDashboard: AppBreadcrumbItem = {
            id: 2,
            icon: <Apps sx={{ mr: 0.5 }} />,
            text: 'Event Bus Messages Dashboard',
            goTo: '/eventbus-messages/dashboard',
            isPage: false
        };

        const messageForm: AppBreadcrumbItem = {
            id: 3,
            icon: <Edit sx={{ mr: 0.5 }} />,
            text: `Edit event Bus queue ${parameters.id}`,
            goTo: '',
            isPage: true
        };

        const newList: AppBreadcrumbItem[] = [home, messageDashboard, messageForm];

        setBreadcrumbItems(newList);
    }

    const getEventBusMessage = (parameterId: string) => {
        dispatch(showBackdrop());

        messageService.GetMessage(parameterId).then(response => {
            dispatch(closeBackdrop());
            const apiResponse = response.data;
            if (apiResponse.isSuccess) {
                setEventBusMessage(apiResponse.object);
                setQueueId(apiResponse.object.queue.id);
                setMessageType(apiResponse.object.type);
                setMessageContent(JSON.stringify(apiResponse.object.content, null, 2));
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

    const loadQueueList = () => {
        dispatch(showBackdrop());
        const request: GetEventBusQueueListRequest = {
            nameMatch: null,
            descriptionMatch: null,
            status: null,
            page: 1,
            pageSize: 1000000,
            summarizeMessages: false
        }

        queueService.ListQueues(request).then(response => {
            dispatch(closeBackdrop());

            const apiResponse = response.data;

            if (apiResponse.isSuccess) {
                setQueueList(apiResponse.data);
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

    const onSubmitMessage = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        dispatch(showBackdrop());

        const request: SaveEventbusMessageRequest = {
            requestId: eventbusMessage!.requestId,
            createdAt: eventbusMessage!.createdAt,
            type: messageType,
            content: messageContent,
            queueId: queueId
        };

        messageService.SaveMessage(request).then(response => {
            dispatch(closeBackdrop());

            const apiResponse = response.data;
            if (apiResponse.isSuccess) {
                navigateTo(-1);
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

    useEffect(() => {
        if (parameters.id) {
            loadQueueList();
            getEventBusMessage(parameters.id);
        }

        buildbreadcrumb();
    }, [parameters]);

    return (
        <>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            {eventbusMessage && <Grid container justifyContent="center" spacing={2}>
                <Grid item md={8}>
                    <Card>
                        <CardContent>
                            <Box component="form" onSubmit={onSubmitMessage}>
                                {queueList && <FormControl fullWidth>
                                    <InputLabel variant="standard" htmlFor="page-size-select">Select the status</InputLabel>
                                    <Select id="page-size-select" value={queueId} onChange={event => setQueueId(event.target.value)} required fullWidth>
                                        <MenuItem value="">Select an option</MenuItem>
                                        {queueList.map(option => <MenuItem key={option.id} value={option.id}>
                                            {option.name}
                                        </MenuItem>)}
                                    </Select>
                                </FormControl>}
                                <TextField value={messageType}
                                    label="Message Type"
                                    variant="standard"
                                    fullWidth
                                    required
                                    onChange={event => setMessageType(event.target.value)} />
                                <TextField value={messageContent}
                                    label="Message Content"
                                    variant="standard"
                                    multiline
                                    rows={10}
                                    fullWidth
                                    required
                                    onChange={event => setMessageContent(event.target.value)} />
                                <Button variant="contained" color="primary" sx={{ marginTop: 3 }} type="submit">
                                    <Save /> Save
                                </Button>
                                <Button variant="contained" color="secondary" sx={{ marginTop: 3, marginLeft: 1 }} type="button" onClick={() => navigateTo(-1)}>
                                    <ArrowBack /> Go Back
                                </Button>
                            </Box>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>}
        </>
    );
}

export default EventBusMessageForm;