import { CleaningServices, Search } from "@mui/icons-material";
import { Box, Button, Card, CardContent, Divider, FormControl, Grid, InputLabel, MenuItem, Select, TextField, Typography } from "@mui/material";
import { useState } from "react";
import EnumData from "../../../interfaces/enum-data";

interface SearchCardProps {
    statusList: EnumData[],
    searchQueues: (name: string, description: string, status: number) => void
}

const EventBusQueueSearchCard = ({ statusList, searchQueues }: SearchCardProps) => {
    const [nameMatch, setNameMatch] = useState('');
    const [descriptionMatch, setDescriptionMatch] = useState('');
    const [currentStatus, setStatus] = useState<number>(0);

    const changeStatus = (selectedStatus: string | number) => {
        if (selectedStatus) {
            if (typeof selectedStatus === 'number') {
                setStatus(selectedStatus);
            }
            else {
                setStatus(+selectedStatus);
            }
        }
    }

    const onSubmitSearch = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        searchQueues(nameMatch, descriptionMatch, currentStatus);
    }

    const cleanSearch = () => {
        setNameMatch('');
        setDescriptionMatch('');
        setStatus(0);

        searchQueues('', '', 0);
    }

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
                                            onChange={event => setNameMatch(event.target.value)} />
                                    </Grid>
                                    <Grid item md={4}>
                                        <TextField value={descriptionMatch}
                                            label="Description"
                                            variant="standard"
                                            fullWidth
                                            onChange={event => setDescriptionMatch(event.target.value)} />
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