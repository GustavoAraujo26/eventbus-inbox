import { Grid, TextField } from "@mui/material";
import { useEffect, useState } from "react";
import Period from "../../interfaces/period";
import { DatePicker } from "@mui/x-date-pickers";

interface PeriodFormProps {
    currentStart: Date | null,
    currentEnd: Date | null,
    onUpdatePeriod: (selectedStart: Date, selectedEnd: Date) => void,
    cleanForm: boolean
}

const AppPeriodForm = ({ currentStart, currentEnd, onUpdatePeriod, cleanForm }: PeriodFormProps) => {
    const [startDate, setStartDate] = useState<Date | null>();
    const [endDate, setEndDate] = useState<Date | null>();

    useEffect(() => {
        if (currentStart && currentEnd) {
            setStartDate(currentStart);
            setEndDate(currentEnd);
        }
        else{
            setStartDate(null);
            setEndDate(null);
        }
    }, []);

    useEffect(() => {
        if (startDate && endDate) {
            onUpdatePeriod(startDate, endDate);
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
                <DatePicker label="Start date to search" value={startDate} onChange={obj => setStartDate(obj)} />
            </Grid>
            <Grid item md={6}>
                <DatePicker label="End date to search" value={endDate} onChange={obj => setEndDate(obj)} />
            </Grid>
        </Grid>
    );
}

export default AppPeriodForm;