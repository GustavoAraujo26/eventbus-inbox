import { AssignmentInd, Code, Today, Queue, GridOn, Task } from "@mui/icons-material";
import { Card, CardHeader, CardContent, Divider, List, ListItem, ListItemAvatar, Tooltip, Avatar, ListItemText, Chip } from "@mui/material";
import EnumData from "../../../interfaces/enum-data";
import EventBusMessageStatus from "../eventbus-message-status";

interface MessageProps {
    requestId: string,
    type: string,
    creationDate: Date,
    processingAttempts: number,
    status: EnumData,
    queueName: string,
    queueProcessingAttempts: number
}

const EventBusMessageCard = ({requestId, type, creationDate, processingAttempts, status, queueName, queueProcessingAttempts}: MessageProps) => {
    return (
        <Card>
            <CardHeader title="Message Details" sx={{ textAlign: 'center' }} />
            <CardContent>
                <Divider />
                <List>
                    <ListItem>
                        <ListItemAvatar>
                            <Tooltip title="Request Id">
                                <Avatar>
                                    <AssignmentInd />
                                </Avatar>
                            </Tooltip>
                        </ListItemAvatar>
                        <ListItemText primary={requestId} />
                    </ListItem>
                </List>
                <Divider />
                <List>
                    <ListItem>
                        <ListItemAvatar>
                            <Tooltip title="Type">
                                <Avatar>
                                    <Code />
                                </Avatar>
                            </Tooltip>
                        </ListItemAvatar>
                        <ListItemText primary={type} />
                    </ListItem>
                    <ListItem>
                        <ListItemAvatar>
                            <Tooltip title="Creation Date">
                                <Avatar>
                                    <Today />
                                </Avatar>
                            </Tooltip>
                        </ListItemAvatar>
                        <ListItemText primary={(new Date(creationDate).toLocaleDateString())} />
                    </ListItem>
                    <ListItem>
                        <ListItemAvatar>
                            <Tooltip title="Queue">
                                <Avatar>
                                    <Queue />
                                </Avatar>
                            </Tooltip>
                        </ListItemAvatar>
                        <ListItemText primary={queueName} />
                    </ListItem>
                    <ListItem>
                        <ListItemAvatar>
                            <Tooltip title="Status">
                                <Avatar>
                                    <GridOn />
                                </Avatar>
                            </Tooltip>
                        </ListItemAvatar>
                        <EventBusMessageStatus status={status} />
                    </ListItem>
                    <ListItem>
                        <ListItemAvatar>
                            <Tooltip title="Processing Attempts">
                                <Avatar>
                                    <Task />
                                </Avatar>
                            </Tooltip>
                        </ListItemAvatar>
                        {processingAttempts > queueProcessingAttempts && <Chip label={processingAttempts} color="error" />}
                        {(processingAttempts <= queueProcessingAttempts) && status.intKey === 1 &&
                            <Chip label={processingAttempts} color="primary" variant="outlined" />}
                        {(processingAttempts <= queueProcessingAttempts) && status.intKey === 2 &&
                            <Chip label={processingAttempts} color="success" />}
                        {(processingAttempts <= queueProcessingAttempts) && status.intKey === 3 &&
                            <Chip label={processingAttempts} color="error" variant="outlined" />}
                        {(processingAttempts <= queueProcessingAttempts) && status.intKey === 4 &&
                            <Chip label={processingAttempts} color="error" />}
                    </ListItem>
                </List>
            </CardContent>
        </Card>
    );
}

export default EventBusMessageCard;