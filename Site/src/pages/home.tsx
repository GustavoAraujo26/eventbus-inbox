import { useEffect, useState } from "react";
import GetEventbusQueueResponse from "../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import { EventBusQueueService } from "../services/eventbus-queue-service";
import GetEventBusQueueListRequest from "../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";
import EventBusQueueCard from "./event-bus-queues/eventbus-queue-card";
import { Card, CardHeader, Grid } from "@mui/material";
import AppBreadcrumbItem from "../interfaces/app-breadcrumb-item";
import { HomeOutlined } from "@mui/icons-material";
import AppBreadcrumb from "../components/app-breadcrumb";
import { useAppDispatch, useAppSelector } from "../state/hooks/app-hooks";
import { RootState } from "../state/app-store";
import { fetchEventBusQueueList } from "../state/slices/eventbus-queue/eventbus-queue-list-slice";

const Home = () => {
    const dispatch = useAppDispatch();
    const [queues, setQueues] = useState<GetEventbusQueueResponse[]>([]);
    const queueService = new EventBusQueueService();

    const queueListContainer = useAppSelector((state: RootState) => state.eventbusQueueList);

    const listRequest: GetEventBusQueueListRequest = {
        nameMatch: null,
        descriptionMatch: null,
        status: null,
        page: 1,
        pageSize: 10,
        summarizeMessages: true
    }

    const [breadcrumbItems, setBreadcrumbItems] = useState<AppBreadcrumbItem[]>([]);
    const buildbreadcrumb = () => {
        const home: AppBreadcrumbItem = {
            id: 1,
            icon: <HomeOutlined sx={{ mr: 0.5 }} />,
            text: 'Home',
            goTo: '/',
            isPage: true
        };

        const newList: AppBreadcrumbItem[] = [home];

        setBreadcrumbItems(newList);
    }

    useEffect(() => {
        buildbreadcrumb();
        dispatch(fetchEventBusQueueList(listRequest));
    }, []);

    useEffect(() => {
        if (queueListContainer.data){
            setQueues(queueListContainer.data);
        }
    }, [queueListContainer]);

    return (
        <>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            <Grid container justifyContent='center' spacing={2}>
                <Grid item md={12}>
                    <Card>
                        <CardHeader title="Some registered queues" sx={{textAlign: 'center'}} />
                    </Card>
                </Grid>
                {queues && queues.map(queue => <Grid key={queue.id} item xs={6} md={4}>
                    <EventBusQueueCard queue={queue} showDescription={false} showSummarization={false} showNavigation={true} />
                </Grid>)}
            </Grid>
        </>
    );
}

export default Home;