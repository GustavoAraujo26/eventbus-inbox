import { useEffect, useState } from "react";
import { Grid, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import EventBusMessageTableFilter from "./eventbus-message-table-filter";
import { Delete, DomainVerification, Edit, Info, RestartAlt } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";
import { AppActionType } from "../../enums/app-action-type";
import GetEventbusMessageListResponse from "../../interfaces/responses/eventbus-received-message/get-eventbus-message-list-response";
import EventBusMessageActionModal from "../../pages/event-bus-messages/eventbus-message-action-modal";
import EventBusMessageProcessAttemptModal from "../../pages/event-bus-messages/eventbus-message-process-attempt-modal";
import EventBusMessageStatus from "../../pages/event-bus-messages/eventbus-message-status";
import { EventBusMessageService } from "../../services/eventbus-message-service";
import AppPagination from "../app-pagination";
import { useAppDispatch, useAppSelector } from "../../state/hooks/app-hooks";
import { RootState } from "../../state/app-store";
import { changeEventBusMessagePagination } from "../../state/slices/eventbus-message/eventbus-message-list-request-slice";
import { fetchEventBusMessageList } from "../../state/slices/eventbus-message/eventbus-message-list-slice";

interface MessageTableProps {
    gridSize: number,
    showQueue: boolean,
    showFilter: boolean,
    currentQueueId: string | null,
    showActions: boolean
}

const EventBusMessageTable = ({ gridSize, showQueue, showFilter, currentQueueId, showActions }: MessageTableProps) => {
    const dispatch = useAppDispatch();
    const navigateTo = useNavigate();
    const messageService = new EventBusMessageService();

    const queueMessagesContainer = useAppSelector((state: RootState) => state.eventbusMessageList);
    const request = useAppSelector((state: RootState) => state.eventbusMessageListRequest);

    const [queueMessages, setQueueMessages] = useState<GetEventbusMessageListResponse[]>([]);

    const [enableNextPage, setNextPageEnabled] = useState(true);

    const [showStatusModal, setShowStatusModal] = useState(false);
    const [showActionModal, setShowActionModal] = useState(false);
    const [selectedStatusMessage, setSelectedStatusMessage] = useState<GetEventbusMessageListResponse | null>(null);
    const [selectedActionMessage, setSelectedActionMessage] = useState<GetEventbusMessageListResponse | null>(null);
    const [selectedActionType, setActionType] = useState<AppActionType | null>(null);

    const changePageData = (selectedPage: number, selectedPageSize: number) => {
        dispatch(changeEventBusMessagePagination({ page: selectedPage, pageSize: selectedPageSize }));
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
        dispatch(fetchEventBusMessageList(request));
    }, []);

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

    useEffect(() => {
        setQueueMessages(queueMessagesContainer.data);
        setNextPageEnabled(queueMessagesContainer.data.length >= 10);
    }, [queueMessagesContainer]);

    useEffect(() => {
        dispatch(fetchEventBusMessageList(request));
    }, [request.page, request.pageSize]);

    return (
        <>
            {queueMessages && <Grid item md={gridSize}>
                {showFilter && <EventBusMessageTableFilter />}
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
                    <AppPagination changePageData={changePageData} 
                        enableNextPage={enableNextPage} 
                        pageData={{currentPage: request.page, currentPageSize: request.pageSize}} />
                </TableContainer>
            </Grid>}
            {selectedStatusMessage !== null && <EventBusMessageProcessAttemptModal requestId={selectedStatusMessage.requestId}
                showModal={showStatusModal} updateList={() => dispatch(fetchEventBusMessageList(request))} closeModal={closeStatusModal} />}
            {selectedActionMessage !== null && <EventBusMessageActionModal selectedMessage={selectedActionMessage} onClose={closeActionModal}
                updateList={() => dispatch(fetchEventBusMessageList(request))} showModal={showActionModal} actionType={selectedActionType!} />}
        </>
    );
}

export default EventBusMessageTable;