import { useEffect, useState } from "react";
import GetEventbusQueueResponse from "../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import { EventBusQueueService } from "../services/eventbus-queue-service";
import GetEventBusQueueListRequest from "../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";
import EventBusQueueCard from "../components/eventbus-queue-card";
import { Backdrop, CircularProgress, Grid, Paper, Typography } from "@mui/material";
import AppBreadcrumbItem from "../interfaces/app-breadcrumb-item";
import { HomeOutlined } from "@mui/icons-material";
import AppBreadcrumb from "../components/app-breadcrumb";

const Home = () => {
    const [queues, setQueues] = useState<GetEventbusQueueResponse[]>([]);
    const queueService = new EventBusQueueService();
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

    const [isLoading, setLoading] = useState(true);

    useEffect(() => {
        buildbreadcrumb();

        queueService.ListQueues(listRequest).then(response => {
            console.log(response);
            const apiResponse = response.data;

            if (apiResponse.data) {
                const queueList = apiResponse.data;
                console.log(queueList);
                setQueues(queueList);
                console.log(queues);
            }

            setLoading(false);
        });
    }, []);

    return (
        <>
            <Backdrop open={isLoading}>
                <CircularProgress color="inherit" />
            </Backdrop>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            <Grid container justifyContent='center' spacing={2}>
                {queues && queues.map(queue => <Grid key={queue.id} item xs={6} md={4}>
                    <EventBusQueueCard queue={queue} showDescription={false} showSummarization={false} />
                </Grid>)}
            </Grid>
        </>
    );
}

export default Home;