import { useNavigate, useParams } from "react-router-dom";
import { EventBusQueueService } from "../../services/eventbus-queue-service";
import { useEffect, useState } from "react";
import AppSnackbarResponse from "../../interfaces/requests/app-snackbar-response";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import { HomeOutlined, Apps, Edit, Add, Info } from "@mui/icons-material";
import { Backdrop, CircularProgress, Grid } from "@mui/material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import GetEventBusQueueRequest from "../../interfaces/requests/eventbus-queue/get-eventbus-queue-request";
import AppSnackBar from "../../components/app-snackbar";
import GetEventbusQueueResponse from "../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import EventBusQueueCard from "./eventbus-queue-card";
import { EventBusMessageService } from "../../services/eventbus-message-service";
import GetEventbusMessageListResponse from "../../interfaces/responses/eventbus-received-message/get-eventbus-message-list-response";
import GetEventbusMessageListRequest from "../../interfaces/requests/eventbus-received-message/get-eventbus-message-list-request";
import EventBusMessageTable from "../event-bus-messages/eventbus-message-table";

const EventBusQueueDetails = () => {
    const queueService = new EventBusQueueService();
    const parameters = useParams();
    const navigateTo = useNavigate();

    const [isLoading, setLoading] = useState(false);

    const [snackbarResponse, setSnackbarResponse] = useState<AppSnackbarResponse>();
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
            setLoading(true);
            getEventBusQueue(parameters.id);
        }

        buildbreadcrumb();
    }, [parameters]);

    const getEventBusQueue = (parameterId: string) => {
        const request: GetEventBusQueueRequest = {
            id: parameterId,
            summarizeMessages: true
        }

        queueService.GetQueue(request).then(response => {
            setLoading(false);

            const apiResponse = response.data;

            if (apiResponse.isSuccess) {
                setCurrentQueue(apiResponse.object);
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

    return (
        <>
            <Backdrop open={isLoading}>
                <CircularProgress color="inherit" />
            </Backdrop>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            <Grid container justifyContent="center" spacing={2}>
                {currentQueue && <>
                    <Grid item md={4}>
                        <EventBusQueueCard queue={currentQueue!} showDescription={true} showSummarization={true} showNavigation={false} />
                    </Grid>
                    <EventBusMessageTable gridSize={8} showQueue={false} showFilter={false} currentQueueId={currentQueue!.id} />
                </>}
            </Grid>
            <AppSnackBar response={snackbarResponse} />
        </>
    );
}

export default EventBusQueueDetails;