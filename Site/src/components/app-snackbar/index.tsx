import { Avatar, Button, Chip, Dialog, DialogActions, DialogContent, DialogTitle, IconButton, List, ListItem, ListItemAvatar, ListItemText, Snackbar } from "@mui/material";
import AppSnackbarResponse from "../../interfaces/requests/app-snackbar-response";
import { useEffect, useState } from "react";
import { AssignmentLate, Close, Report, Route } from "@mui/icons-material";

interface SnackbarProps {
    response: AppSnackbarResponse | undefined
}

const AppSnackBar = ({ response }: SnackbarProps) => {

    const [showSnackbar, setShowSnackbar] = useState(false);
    const [showModal, setShowModal] = useState(false);

    useEffect(() => {
        if (typeof response !== 'undefined') {
            setShowSnackbar(true);
        }
    }, [response])

    return (
        <>
            {response && <Snackbar open={showSnackbar}
                autoHideDuration={5000}
                message={response.message}
                color={response.success ? 'success' : 'error'}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
                onClose={() => setShowSnackbar(false)}
                action={<>
                    <Button color="primary" size="small" onClick={() => setShowModal(true)}>View</Button>
                    <IconButton onClick={() => setShowSnackbar(false)}>
                        <Close />
                    </IconButton>
                </>} />}

            {response && <Dialog open={showModal}>
                <DialogTitle>Error</DialogTitle>
                <DialogContent>
                    <List>
                        <ListItem secondaryAction={<Chip label={response?.statusCode} />}>
                            <ListItemAvatar>
                                <Avatar>
                                    <AssignmentLate />
                                </Avatar>
                            </ListItemAvatar>
                            <ListItemText primary="Status Code" />
                        </ListItem>
                        <ListItem>
                            <ListItemAvatar>
                                <Avatar>
                                    <Report />
                                </Avatar>
                            </ListItemAvatar>
                            <ListItemText primary={response?.message} />
                        </ListItem>
                        {response?.stackTrace && <ListItem>
                            <ListItemAvatar>
                                <Avatar>
                                    <Route />
                                </Avatar>
                            </ListItemAvatar>
                            <ListItemText primary={response?.stackTrace} />
                        </ListItem>}
                    </List>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setShowModal(false)}>
                        <Close />
                    </Button>
                </DialogActions>
            </Dialog>}
        </>
    );
}

export default AppSnackBar;