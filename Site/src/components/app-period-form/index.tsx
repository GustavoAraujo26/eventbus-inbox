import { Grid } from "@mui/material";
import { useEffect, useState } from "react";
import { DatePicker } from "@mui/x-date-pickers";
import dayjs, { Dayjs } from "dayjs";

interface PeriodFormProps {
    currentStart: Date | null,
    currentEnd: Date | null,
    onUpdatePeriod: (selectedStart: Date | null, selectedEnd: Date | null) => void,
    cleanForm: boolean
}

const AppPeriodForm = ({ currentStart, currentEnd, onUpdatePeriod, cleanForm }: PeriodFormProps) => {
    const [startDate, setStartDate] = useState<Date | null>();
    const [endDate, setEndDate] = useState<Date | null>();

    useEffect(() => {
        setStartDate(currentStart);
        setEndDate(currentEnd);
    }, []);

    useEffect(() => {
        let start: Date | null = null;
        let end: Date | null = null;

        if (startDate !== null && typeof startDate !== 'undefined'){
            start = startDate;
        }

        if (endDate !== null && typeof endDate !== 'undefined'){
            end = endDate;
        }
        
        onUpdatePeriod(start, end);
    }, [startDate, endDate]);

    useEffect(() => {
        if (cleanForm){
            setStartDate(null);
            setEndDate(null);
            onUpdatePeriod(null, null);
        }
    }, [cleanForm]);

    return (
        <Grid justifyContent="center" container spacing={0}>
            <Grid item md={6}>
                <DatePicker label="Start date to search" value={currentStart} onChange={obj => {
                    setStartDate(obj);

                    // if (obj !== null && endDate === null){
                    //     setEndDate(obj);
                    // }
                }} />
            </Grid>
            <Grid item md={6}>
                <DatePicker label="End date to search" value={currentEnd} onChange={obj => setEndDate(obj)} />
            </Grid>
        </Grid>
    );
}

export default AppPeriodForm;