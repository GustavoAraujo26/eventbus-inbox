import { useNavigate, useParams } from "react-router-dom";
import { EventBusMessageService } from "../../services/eventbus-message-service";
import { useEffect, useState } from "react";
import AppSnackbarResponse from "../../interfaces/requests/app-snackbar-response";
import { HomeOutlined, Apps, AssignmentInd, Code, Today, Queue, GridOn, Task, ArrowBack, Edit } from "@mui/icons-material";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import GetEventbusMessageResponse from "../../interfaces/responses/eventbus-received-message/get-eventbus-message-response";
import { Avatar, Backdrop, Box, Card, CardContent, CardHeader, Chip, CircularProgress, Divider, Grid, List, ListItem, ListItemAvatar, ListItemText, Paper, SpeedDial, SpeedDialAction, SpeedDialIcon, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Tooltip, Typography } from "@mui/material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import AppSnackBar from "../../components/app-snackbar";
import EventBusMessageStatus from "./eventbus-message-status";

const EventBusMessageDetails = () => {
    const messageService = new EventBusMessageService();
    const parameters = useParams();
    const navigateTo = useNavigate();

    const [isLoading, setLoading] = useState(false);

    const [snackbarResponse, setSnackbarResponse] = useState<AppSnackbarResponse>();

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
        setLoading(true);

        messageService.GetMessage(parameterId).then(response => {
            setLoading(false);
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

    useEffect(() => {
        if (parameters.id) {
            getEventBusMessage(parameters.id);
        }

        buildbreadcrumb();
    }, [parameters]);

    return (
        <>
            <Backdrop open={isLoading}>
                <CircularProgress color="inherit" />
            </Backdrop>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            {eventbusMessage && <>
                <Grid justifyContent="center" spacing={2} container>
                    <Grid item md={6}>
                        <Card>
                            <CardHeader title="Message Details" sx={{ textAlign: 'center' }} />
                            <CardContent>
                                <Divider />
                                <List>
                                    <ListItem>
                                        <ListItemAvatar>
                                            <Tooltip title="Request Id">
                                                <Avatar>
                                                    <AssignmentInd />
                                                </Avatar>
                                            </Tooltip>
                                        </ListItemAvatar>
                                        <ListItemText primary={eventbusMessage.requestId} />
                                    </ListItem>
                                </List>
                                <Divider />
                                <List>
                                    <ListItem>
                                        <ListItemAvatar>
                                            <Tooltip title="Type">
                                                <Avatar>
                                                    <Code />
                                                </Avatar>
                                            </Tooltip>
                                        </ListItemAvatar>
                                        <ListItemText primary={eventbusMessage.type} />
                                    </ListItem>
                                    <ListItem>
                                        <ListItemAvatar>
                                            <Tooltip title="Creation Date">
                                                <Avatar>
                                                    <Today />
                                                </Avatar>
                                            </Tooltip>
                                        </ListItemAvatar>
                                        <ListItemText primary={(new Date(eventbusMessage.createdAt).toLocaleDateString())} />
                                    </ListItem>
                                    <ListItem>
                                        <ListItemAvatar>
                                            <Tooltip title="Queue">
                                                <Avatar>
                                                    <Queue />
                                                </Avatar>
                                            </Tooltip>
                                        </ListItemAvatar>
                                        <ListItemText primary={eventbusMessage.queue.name} />
                                    </ListItem>
                                    <ListItem>
                                        <ListItemAvatar>
                                            <Tooltip title="Status">
                                                <Avatar>
                                                    <GridOn />
                                                </Avatar>
                                            </Tooltip>
                                        </ListItemAvatar>
                                        <EventBusMessageStatus status={eventbusMessage.status} />
                                    </ListItem>
                                    <ListItem>
                                        <ListItemAvatar>
                                            <Tooltip title="Processing Attempts">
                                                <Avatar>
                                                    <Task />
                                                </Avatar>
                                            </Tooltip>
                                        </ListItemAvatar>
                                        {eventbusMessage.processingAttempts > eventbusMessage.queue.processingAttempts && <Chip label={processingAttempts} color="error" />}
                                        {(eventbusMessage.processingAttempts <= eventbusMessage.queue.processingAttempts) && eventbusMessage.status.intKey === 1 &&
                                            <Chip label={processingAttempts} color="primary" variant="outlined" />}
                                        {(eventbusMessage.processingAttempts <= eventbusMessage.queue.processingAttempts) && eventbusMessage.status.intKey === 2 &&
                                            <Chip label={processingAttempts} color="success" />}
                                        {(eventbusMessage.processingAttempts <= eventbusMessage.queue.processingAttempts) && eventbusMessage.status.intKey === 3 &&
                                            <Chip label={processingAttempts} color="error" variant="outlined" />}
                                        {(eventbusMessage.processingAttempts <= eventbusMessage.queue.processingAttempts) && eventbusMessage.status.intKey === 4 &&
                                            <Chip label={processingAttempts} color="error" />}
                                    </ListItem>
                                </List>
                            </CardContent>
                        </Card>
                    </Grid>
                    <Grid item md={6}>
                        <Card>
                            <CardHeader title="Message Content" sx={{ textAlign: 'center' }} />
                            <CardContent>
                                <Divider />
                                <Box sx={{ padding: 1 }}>
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
            <AppSnackBar response={snackbarResponse} />
        </>
    );
}

export default EventBusMessageDetails;