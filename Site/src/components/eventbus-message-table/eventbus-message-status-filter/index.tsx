import { useEffect, useState } from "react";
import { Checkbox, FormControlLabel, Paper } from "@mui/material";
import EnumData from "../../../interfaces/enum-data";
import { useSelector } from "react-redux";
import { RootState } from "../../../state/app-store";
import { fetchEventBusMessageStatusList } from "../../../state/slices/enums/eventbus-message-status-list-slice";
import { useAppDispatch, useAppSelector } from "../../../state/hooks/app-hooks";
import { shallowEqual } from "react-redux";
import { addStatusToEventBusMessageListSearch, removeStatusFromEventBusMessageListSearch } from "../../../state/slices/eventbus-message/eventbus-message-list-request-slice";

const EventBusMessageStatusFilter = () => {

    const dispatch = useAppDispatch();

    const statusListContainer = useAppSelector((state: RootState) => state.eventBusMessageStatusList, shallowEqual);
    const request = useSelector((state: RootState) => state.eventbusMessageListRequest, shallowEqual);

    const [statusList, setStatusList] = useState<EnumData[]>([]);

    const onCheckStatus = (statusId: number, event: React.ChangeEvent<HTMLInputElement>) => {
        const checked: boolean = event.target.checked;

        if (checked){
            dispatch(addStatusToEventBusMessageListSearch(statusId));
        }
        else{
            dispatch(removeStatusFromEventBusMessageListSearch(statusId));
        }
    }

    const isChecked = (item: EnumData): boolean => {
        if (request.statusToSearch === null || request.statusToSearch.length === 0) {
            return false;
        }

        return request.statusToSearch.indexOf(item.intKey) !== -1;
    }

    useEffect(() => {
        dispatch(fetchEventBusMessageStatusList());
    }, []);

    useEffect(() => {
        setStatusList(statusListContainer.data);
    }, [statusListContainer]);

    return (
        <>
            {statusList && statusList.length > 0 && <>
                <p>Select the status to search</p>
                <Paper sx={{ maxHeight: '300px', overflow: 'auto' }}>
                    {statusList.map(item => <FormControlLabel key={item.intKey} control={
                        <Checkbox checked={isChecked(item)} onChange={event => onCheckStatus(item.intKey, event)} name={item.stringKey} />} label={item.description} />)}
                </Paper>
            </>}
        </>
    );
}

export default EventBusMessageStatusFilter;