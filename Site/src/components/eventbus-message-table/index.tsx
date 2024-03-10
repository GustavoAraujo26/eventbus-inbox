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
import AppPagination from "../app-pagination";
import { useAppDispatch, useAppSelector } from "../../state/hooks/app-hooks";
import { RootState } from "../../state/app-store";
import { changeEventBusMessagePagination } from "../../state/slices/eventbus-message/eventbus-message-list-request-slice";
import { fetchEventBusMessageList } from "../../state/slices/eventbus-message/eventbus-message-list-slice";
import { openEventBusMessageStatusModal } from "../../state/slices/eventbus-message/eventbus-message-status-modal-slice";
import { openEventBusMessageActionModal } from "../../state/slices/eventbus-message/eventbus-message-action-modal-slice";

interface MessageTableProps {
    gridSize: number,
    showQueue: boolean,
    showFilter: boolean,
    showActions: boolean
}

const EventBusMessageTable = ({ gridSize, showQueue, showFilter, showActions }: MessageTableProps) => {
    const dispatch = useAppDispatch();
    const navigateTo = useNavigate();

    const queueMessagesContainer = useAppSelector((state: RootState) => state.eventbusMessageList);
    const request = useAppSelector((state: RootState) => state.eventbusMessageListRequest);

    const [queueMessages, setQueueMessages] = useState<GetEventbusMessageListResponse[]>([]);

    const [enableNextPage, setNextPageEnabled] = useState(true);

    const changePageData = (selectedPage: number, selectedPageSize: number) => {
        dispatch(changeEventBusMessagePagination({ page: selectedPage, pageSize: selectedPageSize }));
    }

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
                                        size="small" color="warning" title="Processing Attempt" onClick={() => dispatch(openEventBusMessageStatusModal(message.requestId))}>
                                        <DomainVerification />
                                    </IconButton>}
                                    {(message.status.intKey === 2 || message.status.intKey === 4) && <IconButton aria-label="Reactivate" onClick={() => dispatch(openEventBusMessageActionModal({
                                        action: AppActionType.Update,
                                        data: message
                                    }))}
                                        size="small" color="success" title="Reactivate">
                                        <RestartAlt />
                                    </IconButton>}
                                    <IconButton aria-label="Edit" size="small" color="secondary" onClick={() => navigateTo(`/eventbus-messages/${message.requestId}`)} title="Edit">
                                        <Edit />
                                    </IconButton>
                                    <IconButton aria-label="Delete" size="small" color="error" title="Delete" onClick={() => dispatch(openEventBusMessageActionModal({
                                        action: AppActionType.Delete,
                                        data: message
                                    }))}>
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
            <EventBusMessageProcessAttemptModal />
            <EventBusMessageActionModal />
        </>
    );
}

export default EventBusMessageTable;