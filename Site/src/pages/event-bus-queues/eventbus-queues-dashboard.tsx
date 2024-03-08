import { useEffect, useState } from "react";
import GetEventbusQueueResponse from "../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import { EventBusQueueService } from "../../services/eventbus-queue-service";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import { Add, Apps, Delete, Edit, HomeOutlined, Info, Lock, LockOpen } from "@mui/icons-material";
import { Backdrop, CircularProgress, Fab, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import GetEventBusQueueListRequest from "../../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";
import EventBusQueueStatus from "./eventbus-queue-status";
import AppPagination from "../../components/app-pagination";
import { useNavigate } from "react-router-dom";
import EventBusQueueModal from "./eventbus-queue-modal";
import { AppActionType } from "../../enums/app-action-type";
import AppSnackbarResponse from "../../interfaces/requests/app-snackbar-response";
import EventBusQueueSearchCard from "./event-bus-queue-search-card";
import EnumData from "../../interfaces/enum-data";
import { EnumsService } from "../../services/enums-service";
import { useDispatch } from "react-redux";
import { showSnackbar } from "../../state/slices/app-snackbar-slice";
import { closeBackdrop, showBackdrop } from "../../state/slices/app-backdrop-slice";

const EventBusQueuesDashboard = () => {
    const navigateTo = useNavigate();
    const dispatch = useDispatch();
    const [queues, setQueues] = useState<GetEventbusQueueResponse[]>([]);
    const [statusList, setStatusList] = useState<EnumData[]>([]);
    const queueService = new EventBusQueueService();
    const enumService = new EnumsService();

    const [currentPage, setCurrentPage] = useState(1);
    const [currentPageSize, setCurrentPageSize] = useState(10);
    const [rowsFounded, setRowsFounded] = useState(true);
    const [nameMatch, setNameMatch] = useState('');
    const [descriptionMatch, setdescriptionMatch] = useState('');
    const [status, setStatus] = useState<number | null>(null);

    const [selectedQueue, setSelectedQueue] = useState<GetEventbusQueueResponse>();
    const [showModal, setShowModal] = useState(false);
    const [modalAction, setModalAction] = useState<AppActionType>(AppActionType.View);

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
            goTo: '',
            isPage: true
        };

        const newList: AppBreadcrumbItem[] = [home, queueDashboard];

        setBreadcrumbItems(newList);
    }

    const getPaginatedList = () => {
        dispatch(showBackdrop());

        const listRequest: GetEventBusQueueListRequest = {
            nameMatch: nameMatch,
            descriptionMatch: descriptionMatch,
            status: status,
            page: currentPage!,
            pageSize: currentPageSize!,
            summarizeMessages: false
        }

        queueService.ListQueues(listRequest).then(response => {
            dispatch(closeBackdrop());

            const apiResponse = response.data;

            if (apiResponse.isSuccess) {
                const queueList = apiResponse.data;
                setQueues(queueList);
                setRowsFounded((apiResponse.data.length > 0 && apiResponse.data.length >= currentPageSize));
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

    const getStatusList = () => {
        dispatch(showBackdrop());

        enumService.ListQueueStatus().then(response => {
            dispatch(closeBackdrop());
            
            const apiResponse = response.data;

            if (apiResponse.isSuccess) {
                const queueStatusList = apiResponse.data;
                setStatusList(queueStatusList);
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

    useEffect(() => {
        buildbreadcrumb();
        getStatusList();
        getPaginatedList();
    }, []);

    useEffect(() => {
        getPaginatedList();
    }, [currentPage, currentPageSize])

    const changePageData = (selectedPage: number, selectedPageSize: number) => {
        setCurrentPage(selectedPage);
        setCurrentPageSize(selectedPageSize);
    }

    const selectQueue = (currentQueue: GetEventbusQueueResponse, action: AppActionType) => {
        setSelectedQueue(currentQueue);
        setModalAction(action);
        setShowModal(true);
    }

    const closeStatusModal = () => {
        setShowModal(false);
    }

    const searchQueues = (name: string, description: string, currentStatus: number) => {
        setNameMatch(name);
        setdescriptionMatch(description);
        setStatus(currentStatus === 0 ? null : currentStatus);
    }

    useEffect(() => {
        getPaginatedList();
    }, [nameMatch, descriptionMatch, status]);

    return (
        <>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            <EventBusQueueSearchCard statusList={statusList} searchQueues={searchQueues}/>
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell align="left">#</TableCell>
                            <TableCell align="left">Name</TableCell>
                            <TableCell align="center">Processing Attempts</TableCell>
                            <TableCell align="left">Status</TableCell>
                            <TableCell align="left">Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {queues.length !== 0 ? null : <TableRow>
                            <TableCell rowSpan={5}>
                                <Typography>No queues found!</Typography>
                            </TableCell>
                        </TableRow>}
                        {queues && queues.map(item => <TableRow key={item.id}>
                            <TableCell align="left">{item.id}</TableCell>
                            <TableCell align="left">{item.name}</TableCell>
                            <TableCell align="center">{item.processingAttempts}</TableCell>
                            <TableCell align="left"><EventBusQueueStatus status={item.status} /></TableCell>
                            <TableCell align="left">
                                <IconButton aria-label="Details" size="small" color="info" onClick={() => navigateTo(`/eventbus-queues/details/${item.id}`)} title="Details">
                                    <Info />
                                </IconButton>
                                <IconButton aria-label="Edit" size="small" color="success" onClick={() => navigateTo(`/eventbus-queues/${item.id}`)} title="Edit">
                                    <Edit />
                                </IconButton>
                                <IconButton aria-label="Edit" size="small" color="warning" title="Change status" onClick={() => selectQueue(item, AppActionType.Update)}>
                                    {item.status.intKey == 1 ? <Lock /> : <LockOpen />}
                                </IconButton>
                                <IconButton aria-label="Delete" size="small" color="error" title="Delete" onClick={() => selectQueue(item, AppActionType.Delete)}>
                                    <Delete />
                                </IconButton>
                            </TableCell>
                        </TableRow>)}
                    </TableBody>
                </Table>
                <AppPagination changePageData={changePageData} enableNextPage={rowsFounded} loadData={() => getPaginatedList()} />
            </TableContainer>
            <Fab color="info" sx={{ margin: 0, top: 'auto', right: 20, bottom: 20, left: 'auto', position: 'fixed' }} onClick={() => navigateTo("/eventbus-queues/new")}>
                <Add />
            </Fab>
            <EventBusQueueModal queue={selectedQueue} showModal={showModal} closeModal={closeStatusModal} action={modalAction} updateList={getPaginatedList} />
        </>
    );
}

export default EventBusQueuesDashboard;