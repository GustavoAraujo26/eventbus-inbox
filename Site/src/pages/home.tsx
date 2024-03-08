import { useEffect, useState } from "react";
import GetEventbusQueueResponse from "../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import { EventBusQueueService } from "../services/eventbus-queue-service";
import GetEventBusQueueListRequest from "../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";
import EventBusQueueCard from "./event-bus-queues/eventbus-queue-card";
import { Backdrop, CircularProgress, Grid, Paper, Typography } from "@mui/material";
import AppBreadcrumbItem from "../interfaces/app-breadcrumb-item";
import { HomeOutlined } from "@mui/icons-material";
import AppBreadcrumb from "../components/app-breadcrumb";
import AppSnackbarResponse from "../interfaces/requests/app-snackbar-response";
import { useDispatch } from "react-redux";
import { showSnackbar } from "../state/slices/app-snackbar-slice";
import { closeBackdrop, showBackdrop } from "../state/slices/app-backdrop-slice";

const Home = () => {
    const dispatch = useDispatch();
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

    useEffect(() => {
        buildbreadcrumb();

        dispatch(showBackdrop());

        queueService.ListQueues(listRequest).then(response => {
            dispatch(closeBackdrop());

            const apiResponse = response.data;

            if (apiResponse.isSuccess) {
                const queueList = apiResponse.data;
                console.log(queueList);
                setQueues(queueList);
                console.log(queues);
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
        })
        .catch(error => {
            dispatch(closeBackdrop());
            
            let response: AppSnackbarResponse = {
                success: false,
                message: error.toString().substring(0, 50)
            }

            const apiResponse = error.response.data;
            if (typeof apiResponse !== 'undefined'){
                response.message = apiResponse.message;
                response.stackTrace = apiResponse.stackTrace;
                response.statusCode = apiResponse.status;
            }

            dispatch(showSnackbar(response));
        });
    }, []);

    return (
        <>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            <Grid container justifyContent='center' spacing={2}>
                {queues && queues.map(queue => <Grid key={queue.id} item xs={6} md={4}>
                    <EventBusQueueCard queue={queue} showDescription={false} showSummarization={false} showNavigation={true} />
                </Grid>)}
            </Grid>
        </>
    );
}

export default Home;