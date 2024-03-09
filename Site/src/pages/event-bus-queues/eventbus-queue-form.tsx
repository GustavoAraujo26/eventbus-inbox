import { HomeOutlined, Apps, Add, Edit, Save, ArrowBack } from "@mui/icons-material";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import { Backdrop, Box, Button, Card, CardContent, CircularProgress, FormControlLabel, Grid, Switch, TextField } from "@mui/material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import { v4 as uuidv4 } from 'uuid';
import SaveEventbusQueueRequest from "../../interfaces/requests/eventbus-queue/save-eventbus-queue-request";
import { EventBusQueueService } from "../../services/eventbus-queue-service";
import GetEventBusQueueRequest from "../../interfaces/requests/eventbus-queue/get-eventbus-queue-request";
import AppSnackBar from "../../components/app-snackbar";
import AppSnackbarResponse from "../../interfaces/requests/app-snackbar-response";
import { useDispatch } from "react-redux";
import { showSnackbar } from "../../state/slices/app-snackbar-slice";
import { closeBackdrop, showBackdrop } from "../../state/slices/app-backdrop-slice";
import { useAppDispatch } from "../../state/hooks/app-hooks";

const EventBusQueueForm = () => {
    const dispatch = useAppDispatch();
    const queueService = new EventBusQueueService();
    const parameters = useParams();
    const navigateTo = useNavigate();

    const [id, setId] = useState('');
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [status, setStatus] = useState(1);
    const [processingAttempts, setProcessingAttempts] = useState(0);

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
            icon: parameters.id ? <Edit sx={{ mr: 0.5 }} /> : <Add sx={{ mr: 0.5 }} />,
            text: parameters.id ? `Edit event Bus queue ${parameters.id}` : 'Add new event bus queue',
            goTo: '',
            isPage: true
        };

        const newList: AppBreadcrumbItem[] = [home, queueDashboard, queueForm];

        setBreadcrumbItems(newList);
    }

    useEffect(() => {
        buildbreadcrumb();
    }, []);

    useEffect(() => {
        if (parameters.id){
            setId(parameters.id);
            getEventBusQueue(parameters.id);
        }
        else{
            setId(uuidv4());
        }
    }, [parameters]);

    const onSwitchChange = (event: React.ChangeEvent<HTMLInputElement>, checked: boolean) => {
        const statusId = checked ? 1 : 2;
        setStatus(statusId);
    }

    const onSubmitForm = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        dispatch(showBackdrop());

        const request: SaveEventbusQueueRequest = {
            id: id,
            name: name.toLowerCase(),
            description: description,
            status: status,
            processingAttempts: processingAttempts
        };

        queueService.SaveQueue(request).then(response => {
            dispatch(closeBackdrop());
            var apiResponse = response.data;

            if (apiResponse.isSuccess){
                navigateTo('/eventbus-queues/dashboard');
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
        }).catch(error => {
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
    }

    const getEventBusQueue = (parameterId: string) => {
        dispatch(showBackdrop());
        
        const request: GetEventBusQueueRequest = {
            id: parameterId,
            summarizeMessages: false
        }

        queueService.GetQueue(request).then(response => {
            dispatch(closeBackdrop());

            const apiResponse = response.data;

            if (apiResponse.isSuccess){
                setName(apiResponse.object.name);
                setDescription(apiResponse.object.description);
                setStatus(apiResponse.object.status.intKey);
                setProcessingAttempts(apiResponse.object.processingAttempts);
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
    }

    const cleanForm = () => {
        setName('');
        setDescription('');
        setStatus(1);
        setProcessingAttempts(0);
    }

    return (
        <>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            <Grid container justifyContent="center" spacing={2}>
                <Grid item md={8}>
                    <Card>
                        <CardContent>
                            <Box component="form" onSubmit={onSubmitForm}>
                                <TextField value={name} 
                                    label="Name" 
                                    variant="standard" 
                                    fullWidth 
                                    required 
                                    onChange={event => setName(event.target.value)}/>
                                <TextField value={description} 
                                    label="Description" 
                                    variant="standard" 
                                    multiline 
                                    rows={5} 
                                    fullWidth 
                                    onChange={event => setDescription(event.target.value)}/>
                                <FormControlLabel control={<Switch value={status === 1} onChange={onSwitchChange} checked={status === 1} />} label="Habilitado?"/>
                                <TextField value={processingAttempts} 
                                    label="Processing Attempts" 
                                    variant="standard" 
                                    fullWidth 
                                    required 
                                    type="number" 
                                    InputProps={{ inputProps: { min: 1 } }} 
                                    onChange={event => setProcessingAttempts(+event.target.value)}/>
                                <Button variant="contained" color="primary" sx={{ marginTop: 3 }} type="submit">
                                    <Save/> Save
                                </Button>
                                <Button variant="contained" color="secondary" sx={{ marginTop: 3, marginLeft: 1 }} type="button" onClick={() => navigateTo(-1)}>
                                    <ArrowBack/> Go Back
                                </Button>
                            </Box>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>
        </>
    );
}

export default EventBusQueueForm;