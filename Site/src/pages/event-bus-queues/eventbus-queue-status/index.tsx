import { Chip, Typography } from "@mui/material";
import EnumData from "../../../interfaces/enum-data"
import { DisabledByDefault, DoneOutlined } from "@mui/icons-material";

interface StatusProps {
    status: EnumData
}

const EventBusQueueStatus = ({ status }: StatusProps) => {
    return (
        <>
            {status.intKey === 1 && <Chip variant="filled" color="success" icon={<DoneOutlined/>} label={status.description}/>}
            {status.intKey === 2 && <Chip variant="filled" color="secondary" icon={<DisabledByDefault/>} label={status.description}/>}
        </>
    );
}

export default EventBusQueueStatus;