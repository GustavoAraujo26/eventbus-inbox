import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { HomeOutlined, Apps, ArrowBack, Edit } from "@mui/icons-material";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import GetEventbusMessageResponse from "../../interfaces/responses/eventbus-received-message/get-eventbus-message-response";
import { Box, Card, CardContent, CardHeader, Chip, Divider, Grid, SpeedDial, SpeedDialAction, SpeedDialIcon, Table, TableBody, TableCell, TableHead, TableRow } from "@mui/material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import EventBusMessageCard from "./eventbus-message-card";
import { useAppDispatch, useAppSelector } from "../../state/hooks/app-hooks";
import { RootState } from "../../state/app-store";
import { fetchEventBusMessage } from "../../state/slices/eventbus-message/eventbus-message-slice";

const EventBusMessageDetails = () => {
    const dispatch = useAppDispatch();
    const parameters = useParams();
    const navigateTo = useNavigate();

    const messageContaienr = useAppSelector((state: RootState) => state.eventbusMessage);

    const [eventbusMessage, setEventBusMessage] = useState<GetEventbusMessageResponse>();
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

    useEffect(() => {
        if (parameters.id) {
            dispatch(fetchEventBusMessage(parameters.id));
        }

        buildbreadcrumb();
    }, [parameters]);

    useEffect(() => {
        if (messageContaienr.data){
            setEventBusMessage(messageContaienr.data);
        }
    }, [messageContaienr]);

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
                <SpeedDial ariaLabel="Event bus message navigation" sx={{ position: 'fixed', bottom: 16, right: 16 }} icon={<SpeedDialIcon />}>
                    <SpeedDialAction icon={<ArrowBack />} tooltipTitle="Go Back" onClick={() => navigateTo(-1)} />
                    <SpeedDialAction icon={<Edit />} tooltipTitle="Edit" onClick={() => navigateTo(`/eventbus-messages/${eventbusMessage.requestId}`)} />
                </SpeedDial>
            </>}
        </>
    );
}

export default EventBusMessageDetails;