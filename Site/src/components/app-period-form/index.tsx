import { Grid } from "@mui/material";
import { useEffect, useState } from "react";
import { DatePicker } from "@mui/x-date-pickers";
import dayjs, { Dayjs } from "dayjs";

interface PeriodFormProps {
    currentStart: Date | null,
    currentEnd: Date | null,
    onUpdatePeriod: (selectedStart: Date, selectedEnd: Date) => void,
    cleanForm: boolean
}

const AppPeriodForm = ({ currentStart, currentEnd, onUpdatePeriod, cleanForm }: PeriodFormProps) => {
    const [startDate, setStartDate] = useState<Dayjs | null>();
    const [endDate, setEndDate] = useState<Dayjs | null>();

    useEffect(() => {
        if (currentStart && currentEnd) {
            setStartDate(dayjs(currentStart.toLocaleDateString()));
            setEndDate(dayjs(currentEnd.toLocaleDateString()));
        }
        else{
            setStartDate(null);
            setEndDate(null);
        }
    }, []);

    useEffect(() => {
        if (startDate && endDate) {
            onUpdatePeriod(startDate.toDate(), endDate.toDate());
        }
    }, [startDate, endDate]);

    useEffect(() => {
        if (cleanForm){
            setStartDate(null);
            setEndDate(null);
        }
    }, [cleanForm]);

    return (
        <Grid justifyContent="center" container spacing={0}>
            <Grid item md={6}>
                <DatePicker label="Start date to search" defaultValue={startDate} onChange={obj => setStartDate(obj)} />
            </Grid>
            <Grid item md={6}>
                <DatePicker label="End date to search" defaultValue={endDate} onChange={obj => setEndDate(obj)} />
            </Grid>
        </Grid>
    );
}

export default AppPeriodForm;