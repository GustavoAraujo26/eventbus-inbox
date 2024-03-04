import { useEffect, useState } from "react";
import GetEventbusQueueResponse from "../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import { EventBusQueueService } from "../../services/eventbus-queue-service";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import { Add, Apps, Delete, Edit, HomeOutlined } from "@mui/icons-material";
import { Backdrop, Card, CardContent, CircularProgress, Fab, IconButton, Pagination, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TablePagination, TableRow, Typography } from "@mui/material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import GetEventBusQueueListRequest from "../../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";
import EventBusQueueStatus from "../../components/eventbus-queue-status";
import AppPagination from "../../components/app-pagination";
import { useNavigate } from "react-router-dom";

const EventBusQueuesDashboard = () => {
    const navigateTo = useNavigate();
    const [queues, setQueues] = useState<GetEventbusQueueResponse[]>([]);
    const queueService = new EventBusQueueService();

    const [currentPage, setCurrentPage] = useState(1);
    const [currentPageSize, setCurrentPageSize] = useState(10);
    const [rowsFounded, setRowsFounded] = useState(true);
    const [nameMatch, setNameMatch] = useState('');
    const [descriptionMatch, setdescriptionMatch] = useState('');
    const [status, setStatus] = useState<number | null>(null);

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

    const [isLoading, setLoading] = useState(true);

    const getPaginatedList = () => {
        const listRequest: GetEventBusQueueListRequest = {
            nameMatch: nameMatch,
            descriptionMatch: descriptionMatch,
            status: status,
            page: currentPage!,
            pageSize: currentPageSize!,
            summarizeMessages: false
        }

        queueService.ListQueues(listRequest).then(response => {
            const apiResponse = response.data;

            if (apiResponse.data) {
                const queueList = apiResponse.data;
                setQueues(queueList);
                setRowsFounded((queueList.length > 0 && queueList.length >= currentPageSize));
            }

            setLoading(false);
        });
    }

    useEffect(() => {
        buildbreadcrumb();
        getPaginatedList();
    }, []);

    useEffect(() => {
        setLoading(true);
        getPaginatedList();
    }, [currentPage, currentPageSize])

    const changePageData = (selectedPage: number, selectedPageSize: number) => {
        setCurrentPage(selectedPage);
        setCurrentPageSize(selectedPageSize);
    }

    return (
        <>
            <Backdrop open={isLoading}>
                <CircularProgress color="inherit" />
            </Backdrop>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell align="left">#</TableCell>
                            <TableCell align="left">Name</TableCell>
                            <TableCell align="left">Processing Attempts</TableCell>
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
                            <TableCell align="left">{item.processingAttempts}</TableCell>
                            <TableCell align="left"><EventBusQueueStatus status={item.status} /></TableCell>
                            <TableCell align="left">
                                <IconButton aria-label="Edit" size="small" color="info" onClick={() => navigateTo(`/eventbus-queues/${item.id}`)}>
                                    <Edit/>
                                </IconButton>
                                <IconButton aria-label="Delete" size="small" color="error">
                                    <Delete/>
                                </IconButton>
                            </TableCell>
                        </TableRow>)}
                    </TableBody>
                </Table>
                <AppPagination changePageData={changePageData} rowsFounded={rowsFounded} />
            </TableContainer>
            <Fab color="info" sx={{margin: 0, top: 'auto', right: 20, bottom: 20, left: 'auto', position: 'fixed'}} onClick={() => navigateTo("/eventbus-queues/new")}>
                <Add/>
            </Fab>
        </>
    );
}

export default EventBusQueuesDashboard;