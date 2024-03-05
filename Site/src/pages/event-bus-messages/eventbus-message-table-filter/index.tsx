import { Backdrop, Box, Button, Card, CardContent, CircularProgress, FormControl, Grid, InputLabel, MenuItem, Paper, Select, TextField } from "@mui/material";
import { EventBusQueueService } from "../../../services/eventbus-queue-service";
import GetEventbusQueueResponse from "../../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import { useEffect, useState } from "react";
import GetEventBusQueueListRequest from "../../../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";
import AppSnackbarResponse from "../../../interfaces/requests/app-snackbar-response";
import AppSnackBar from "../../../components/app-snackbar";
import { EnumsService } from "../../../services/enums-service";
import EnumData from "../../../interfaces/enum-data";
import Period from "../../../interfaces/period";
import { CleaningServices, Search } from "@mui/icons-material";
import EventBusMessageStatusFilter from "../eventbus-message-status-filter";

interface TableFilterProps {
    executeFilter: (queueId: string | null, creationDateSearch: Period | null,
        updateDateSearch: Period | null, typeMatch: string | null, status: number[] | null) => void
}

const EventBusMessageTableFilter = ({ executeFilter }: TableFilterProps) => {
    const queueService = new EventBusQueueService();
    const enumService = new EnumsService();

    const [queueList, setQueueList] = useState<GetEventbusQueueResponse[]>([]);
    const [statusList, setStatusList] = useState<EnumData[]>([]);
    const [snackbarResponse, setSnackbarResponse] = useState<AppSnackbarResponse>();
    const [isLoading, setLoading] = useState(true);
    const [queueId, setQueueId] = useState<string | null>(null);
    const [creationStartDateSearch, setCreationStartDateSearch] = useState<Date | null>(null);
    const [creationEndDateSearch, setCreationEndDateSearch] = useState<Date | null>(null);
    const [updateStartDateSearch, setUpdateStartDateSearch] = useState<Date | null>(null);
    const [updateEndDateSearch, setUpdateEndDateSearch] = useState<Date | null>(null);
    const [typeMatch, setTypeMatch] = useState<string | null>(null);
    const [statusToSearch, setStatusToSearch] = useState<number[] | null>(null);

    const loadQueueList = () => {
        setLoading(true);

        const request: GetEventBusQueueListRequest = {
            nameMatch: null,
            descriptionMatch: null,
            status: null,
            page: 1,
            pageSize: 1000000,
            summarizeMessages: false
        }

        queueService.ListQueues(request).then(response => {
            setLoading(false);

            const apiResponse = response.data;

            if (apiResponse.isSuccess) {
                setQueueList(apiResponse.data);
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

    const loadStatusList = () => {
        setLoading(true);

        enumService.ListMessageStatus().then(response => {
            setLoading(false);
            const apiResponse = response.data;

            if (apiResponse.isSuccess) {
                setStatusList(apiResponse.data);
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

    useEffect(() => {
        loadQueueList();
        loadStatusList();
    }, []);

    const filterTable = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        let selectedQueue: string | null = null;
        let creationDateSearch: Period | null = null;
        let updateDateSearch: Period | null = null;
        let typeToMatch: string | null = null;
        let statusSearchList: number[] | null = null;

        if (queueId && queueId !== '') {
            selectedQueue = queueId;
        }

        if (creationStartDateSearch && creationEndDateSearch) {
            creationDateSearch = {
                start: creationStartDateSearch,
                end: creationEndDateSearch
            };
        }

        if (updateStartDateSearch && updateEndDateSearch) {
            updateDateSearch = {
                start: updateStartDateSearch,
                end: updateEndDateSearch
            }
        }

        if (typeMatch && typeMatch !== '') {
            typeToMatch = typeMatch;
        }

        if (statusToSearch && statusToSearch.length > 0) {
            statusSearchList = statusToSearch;
        }

        executeFilter(selectedQueue, creationDateSearch, updateDateSearch, typeToMatch, statusSearchList);
    }

    const cleanForm = () => {
        setQueueId(null);
        setCreationStartDateSearch(null);
        setCreationEndDateSearch(null);
        setUpdateStartDateSearch(null);
        setUpdateEndDateSearch(null);
        setTypeMatch(null);
        setStatusToSearch(null);
    }

    return (
        <>
            <Backdrop open={isLoading}>
                <CircularProgress color="inherit" />
            </Backdrop>
            <Grid justifyContent="center" container spacing={2}>
                <Grid item md={12}>
                    <Card sx={{ marginBottom: 3 }}>
                        <CardContent>
                            <Box component="form" onSubmit={filterTable}>
                                <Grid container justifyContent="left" spacing={2}>
                                    {queueList && <Grid item md={3}>
                                        <FormControl fullWidth>
                                            <InputLabel variant="standard" htmlFor="queue-select">Select the Queue</InputLabel>
                                            <Select id="queue-select" value={queueId} onChange={event => setQueueId(event.target.value)} fullWidth>
                                                <MenuItem value="">Select an option</MenuItem>
                                                {queueList.map(option => <MenuItem key={option.id} value={option.id}>
                                                    {option.name}
                                                </MenuItem>)}
                                            </Select>
                                        </FormControl>
                                    </Grid>}
                                    <Grid item md={3}>
                                        <TextField value={typeMatch}
                                            label="Type to search"
                                            variant="standard"
                                            fullWidth
                                            onChange={event => setTypeMatch(event.target.value)} />
                                    </Grid>
                                    
                                    <Grid item md={3}>

                                    </Grid>
                                </Grid>
                                {statusList && <EventBusMessageStatusFilter statusList={statusList} updateStatusFilter={obj => setStatusToSearch(obj)} cleanList={statusToSearch === null} />}
                                <Button variant="contained" color="primary" sx={{ marginTop: 3 }} type="submit">
                                    <Search /> Search
                                </Button>
                                <Button variant="contained" color="secondary" sx={{ marginTop: 3, marginLeft: 2 }} type="button" onClick={cleanForm}>
                                    <CleaningServices /> Clean
                                </Button>
                            </Box>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>
            <AppSnackBar response={snackbarResponse} />
        </>
    );
}

export default EventBusMessageTableFilter;