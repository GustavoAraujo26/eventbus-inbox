import { useEffect, useState } from "react";
import Period from "../../../interfaces/period";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import GetEventbusMessageListRequest from "../../../interfaces/requests/eventbus-received-message/get-eventbus-message-list-request";
import { EventBusMessageService } from "../../../services/eventbus-message-service";
import GetEventbusMessageListResponse from "../../../interfaces/responses/eventbus-received-message/get-eventbus-message-list-response";
import { Backdrop, CircularProgress, Grid, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import AppSnackBar from "../../../components/app-snackbar";
import EventBusMessageStatus from "../eventbus-message-status";
import AppPagination from "../../../components/app-pagination";
import EventBusMessageTableFilter from "../eventbus-message-table-filter";
import { Info } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";

interface MessageTableProps {
    gridSize: number,
    showQueue: boolean,
    showFilter: boolean,
    currentQueueId: string | null
}

const EventBusMessageTable = ({ gridSize, showQueue, showFilter, currentQueueId }: MessageTableProps) => {
    const navigateTo = useNavigate();
    const messageService = new EventBusMessageService();
    const [isLoading, setLoading] = useState(false);
    const [snackbarResponse, setSnackbarResponse] = useState<AppSnackbarResponse>();
    const [queueMessages, setQueueMessages] = useState<GetEventbusMessageListResponse[]>([]);

    const [currentPage, setCurrentPage] = useState(1);
    const [currentPageSize, setCurrentPageSize] = useState(10);
    const [rowsFounded, setRowsFounded] = useState(true);

    const [queueId, setQueueId] = useState<string | null>(null);
    const [creationDateSearch, setCreationDateSearch] = useState<Period | null>(null);
    const [updateDateSearch, setUpdateDateSearch] = useState<Period | null>(null);
    const [typeMatch, setTypeMatch] = useState<string | null>(null);
    const [statusToSearch, setStatusToSearch] = useState<number[] | null>(null);

    const getEventBusMessages = () => {
        setLoading(true);
        const request: GetEventbusMessageListRequest = {
            queueId: currentQueueId ? currentQueueId : queueId,
            creationDateSearch: creationDateSearch,
            updateDateSearch: updateDateSearch,
            typeMatch: typeMatch,
            statusToSearch: statusToSearch,
            page: currentPage,
            pageSize: currentPageSize
        };

        messageService.ListMessage(request).then(response => {
            setLoading(false);
            const apiResponse = response.data;
            if (apiResponse.isSuccess) {
                setQueueMessages(apiResponse.data);
                setRowsFounded((queueMessages.length > 0 && queueMessages.length >= currentPageSize));
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

    const changePageData = (selectedPage: number, selectedPageSize: number) => {
        setCurrentPage(selectedPage);
        setCurrentPageSize(selectedPageSize);
    }

    const executeFilter = (selectedQueue: string | null, selectedCreationDateSearch: Period | null,
        selectedUpdateDateSearch: Period | null, selectedTypeMatch: string | null, selectedStatus: number[] | null) => {
        setQueueId(selectedQueue);
        setCreationDateSearch(selectedCreationDateSearch);
        setUpdateDateSearch(selectedUpdateDateSearch);
        setTypeMatch(selectedTypeMatch);
        setStatusToSearch(selectedStatus);
    }

    useEffect(() => {
        getEventBusMessages();
    }, []);

    useEffect(() => {
        getEventBusMessages();
    }, [currentPage, currentPageSize]);

    useEffect(() => {
        getEventBusMessages();
    }, [queueId, creationDateSearch, updateDateSearch, typeMatch, statusToSearch]);

    return (
        <>
            <Backdrop open={isLoading}>
                <CircularProgress color="inherit" />
            </Backdrop>
            {queueMessages && <Grid item md={gridSize}>
                {showFilter && <EventBusMessageTableFilter executeFilter={executeFilter} />}
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell align="left">Request Id</TableCell>
                                <TableCell align="left">Last Update</TableCell>
                                {showQueue && <TableCell align="left">Queue</TableCell>}
                                <TableCell align="left">Type</TableCell>
                                <TableCell align="left">Status</TableCell>
                                <TableCell align="left">Processing Attempts</TableCell>
                                <TableCell align="left">Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {queueMessages.length === 0 && <TableRow>
                                <TableCell rowSpan={7}>
                                    <Typography>No messages found!</Typography>
                                </TableCell>
                            </TableRow>}
                            {queueMessages.length > 0 && queueMessages.map(message => <TableRow key={message.requestId}>
                                <TableCell>{message.requestId}</TableCell>
                                <TableCell>{(new Date(message.lastUpdate.occurredAt).toLocaleDateString())}</TableCell>
                                {showQueue && <TableCell>{message.queue.name}</TableCell>}
                                <TableCell>{message.type}</TableCell>
                                <TableCell>
                                    <EventBusMessageStatus status={message.status} />
                                </TableCell>
                                <TableCell>{message.processingAttempts}</TableCell>
                                <TableCell>
                                    <IconButton aria-label="Details" size="small" color="info" onClick={() => navigateTo(`/eventbus-messages/details/${message.requestId}`)} title="Details">
                                        <Info />
                                    </IconButton>
                                </TableCell>
                            </TableRow>)}
                        </TableBody>
                    </Table>
                    <AppPagination changePageData={changePageData} rowsFounded={rowsFounded} />
                </TableContainer>
            </Grid>}
            <AppSnackBar response={snackbarResponse} />
        </>
    );
}

export default EventBusMessageTable;