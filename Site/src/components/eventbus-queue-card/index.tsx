import { AssignmentInd, Book, Description, GridOn, Inbox, Visibility } from "@mui/icons-material";
import { Avatar, Button, Card, CardActions, CardContent, Chip, Divider, List, ListItem, ListItemAvatar, ListItemText, Typography } from "@mui/material";
import GetEventbusQueueResponse from "../../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import EventBusQueueStatus from "../eventbus-queue-status";
import { useNavigate } from "react-router-dom";

interface QueueProps {
    queue: GetEventbusQueueResponse,
    showDescription: boolean,
    showSummarization: boolean
}

const EventBusQueueCard = ({ queue, showDescription, showSummarization = false }: QueueProps) => {
    const navigateTo = useNavigate();

    return (
        <Card>
            <CardContent>
                <List>
                    <ListItem>
                        <ListItemAvatar>
                            <Avatar>
                                <AssignmentInd />
                            </Avatar>
                        </ListItemAvatar>
                        <ListItemText primary={queue.id} />
                    </ListItem>
                </List>
                <Divider />
                <List>
                    <ListItem>
                        <ListItemAvatar>
                            <Avatar>
                                <Book />
                            </Avatar>
                        </ListItemAvatar>
                        <ListItemText primary={queue.name} />
                    </ListItem>
                    <ListItem>
                        <ListItemAvatar>
                            <Avatar>
                                <GridOn />
                            </Avatar>
                        </ListItemAvatar>
                        <EventBusQueueStatus status={queue.status} />
                    </ListItem>
                    {showDescription === true ? <ListItem>
                        <ListItemAvatar>
                            <Avatar>
                                <Description />
                            </Avatar>
                        </ListItemAvatar>
                        <ListItemText secondary={queue.description} />
                    </ListItem> : null}
                </List>
                <Divider />
                {queue.messagesSummarization.length > 0 && showDescription === true ? <>
                    <List>
                        {queue.messagesSummarization && queue.messagesSummarization.map(itemSum => <ListItem key={itemSum.status.intKey} secondaryAction={
                            <Chip label={itemSum.messageCount} />
                        }>
                            <ListItemAvatar>
                                <Avatar>
                                    <Inbox/>
                                </Avatar>
                            </ListItemAvatar>
                            <ListItemText primary={itemSum.status.description} />
                        </ListItem>)}
                    </List>
                    <Divider/>
                </> : null}
            </CardContent>
            <CardActions>
                <Button onClick={() => navigateTo(`/eventbus-queues/details/${queue.id}`)}>
                    <Visibility />
                    <Typography>View</Typography>
                </Button>
            </CardActions>
        </Card>
    );
}

export default EventBusQueueCard;