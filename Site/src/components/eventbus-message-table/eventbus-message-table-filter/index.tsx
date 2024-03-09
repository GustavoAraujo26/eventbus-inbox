import { Box, Button, Card, CardContent, Divider, FormControl, FormControlLabel, Grid, InputLabel, MenuItem, Select, Switch, TextField, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { CleaningServices, Search } from "@mui/icons-material";
import Period from "../../../interfaces/period";
import GetEventbusQueueResponse from "../../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import AppPeriodForm from "../../app-period-form";
import EventBusMessageStatusFilter from "../eventbus-message-status-filter";
import { useAppDispatch, useAppSelector } from "../../../state/hooks/app-hooks";
import { RootState } from "../../../state/app-store";
import { fetchEventBusQueueList } from "../../../state/slices/eventbus-queue/eventbus-queue-list-slice";
import { setCleanEventBusListRequest, setEventBusMessageListCreatedAtSearch, setEventBusMessageListQueue, setEventBusMessageListTypeMatch, setEventBusMessageListUpdatedAtSearch } from "../../../state/slices/eventbus-message/eventbus-message-list-request-slice";
import { fetchEventBusMessageList } from "../../../state/slices/eventbus-message/eventbus-message-list-slice";

const EventBusMessageTableFilter = () => {
    const dispatch = useAppDispatch();

    const queueListContainer = useAppSelector((state: RootState) => state.eventbusQueueList);
    const request = useAppSelector((state: RootState) => state.eventbusMessageListRequest);

    const [queueList, setQueueList] = useState<GetEventbusQueueResponse[]>([]);
    const [creationDateToogle, setCreationDateToogle] = useState<boolean>(true);
    const [updateDateToogle, setUpdateDateToogle] = useState<boolean>(false);

    const filterTable = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        dispatch(fetchEventBusMessageList(request));
    }

    const setCreationPeriod = (startDate: Date, endDate: Date) => {
        if (startDate && endDate) {
            let creationDateSearch: Period = {
                start: startDate,
                end: endDate
            };
            dispatch(setEventBusMessageListCreatedAtSearch(creationDateSearch));
        }
    }

    const setUpdatePeriod = (startDate: Date, endDate: Date) => {
        if (startDate && endDate) {
            let updateDateSearch = {
                start: startDate,
                end: endDate
            }
            dispatch(setEventBusMessageListUpdatedAtSearch(updateDateSearch));
        }
    }

    const cleanDates = () => {
        dispatch(setEventBusMessageListCreatedAtSearch(null));
        dispatch(setEventBusMessageListUpdatedAtSearch(null));
    }

    useEffect(() => {
        dispatch(fetchEventBusQueueList(null));
    }, []);

    useEffect(() => {
        setQueueList(queueListContainer.data);
    }, [queueListContainer]);

    return (
        <>
            <Grid justifyContent="center" container spacing={2}>
                <Grid item md={12}>
                    <Card sx={{ marginBottom: 3 }}>
                        <CardContent>
                            <Box component="form" onSubmit={filterTable}>
                                <Typography component="h3" sx={{ fontWeight: 'bold' }}>Filters</Typography>
                                <Divider />
                                <Grid container justifyContent="left" spacing={2}>
                                    {queueList && <Grid item md={6}>
                                        <FormControl variant="standard" fullWidth>
                                            <InputLabel variant="standard" htmlFor="queue-select">Select the Queue</InputLabel>
                                            <Select id="queue-select" value={(request.queueId === null) ? '' : request.queueId} onChange={event => dispatch(setEventBusMessageListQueue(event.target.value))} fullWidth>
                                                <MenuItem value="">Select an option</MenuItem>
                                                {queueList.map(option => <MenuItem key={option.id} value={option.id}>
                                                    {option.name}
                                                </MenuItem>)}
                                            </Select>
                                        </FormControl>
                                    </Grid>}
                                    <Grid item md={6}>
                                        <TextField value={(request.typeMatch === null) ? '': request.typeMatch}
                                            label="Type to search"
                                            variant="standard"
                                            fullWidth
                                            onChange={event => dispatch(setEventBusMessageListTypeMatch(event.target.value))} />
                                    </Grid>
                                </Grid>
                                <br />
                                <Grid justifyContent="center" container spacing={2}>
                                    <Grid item md={6}>
                                        <Typography>Search By</Typography>
                                        <FormControlLabel label="Creation date" control={<Switch checked={creationDateToogle} value={creationDateToogle} onChange={(event) => {
                                            const checked: boolean = event.target.checked;
                                            cleanDates();
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
                                            cleanDates();
                                            if (checked) {
                                                setCreationDateToogle(false);
                                                setUpdateDateToogle(true);
                                            }
                                            else {
                                                setCreationDateToogle(true);
                                                setUpdateDateToogle(false);
                                            }
                                        }} />} />

                                        {creationDateToogle && <AppPeriodForm
                                            currentStart={request.creationDateSearch === null ? null : request.creationDateSearch.start}
                                            currentEnd={request.creationDateSearch === null ? null : request.creationDateSearch.end}
                                            cleanForm={request.creationDateSearch === null || (request.creationDateSearch.start === null && request.creationDateSearch.end === null)}
                                            onUpdatePeriod={(selectedStart: Date, selectedEnd: Date) => setCreationPeriod(selectedStart, selectedEnd)} />}

                                        {updateDateToogle && <AppPeriodForm
                                            currentStart={request.updateDateSearch === null ? null : request.updateDateSearch.start}
                                            currentEnd={request.updateDateSearch === null ? null : request.updateDateSearch.end}
                                            cleanForm={request.updateDateSearch === null || (request.updateDateSearch.start === null && request.updateDateSearch.end === null)}
                                            onUpdatePeriod={(selectedStart: Date, selectedEnd: Date) => setUpdatePeriod(selectedStart, selectedEnd)} />}


                                    </Grid>
                                    <Grid item md={6}>
                                        <EventBusMessageStatusFilter />
                                    </Grid>
                                </Grid>
                                <Button variant="contained" color="primary" sx={{ marginTop: 3 }} type="submit">
                                    <Search /> Search
                                </Button>
                                <Button variant="contained" color="warning" sx={{ marginTop: 3, marginLeft: 2 }} type="button" onClick={() => dispatch(setCleanEventBusListRequest())}>
                                    <CleaningServices /> Clean
                                </Button>
                            </Box>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>
        </>
    );
}

export default EventBusMessageTableFilter;