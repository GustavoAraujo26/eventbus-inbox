import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import { HomeOutlined, Apps, Info, ArrowBack, Edit } from "@mui/icons-material";
import { Card, CardContent, CardHeader, Divider, Grid, SpeedDial, SpeedDialAction, SpeedDialIcon } from "@mui/material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import GetEventbusQueueResponse from "../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import EventBusQueueCard from "./eventbus-queue-card";
import EventBusMessageTable from "../../components/eventbus-message-table";
import { showBackdrop } from "../../state/slices/app-backdrop-slice";
import { useAppDispatch, useAppSelector } from "../../state/hooks/app-hooks";
import { RootState } from "../../state/app-store";
import { fetchEventBusQueue } from "../../state/slices/eventbus-queue/eventbus-queue-slice";
import { fetchEventBusMessageList } from "../../state/slices/eventbus-message/eventbus-message-list-slice";
import { setEventBusMessageListRequest } from "../../state/slices/eventbus-message/eventbus-message-list-request-slice";

const EventBusQueueDetails = () => {
    const dispatch = useAppDispatch();
    const parameters = useParams();
    const navigateTo = useNavigate();

    const queueContainer = useAppSelector((state: RootState) => state.eventbusQueue);
    const messageRequest = useAppSelector((state: RootState) => state.eventbusMessageListRequest);

    const [currentQueue, setCurrentQueue] = useState<GetEventbusQueueResponse>();
    const [breadcrumbItems, setBreadcrumbItems] = useState<AppBreadcrumbItem[]>([]);

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
            icon: <Apps sx={{ mr: 0.5 }} />,
            text: 'Event Bus Queues Dashboard',
            goTo: '/eventbus-queues/dashboard',
            isPage: false
        };

        const queueForm: AppBreadcrumbItem = {
            id: 3,
            icon: <Info />,
            text: `Details for event Bus queue ${parameters.id}`,
            goTo: '',
            isPage: true
        };

        const newList: AppBreadcrumbItem[] = [home, queueDashboard, queueForm];

        setBreadcrumbItems(newList);
    }

    useEffect(() => {
        if (parameters.id) {
            dispatch(setEventBusMessageListRequest({
                queueId: parameters.id,
                page: 1,
                pageSize: 10,
                creationDateSearch: null,
                updateDateSearch: null,
                statusToSearch: null,
                typeMatch: null
            }));
            dispatch(fetchEventBusQueue({
                id: parameters.id,
                summarizeMessages: true
            }));
        }

        buildbreadcrumb();
    }, [parameters]);

    useEffect(() => {
        if (queueContainer.data){
            setCurrentQueue(queueContainer.data);
        }
    }, [queueContainer]);

    useEffect(() => {
        dispatch(fetchEventBusMessageList(messageRequest));
    }, [messageRequest]);

    return (
        <>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            <Grid container justifyContent="center" spacing={2}>
                {currentQueue && <>
                    <Grid item md={4}>
                        <EventBusQueueCard queue={currentQueue!} showDescription={true} showSummarization={true} showNavigation={false} />
                    </Grid>
                    <Grid item md={8}>
                        <Card>
                            <CardHeader title="Queue Messages" sx={{ fontWeight: 'bold', textAlign: 'center' }} />
                            <CardContent>
                                <Divider />
                                <EventBusMessageTable gridSize={12} showQueue={false} showFilter={false} showActions={false} />
                            </CardContent>
                        </Card>
                    </Grid>
                </>}
            </Grid>
            <SpeedDial ariaLabel="Event bus message navigation" sx={{ position: 'fixed', bottom: 16, right: 16 }} icon={<SpeedDialIcon />}>
                <SpeedDialAction icon={<ArrowBack />} tooltipTitle="Go Back" onClick={() => navigateTo(-1)} />
                <SpeedDialAction icon={<Edit />} tooltipTitle="Edit" onClick={() => navigateTo(`/eventbus-queues/${currentQueue!.id}`)} />
            </SpeedDial>
        </>
    );
}

export default EventBusQueueDetails;