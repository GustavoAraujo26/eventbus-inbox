import { Button, Chip, Typography } from "@mui/material";
import EnumData from "../../../interfaces/enum-data";
import { Done, ErrorOutline, Pending, PendingActions, Warning } from "@mui/icons-material";

interface StatusProps {
    status: EnumData
}

const EventBusMessageStatus = ({ status }: StatusProps) => {
    return (<>
    {status.intKey === 1 && <Chip variant="outlined" color="primary" icon={<PendingActions/>} label={status.description}/>}
    {status.intKey === 2 && <Chip variant="filled" color="success" icon={<Done/>} label={status.description}/>}
    {status.intKey === 3 && <Chip variant="outlined" color="error" icon={<Done/>} label={status.description}/>}
    {status.intKey === 4 && <Chip variant="filled" color="error" icon={<Done/>} label={status.description}/>}
    </>);
}

export default EventBusMessageStatus;