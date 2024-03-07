import { Backdrop, Box, Button, Card, CardContent, CircularProgress, Divider, FormControl, FormControlLabel, Grid, InputLabel, MenuItem, Paper, Select, Switch, TextField, Typography } from "@mui/material";
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
import AppPeriodForm from "../../../components/app-period-form";

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
    const [queueId, setQueueId] = useState<string>('');
    const [creationStartDateSearch, setCreationStartDateSearch] = useState<Date | null>(null);
    const [creationEndDateSearch, setCreationEndDateSearch] = useState<Date | null>(null);
    const [updateStartDateSearch, setUpdateStartDateSearch] = useState<Date | null>(null);
    const [updateEndDateSearch, setUpdateEndDateSearch] = useState<Date | null>(null);
    const [typeMatch, setTypeMatch] = useState<string>('');
    const [statusToSearch, setStatusToSearch] = useState<number[] | null>(null);
    const [creationDateToogle, setCreationDateToogle] = useState<boolean>(true);
    const [updateDateToogle, setUpdateDateToogle] = useState<boolean>(false);

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
        debugger;

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
        setQueueId('');
        setCreationStartDateSearch(null);
        setCreationEndDateSearch(null);
        setUpdateStartDateSearch(null);
        setUpdateEndDateSearch(null);
        setTypeMatch('');
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
                                <Typography component="h3" sx={{fontWeight: 'bold'}}>Filters</Typography>
                                <Divider/>
                                <Grid container justifyContent="left" spacing={2}>
                                    {queueList && <Grid item md={6}>
                                        <FormControl variant="standard" fullWidth>
                                            <InputLabel variant="standard" htmlFor="queue-select">Select the Queue</InputLabel>
                                            <Select id="queue-select" value={queueId} onChange={event => setQueueId(event.target.value)} fullWidth>
                                                <MenuItem value="">Select an option</MenuItem>
                                                {queueList.map(option => <MenuItem key={option.id} value={option.id}>
                                                    {option.name}
                                                </MenuItem>)}
                                            </Select>
                                        </FormControl>
                                    </Grid>}
                                    <Grid item md={6}>
                                        <TextField value={typeMatch}
                                            label="Type to search"
                                            variant="standard"
                                            fullWidth
                                            onChange={event => setTypeMatch(event.target.value)} />
                                    </Grid>
                                </Grid>
                                <br/>
                                <Grid justifyContent="center" container spacing={2}>
                                    <Grid item md={6}>
                                        <Typography>Search By</Typography>
                                        <FormControlLabel label="Creation date" control={<Switch checked={creationDateToogle} value={creationDateToogle} onChange={(event) => {
                                            const checked: boolean = event.target.checked;
                                            if (checked) {
                                                setCreationDateToogle(true);
                                                setUpdateDateToogle(false);
                                            }
                                            else {
                                                setCreationDateToogle(false);
                                                setUpdateDateToogle(true);
                                            }
                                        }} />} />

                                        <FormControlLabel label="Update date" control={<Switch checked={updateDateToogle} value={updateDateToogle} onChange={(event) => {
                                            const checked: boolean = event.target.checked;
                                            if (checked) {
                                                setCreationDateToogle(false);
                                                setUpdateDateToogle(true);
                                            }
                                            else {
                                                setCreationDateToogle(true);
                                                setUpdateDateToogle(false);
                                            }
                                        }} />} />

                                        {creationDateToogle && <AppPeriodForm currentStart={creationStartDateSearch} currentEnd={creationEndDateSearch} 
                                            cleanForm={(creationStartDateSearch === null && creationEndDateSearch === null)}
                                            onUpdatePeriod={function (selectedStart: Date, selectedEnd: Date): void {
                                            setCreationStartDateSearch(selectedStart);
                                            setCreationEndDateSearch(selectedEnd);
                                        }} />}

                                        {updateDateToogle && <AppPeriodForm currentStart={updateStartDateSearch} currentEnd={updateEndDateSearch} 
                                            cleanForm={(updateStartDateSearch === null && updateEndDateSearch === null)}
                                            onUpdatePeriod={function (selectedStart: Date, selectedEnd: Date): void {
                                            setUpdateStartDateSearch(selectedStart);
                                            setUpdateEndDateSearch(selectedEnd);
                                        }} />}

                                    </Grid>
                                    {statusList && <Grid item md={6}>
                                        <EventBusMessageStatusFilter statusList={statusList} updateStatusFilter={obj => setStatusToSearch(obj)} cleanList={statusToSearch === null} />
                                    </Grid>}
                                </Grid>
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