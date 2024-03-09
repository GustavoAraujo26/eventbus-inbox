import { useNavigate, useParams } from "react-router-dom";
import { EventBusMessageService } from "../../services/eventbus-message-service";
import { useEffect, useState } from "react";
import AppSnackbarResponse from "../../interfaces/requests/app-snackbar-response";
import { HomeOutlined, Apps, ArrowBack, Edit } from "@mui/icons-material";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import GetEventbusMessageResponse from "../../interfaces/responses/eventbus-received-message/get-eventbus-message-response";
import { Backdrop, Box, Card, CardContent, CardHeader, Chip, CircularProgress, Divider, Grid, SpeedDial, SpeedDialAction, SpeedDialIcon, Table, TableBody, TableCell, TableHead, TableRow } from "@mui/material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import EventBusMessageCard from "./eventbus-message-card";
import { useDispatch } from "react-redux";
import { showSnackbar } from "../../state/slices/app-snackbar-slice";
import { closeBackdrop, showBackdrop } from "../../state/slices/app-backdrop-slice";
import { useAppDispatch } from "../../state/hooks/app-hooks";

const EventBusMessageDetails = () => {
    const dispatch = useAppDispatch();
    const messageService = new EventBusMessageService();
    const parameters = useParams();
    const navigateTo = useNavigate();

    const [eventbusMessage, setEventBusMessage] = useState<GetEventbusMessageResponse>();

    const [processingAttempts, setProcessingAttempts] = useState('');

    const [breadcrumbItems, setBreadcrumbItems] = useState<AppBreadcrumbItem[]>([]);
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

        const messageDetails: AppBreadcrumbItem = {
            id: 3,
            icon: <Apps sx={{ mr: 0.5 }} />,
            text: `Details for event bus message ${parameters.id}`,
            goTo: '',
            isPage: true
        };

        const newList: AppBreadcrumbItem[] = [home, messageDashboard, messageDetails];

        setBreadcrumbItems(newList);
    }

    const getEventBusMessage = (parameterId: string) => {
        dispatch(showBackdrop());

        messageService.GetMessage(parameterId).then(response => {
            dispatch(closeBackdrop());
            const apiResponse = response.data;
            if (apiResponse.isSuccess) {
                setEventBusMessage(apiResponse.object);
                setProcessingAttempts(`${apiResponse.object.processingAttempts}/${apiResponse.object.queue.processingAttempts}`);
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
            getEventBusMessage(parameters.id);
        }

        buildbreadcrumb();
    }, [parameters]);

    return (
        <>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            {eventbusMessage && <>
                <Grid justifyContent="center" spacing={2} container>
                    <Grid item md={6}>
                        <EventBusMessageCard requestId={eventbusMessage.requestId} type={eventbusMessage.type} 
                            creationDate={eventbusMessage.createdAt} processingAttempts={eventbusMessage.processingAttempts} 
                            status={eventbusMessage.status} queueName={eventbusMessage.queue.name} 
                            queueProcessingAttempts={eventbusMessage.queue.processingAttempts}/>
                    </Grid>
                    <Grid item md={6}>
                        <Card>
                            <CardHeader title="Message Content" sx={{ textAlign: 'center' }} />
                            <CardContent>
                                <Divider />
                                <Box sx={{ padding: 1, maxHeight: '300px', overflow: 'auto' }}>
                                    <pre>{JSON.stringify(eventbusMessage.content, null, 2)}</pre>
                                </Box>
                            </CardContent>
                        </Card>
                    </Grid>
                    {eventbusMessage.processingHistory && <Grid item md={8}>
                        <Card>
                            <CardHeader title="Processing History" sx={{ textAlign: 'center' }} />
                            <CardContent>
                                <Divider />
                                <Table>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell>Occurred At</TableCell>
                                            <TableCell>Status</TableCell>
                                            <TableCell>Description</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {eventbusMessage.processingHistory.map(history => <TableRow key={eventbusMessage.processingHistory.indexOf(history)}>
                                            <TableCell>{(new Date(history.occurredAt).toLocaleDateString())}</TableCell>
                                            <TableCell>
                                                {(history.status.intKey >= 200 && history.status.intKey <= 299) && <Chip label={history.status.description} color="success" />}
                                                {(history.status.intKey >= 400 && history.status.intKey <= 499) && <Chip label={history.status.description} color="error" />}
                                                {(history.status.intKey >= 500 && history.status.intKey <= 599) && <Chip label={history.status.description} color="error" variant="outlined" />}
                                                {(history.status.intKey < 200 || (history.status.intKey > 299 && history.status.intKey < 400) || history.status.intKey > 599) &&
                                                    <Chip label={history.status.description} color="secondary" variant="outlined" />}
                                            </TableCell>
                                            <TableCell>{history.resultMessage}</TableCell>
                                        </TableRow>)}
                                    </TableBody>
                                </Table>
                            </CardContent>
                        </Card>
                    </Grid>}
                </Grid>
                <SpeedDial ariaLabel="Event bus message navigation" sx={{ position: 'absolute', bottom: 16, right: 16 }} icon={<SpeedDialIcon />}>
                    <SpeedDialAction icon={<ArrowBack />} tooltipTitle="Go Back" onClick={() => navigateTo(-1)} />
                    <SpeedDialAction icon={<Edit />} tooltipTitle="Edit" onClick={() => navigateTo(`/eventbus-messages/${eventbusMessage.requestId}`)} />
                </SpeedDial>
            </>}
        </>
    );
}

export default EventBusMessageDetails;