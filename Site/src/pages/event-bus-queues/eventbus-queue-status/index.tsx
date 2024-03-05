import { Button, Typography } from "@mui/material";
import EnumData from "../../../interfaces/enum-data"
import { DisabledByDefault, DoneOutlined } from "@mui/icons-material";

interface StatusProps {
    status: EnumData
}

const EventBusQueueStatus = ({ status }: StatusProps) => {
    return (
        <Button variant="contained" disabled={true}>
            { status.intKey === 1 ? <DoneOutlined/> : <DisabledByDefault/> }
            <Typography>{status.description}</Typography>
        </Button>
    );
}

export default EventBusQueueStatus;