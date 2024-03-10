import { CleaningServices, Search } from "@mui/icons-material";
import { Box, Button, Card, CardContent, Divider, FormControl, Grid, InputLabel, MenuItem, Select, TextField, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import EnumData from "../../../interfaces/enum-data";
import { useAppDispatch, useAppSelector } from "../../../state/hooks/app-hooks";
import { RootState } from "../../../state/app-store";
import { fetchEventBusQueueStatusList } from "../../../state/slices/enums/eventbus-queue-status-list-slice";
import { cleanEventBusQueueListRequest, setEventBusQueueListDescriptionMatch, setEventBusQueueListNameMatch, setEventBusQueueListStatusMatch } from "../../../state/slices/eventbus-queue/eventbus-queue-list-request-slice";
import { fetchEventBusQueueList } from "../../../state/slices/eventbus-queue/eventbus-queue-list-slice";

const EventBusQueueSearchCard = () => {
    const dispatch = useAppDispatch();

    const [nameMatch, setNameMatch] = useState('');
    const [descriptionMatch, setDescriptionMatch] = useState('');
    const [currentStatus, setStatus] = useState<number>(0);
    const [statusList, setStatusList] = useState<EnumData[]>([]);

    const statusListContainer = useAppSelector((state: RootState) => state.eventBusQueueStatusList);
    const currentRequest = useAppSelector((state: RootState) => state.eventbusQueueListRequest);

    const changeStatus = (selectedStatus: string | number) => {
        if (selectedStatus) {
            if (typeof selectedStatus === 'number') {
                setStatus(selectedStatus);
                const requestStatus: number | null = (selectedStatus === 0 ? null : selectedStatus);
                dispatch(setEventBusQueueListStatusMatch(requestStatus))
            }
            else {
                setStatus(+selectedStatus);
                const requestStatus: number | null = (+selectedStatus === 0 ? null : +selectedStatus);
                dispatch(setEventBusQueueListStatusMatch(requestStatus))
            }
        }
    }

    const onSubmitSearch = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        dispatch(fetchEventBusQueueList(currentRequest));
    }

    const cleanSearch = () => {
        setNameMatch('');
        setDescriptionMatch('');
        setStatus(0);

        dispatch(cleanEventBusQueueListRequest());
    }

    useEffect(() => {
        dispatch(fetchEventBusQueueStatusList());
    }, []);

    useEffect(() => {
        if (statusListContainer.data){
            setStatusList(statusListContainer.data);
        }
    }, [statusListContainer]);

    useEffect(() => {
        if (currentRequest !== null){
            setNameMatch(currentRequest.nameMatch ?? '');
            setDescriptionMatch(currentRequest.descriptionMatch ?? '');
            setStatus(currentRequest.status ?? 0);
        }
    }, [currentRequest]);

    return (
        <>
            <Grid container justifyContent="center" sx={{ marginBottom: 3 }}>
                <Grid item md={12}>
                    <Card>
                        <CardContent>
                            <Box component="form" onSubmit={onSubmitSearch}>
                                <Typography component="h3" sx={{ fontWeight: 'bold' }}>Filters</Typography>
                                <Divider />
                                <br />
                                <Grid container justifyContent="center" spacing={2}>
                                    <Grid item md={4}>
                                        <TextField value={nameMatch}
                                            label="Name"
                                            variant="standard"
                                            fullWidth
                                            onChange={event => dispatch(setEventBusQueueListNameMatch(event.target.value))} />
                                    </Grid>
                                    <Grid item md={4}>
                                        <TextField value={descriptionMatch}
                                            label="Description"
                                            variant="standard"
                                            fullWidth
                                            onChange={event => dispatch(setEventBusQueueListDescriptionMatch(event.target.value))} />
                                    </Grid>
                                    {statusList && <Grid item md={4}>
                                        <FormControl sx={{ minWidth: '150px' }}>
                                            <InputLabel variant="standard" htmlFor="page-size-select">Select the status</InputLabel>
                                            <Select id="page-size-select" value={currentStatus} onChange={event => changeStatus(event.target.value)} fullWidth>
                                                <MenuItem value="0">Select an option</MenuItem>
                                                {statusList.map(option => <MenuItem key={option.intKey} value={option.intKey}>
                                                    {option.description}
                                                </MenuItem>)}
                                            </Select>
                                        </FormControl>
                                    </Grid>}
                                </Grid>
                                <Button variant="contained" color="primary" sx={{ marginTop: 2 }} type="submit" title="Search">
                                    <Search /> Search
                                </Button>
                                <Button variant="contained" color="warning" sx={{ marginTop: 2, marginLeft: 2 }} title="Clean search" onClick={cleanSearch}>
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

export default EventBusQueueSearchCard;