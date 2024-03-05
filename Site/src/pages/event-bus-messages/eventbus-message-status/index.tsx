import { Button, Typography } from "@mui/material";
import EnumData from "../../../interfaces/enum-data";
import { Done, ErrorOutline, PendingActions, Warning } from "@mui/icons-material";

interface StatusProps {
    status: EnumData
}

const EventBusMessageStatus = ({ status }: StatusProps) => {
    return (<Button variant="contained" disabled={true}>
        {status.intKey === 1 && <PendingActions/>}
        {status.intKey === 2 && <Done/>}
        {status.intKey === 3 && <Warning/>}
        {status.intKey === 4 && <ErrorOutline/>}
        <Typography>{status.description}</Typography>
    </Button>);
}

export default EventBusMessageStatus;