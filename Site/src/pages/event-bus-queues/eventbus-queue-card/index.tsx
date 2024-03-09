import { AssignmentInd, Book, Description, GridOn, Inbox, Visibility } from "@mui/icons-material";
import { Avatar, Button, Card, CardActions, CardContent, CardHeader, Chip, Divider, List, ListItem, ListItemAvatar, ListItemText, Tooltip, Typography } from "@mui/material";
import GetEventbusQueueResponse from "../../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import EventBusQueueStatus from "../eventbus-queue-status";
import { useNavigate } from "react-router-dom";

interface QueueProps {
    queue: GetEventbusQueueResponse,
    showDescription: boolean,
    showSummarization: boolean,
    showNavigation: boolean
}

const EventBusQueueCard = ({ queue, showDescription, showSummarization, showNavigation }: QueueProps) => {
    const navigateTo = useNavigate();

    return (
        <Card>
            <CardHeader title="Event Bus Queue" sx={{ textAlign: 'center' }} />
            <CardContent>
                <Divider />
                <List>
                    <ListItem>
                        <ListItemAvatar>
                            <Tooltip title="Id">
                                <Avatar>
                                    <AssignmentInd />
                                </Avatar>
                            </Tooltip>
                        </ListItemAvatar>
                        <ListItemText primary={queue.id} />
                    </ListItem>
                </List>
                <Divider />
                <List>
                    <ListItem>
                        <ListItemAvatar>
                            <Tooltip title="Name">
                                <Avatar>
                                    <Book />
                                </Avatar>
                            </Tooltip>
                        </ListItemAvatar>
                        <ListItemText primary={queue.name} />
                    </ListItem>
                    <ListItem>
                        <ListItemAvatar>
                            <Tooltip title="Status">
                                <Avatar>
                                    <GridOn />
                                </Avatar>
                            </Tooltip>
                        </ListItemAvatar>
                        <EventBusQueueStatus status={queue.status} />
                    </ListItem>
                    {showDescription === true ? <ListItem>
                        <ListItemAvatar>
                            <Tooltip title="Description">
                                <Avatar>
                                <Description />
                                </Avatar>
                            </Tooltip>
                        </ListItemAvatar>
                        <ListItemText secondary={queue.description} />
                    </ListItem> : null}
                </List>
                <Divider />
                {queue.messagesSummarization && showSummarization === true ? <>
                    <List>
                        {queue.messagesSummarization && queue.messagesSummarization.map(itemSum => <ListItem key={itemSum.status.intKey} secondaryAction={
                            <Chip label={itemSum.messageCount} />
                        }>
                            <ListItemAvatar>
                                <Tooltip title="Status Name">
                                    <Avatar>
                                        <Inbox />
                                    </Avatar>
                                </Tooltip>
                            </ListItemAvatar>
                            <ListItemText primary={itemSum.status.description} />
                        </ListItem>)}
                    </List>
                    <Divider />
                </> : null}
            </CardContent>
            {showNavigation && <CardActions sx={{ justifyContent: 'center' }}>
                <Button onClick={() => navigateTo(`/eventbus-queues/details/${queue.id}`)}>
                    <Visibility />
                    <Typography>View</Typography>
                </Button>
            </CardActions>}
        </Card>
    );
}

export default EventBusQueueCard;