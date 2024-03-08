import { useEffect, useState } from "react";
import { Backdrop, CircularProgress, Grid, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import EventBusMessageTableFilter from "./eventbus-message-table-filter";
import { Delete, DomainVerification, Edit, Info, RestartAlt } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";
import { AppActionType } from "../../enums/app-action-type";
import Period from "../../interfaces/period";
import AppSnackbarResponse from "../../interfaces/requests/app-snackbar-response";
import GetEventbusMessageListRequest from "../../interfaces/requests/eventbus-received-message/get-eventbus-message-list-request";
import GetEventbusMessageListResponse from "../../interfaces/responses/eventbus-received-message/get-eventbus-message-list-response";
import EventBusMessageActionModal from "../../pages/event-bus-messages/eventbus-message-action-modal";
import EventBusMessageProcessAttemptModal from "../../pages/event-bus-messages/eventbus-message-process-attempt-modal";
import EventBusMessageStatus from "../../pages/event-bus-messages/eventbus-message-status";
import { EventBusMessageService } from "../../services/eventbus-message-service";
import AppPagination from "../app-pagination";
import AppSnackBar from "../app-snackbar";

interface MessageTableProps {
    gridSize: number,
    showQueue: boolean,
    showFilter: boolean,
    currentQueueId: string | null,
    showActions: boolean
}

const EventBusMessageTable = ({ gridSize, showQueue, showFilter, currentQueueId, showActions }: MessageTableProps) => {
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

    const [showStatusModal, setShowStatusModal] = useState(false);
    const [showActionModal, setShowActionModal] = useState(false);
    const [selectedStatusMessage, setSelectedStatusMessage] = useState<GetEventbusMessageListResponse | null>(null);
    const [selectedActionMessage, setSelectedActionMessage] = useState<GetEventbusMessageListResponse | null>(null);
    const [selectedActionType, setActionType] = useState<AppActionType | null>(null);

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
                setRowsFounded((apiResponse.data.length > 0 && apiResponse.data.length >= currentPageSize));
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
        setCurrentPage(1);
    }

    const closeStatusModal = () => {
        setShowStatusModal(false);
        setSelectedStatusMessage(null);
    }

    const closeActionModal = () => {
        setShowActionModal(false);
        setSelectedActionMessage(null);
        setActionType(null);
    }

    const selectStatusMessage = (currentMessage: GetEventbusMessageListResponse) => {
        setSelectedStatusMessage(currentMessage);
    }

    const selectActionMessage = (currentMessage: GetEventbusMessageListResponse, selectedAction: AppActionType) => {
        setSelectedActionMessage(currentMessage);
        setActionType(selectedAction);
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

    useEffect(() => {
        if (selectedStatusMessage === null) {
            setShowStatusModal(false);
        }
        else {
            setShowStatusModal(true);
        }
    }, [selectedStatusMessage]);

    useEffect(() => {
        if (selectedActionMessage === null) {
            setShowActionModal(false);
        }
        else {
            setShowActionModal(true);
        }
    }, [selectedActionMessage]);

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
                                <TableCell align="center">Attempts</TableCell>
                                {showActions && <TableCell align="left">Actions</TableCell>}
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
                                <TableCell align="center">{message.processingAttempts}</TableCell>
                                {showActions && <TableCell>
                                    <IconButton aria-label="Details" size="small" color="info" onClick={() => navigateTo(`/eventbus-messages/details/${message.requestId}`)} title="Details">
                                        <Info />
                                    </IconButton>
                                    {message.status.intKey != 2 && message.status.intKey != 4 && <IconButton aria-label="Processing Attempt"
                                        size="small" color="warning" title="Processing Attempt" onClick={() => selectStatusMessage(message)}>
                                        <DomainVerification />
                                    </IconButton>}
                                    {(message.status.intKey === 2 || message.status.intKey === 4) && <IconButton aria-label="Reactivate" onClick={() => selectActionMessage(message, AppActionType.Update)}
                                        size="small" color="success" title="Reactivate">
                                        <RestartAlt />
                                    </IconButton>}
                                    <IconButton aria-label="Edit" size="small" color="secondary" onClick={() => navigateTo(`/eventbus-messages/${message.requestId}`)} title="Edit">
                                        <Edit />
                                    </IconButton>
                                    <IconButton aria-label="Delete" size="small" color="error" title="Delete" onClick={() => selectActionMessage(message, AppActionType.Delete)}>
                                        <Delete />
                                    </IconButton>
                                </TableCell>}
                            </TableRow>)}
                        </TableBody>
                    </Table>
                    <AppPagination changePageData={changePageData} rowsFounded={rowsFounded} />
                </TableContainer>
            </Grid>}
            {selectedStatusMessage !== null && <EventBusMessageProcessAttemptModal requestId={selectedStatusMessage.requestId}
                showModal={showStatusModal} updateList={getEventBusMessages} closeModal={closeStatusModal} />}
            {selectedActionMessage !== null && <EventBusMessageActionModal selectedMessage={selectedActionMessage} onClose={closeActionModal}
                updateList={getEventBusMessages} showModal={showActionModal} actionType={selectedActionType!} />}
            <AppSnackBar response={snackbarResponse} />
        </>
    );
}

export default EventBusMessageTable;