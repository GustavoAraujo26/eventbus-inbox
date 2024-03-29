import { useEffect, useState } from "react";
import GetEventbusQueueResponse from "../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import AppSnackbarResponse from "../../interfaces/requests/app-snackbar-response";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import { Box, Button, Card, CardContent, FormControl, Grid, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import { HomeOutlined, Send, ArrowBack } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";
import SendEventbusMessageRequest from "../../interfaces/requests/eventbus-sender/send-eventbus-message-request";
import { v4 as uuidv4 } from 'uuid';
import { EventBusSenderService } from "../../services/event-bus-sender-service";
import { showSnackbar } from "../../state/slices/app-snackbar-slice";
import { closeBackdrop, showBackdrop } from "../../state/slices/app-backdrop-slice";
import { useAppDispatch, useAppSelector } from "../../state/hooks/app-hooks";
import { RootState } from "../../state/app-store";
import { fetchEventBusQueueList } from "../../state/slices/eventbus-queue/eventbus-queue-list-slice";

const EventBusMessageSender = () => {
    const dispatch = useAppDispatch();
    const navigateTo = useNavigate();

    const senderService = new EventBusSenderService();

    const [queueList, setQueueList] = useState<GetEventbusQueueResponse[]>([]);
    const [breadcrumbItems, setBreadcrumbItems] = useState<AppBreadcrumbItem[]>([]);

    const [queueId, setQueueId] = useState('');
    const [messageType, setMessageType] = useState('');
    const [messageContent, setMessageContent] = useState('');

    const queueListContainer = useAppSelector((state: RootState) => state.eventbusQueueList);

    const buildbreadcrumb = () => {
        const home: AppBreadcrumbItem = {
            id: 1,
            icon: <HomeOutlined sx={{ mr: 0.5 }} />,
            text: 'Home',
            goTo: '/',
            isPage: false
        };

        const queueDashboard: AppBreadcrumbItem = {
            id: 2,
            icon: <Send sx={{ mr: 0.5 }} />,
            text: 'Sending message to event bus queue',
            goTo: '',
            isPage: true
        };

        const newList: AppBreadcrumbItem[] = [home, queueDashboard];

        setBreadcrumbItems(newList);
    }

    useEffect(() => {
        buildbreadcrumb();
        dispatch(fetchEventBusQueueList(null));
    }, []);

    useEffect(() => {
        if (queueListContainer.data){
            setQueueList(queueListContainer.data);
        }
    }, [queueListContainer]);

    const onSubmitMessage = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        dispatch(showBackdrop());

        const request: SendEventbusMessageRequest = {
            requestId: uuidv4(),
            createdAt: new Date(),
            type: messageType,
            content: messageContent,
            queueId: queueId
        };

        const apiResponse = await senderService.SendMessage(request);

        dispatch(closeBackdrop());

        if (apiResponse === null){
            return;
        }

        if (apiResponse.isSuccess){
            navigateTo(-1);
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
    }

    return (
        <>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            <Grid justifyContent="center" container spacing={2}>
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
                                    <Send /> Send
                                </Button>
                                <Button variant="contained" color="secondary" sx={{ marginTop: 3, marginLeft: 1 }} type="button" onClick={() => navigateTo(-1)}>
                                    <ArrowBack /> Go Back
                                </Button>
                            </Box>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>
        </>
    );
}

export default EventBusMessageSender;