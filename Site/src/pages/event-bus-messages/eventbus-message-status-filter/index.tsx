import { useEffect, useState } from "react";
import EnumData from "../../../interfaces/enum-data";
import { Checkbox, FormControlLabel, Paper } from "@mui/material";
import { CheckBox, Label } from "@mui/icons-material";

interface StatusFilterProps {
    statusList: EnumData[],
    updateStatusFilter: (selectedStatus: number[]) => void,
    cleanList: boolean
}

const EventBusMessageStatusFilter = ({ statusList, updateStatusFilter, cleanList }: StatusFilterProps) => {

    const [selectedStatus, setSelectedStatus] = useState<number[]>([]);

    const onCheckStatus = (statusId: number, event: React.ChangeEvent<HTMLInputElement>) => {
        const checked: boolean = event.target.checked;

        setSelectedStatus(selectedStatus.filter(x => x !== statusId));
        if (checked){
            setSelectedStatus(oldList => [...oldList, statusId]);
        }
    }

    useEffect(() => {
        updateStatusFilter(selectedStatus);
    }, [selectedStatus]);

    useEffect(() => {
        setSelectedStatus([]);
    }, [cleanList]);

    return (
        <>
            
            {statusList && statusList.length > 0 && <>
                <p>Select the status to search</p>
                <Paper sx={{ maxHeight: '300px', overflow: 'scroll' }}>
                    {statusList.map(item => <FormControlLabel key={item.intKey} control={
                        <Checkbox checked={selectedStatus.indexOf(item.intKey) !== -1} onChange={event => onCheckStatus(item.intKey, event)} name={item.stringKey} />} label={item.description}/>)}
                </Paper>
            </>}
        </>
    );
}

export default EventBusMessageStatusFilter;