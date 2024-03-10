import { useEffect, useState } from "react";
import GetEventbusQueueResponse from "../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import { Add, Apps, Delete, Edit, HomeOutlined, Info, Lock, LockOpen } from "@mui/icons-material";
import { Fab, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import EventBusQueueStatus from "./eventbus-queue-status";
import AppPagination from "../../components/app-pagination";
import { useNavigate } from "react-router-dom";
import EventBusQueueModal from "./eventbus-queue-modal";
import { AppActionType } from "../../enums/app-action-type";
import EventBusQueueSearchCard from "./event-bus-queue-search-card";
import { useAppDispatch, useAppSelector } from "../../state/hooks/app-hooks";
import { openEventBusQueueModal } from "../../state/slices/eventbus-queue/eventbus-queue-modal-slice";
import { RootState } from "../../state/app-store";
import { fetchEventBusQueueList } from "../../state/slices/eventbus-queue/eventbus-queue-list-slice";
import { setEventBusQueueListPagination } from "../../state/slices/eventbus-queue/eventbus-queue-list-request-slice";

const EventBusQueuesDashboard = () => {
    const navigateTo = useNavigate();
    const dispatch = useAppDispatch();

    const [queues, setQueues] = useState<GetEventbusQueueResponse[]>([]);
    const [enableNextPage, setNextPageEnabled] = useState(true);
    const [breadcrumbItems, setBreadcrumbItems] = useState<AppBreadcrumbItem[]>([]);

    const currentRequest = useAppSelector((state: RootState) => state.eventbusQueueListRequest);
    const queueListContainer = useAppSelector((state: RootState) => state.eventbusQueueList);

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

    const changePageData = (selectedPage: number, selectedPageSize: number) => {
        dispatch(setEventBusQueueListPagination({ page: selectedPage, pageSize: selectedPageSize }));
    }

    useEffect(() => {
        buildbreadcrumb();
        dispatch(fetchEventBusQueueList(currentRequest));
    }, []);

    useEffect(() => {
        if (queueListContainer.data){
            setQueues(queueListContainer.data);
            setNextPageEnabled(queueListContainer.data.length >= 10);
        }
    }, [queueListContainer]);

    useEffect(() => {
        dispatch(fetchEventBusQueueList(currentRequest));
    }, [currentRequest.page, currentRequest.pageSize])

    return (
        <>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            <EventBusQueueSearchCard/>
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
                                <IconButton aria-label="Change Status" size="small" color="warning" title="Change status" onClick={() => dispatch(openEventBusQueueModal({
                                    queue: item,
                                    action: AppActionType.Update
                                }))}>
                                    {item.status.intKey == 1 ? <Lock /> : <LockOpen />}
                                </IconButton>
                                <IconButton aria-label="Delete" size="small" color="error" title="Delete" onClick={() => dispatch(openEventBusQueueModal({
                                    queue: item,
                                    action: AppActionType.Delete
                                }))}>
                                    <Delete />
                                </IconButton>
                            </TableCell>
                        </TableRow>)}
                    </TableBody>
                </Table>
                <AppPagination changePageData={changePageData} enableNextPage={enableNextPage} 
                    pageData={{currentPage: currentRequest.page, currentPageSize: currentRequest.pageSize}} />
            </TableContainer>
            <Fab color="info" sx={{ margin: 0, top: 'auto', right: 20, bottom: 20, left: 'auto', position: 'fixed' }} onClick={() => navigateTo("/eventbus-queues/new")}>
                <Add />
            </Fab>
            <EventBusQueueModal />
        </>
    );
}

export default EventBusQueuesDashboard;