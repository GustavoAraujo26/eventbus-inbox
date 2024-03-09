import { Avatar, Button, Chip, Dialog, DialogActions, DialogContent, DialogTitle, IconButton, List, ListItem, ListItemAvatar, ListItemText, Snackbar } from "@mui/material";
import { useEffect, useState } from "react";
import { AssignmentLate, Close, Report, Route } from "@mui/icons-material";
import AppSnackbarResponse from "../../interfaces/requests/app-snackbar-response";
import { useDispatch } from "react-redux";
import { useSelector } from "react-redux";
import { closeSnackbar } from "../../state/slices/app-snackbar-slice";
import { RootState } from "../../state/app-store";
import { useAppDispatch, useAppSelector } from "../../state/hooks/app-hooks";

const AppSnackBar = () => {
    const dispatch = useAppDispatch();

    const snackbarControl = useAppSelector((state: RootState) => state.appSnackbar);

    const [showModal, setShowModal] = useState(false);
    const [response, setResponse] = useState<AppSnackbarResponse | null>(null);
    const [showSnackbar, setShowSnackbar] = useState<boolean>(false);
    const [message, setMessage] = useState('');
    const [snackbarColor, setSnackbarColor] = useState('success');

    const configureEnvironment = () => {
        if (!snackbarControl)
        return;

        setShowSnackbar(snackbarControl.show);

        if (snackbarControl.response != null){
            setResponse(snackbarControl.response);
            setMessage(snackbarControl.response.message);

            if (snackbarControl.response.success){
                setSnackbarColor('success');
            }
            else{
                setSnackbarColor('error');
            }
        }
    }

    useEffect(() => {
        configureEnvironment();
    }, [snackbarControl]);

    return (
        <>
            <Snackbar open={showSnackbar}
                autoHideDuration={5000}
                message={message}
                color={snackbarColor}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
                onClose={() => dispatch(closeSnackbar())}
                action={<>
                    <Button color="primary" size="small" onClick={() => setShowModal(true)}>View</Button>
                    <IconButton onClick={() => dispatch(closeSnackbar())}>
                        <Close />
                    </IconButton>
                </>} />

            {response && <>
                <Dialog open={showModal}>
                    <DialogTitle>Error</DialogTitle>
                    <DialogContent>
                        <List>
                            <ListItem secondaryAction={<Chip label={response.statusCode} />}>
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
                                <ListItemText primary={response.message} />
                            </ListItem>
                            {response.stackTrace && <ListItem>
                                <ListItemAvatar>
                                    <Avatar>
                                        <Route />
                                    </Avatar>
                                </ListItemAvatar>
                                <ListItemText primary={response.stackTrace} />
                            </ListItem>}
                        </List>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={() => setShowModal(false)}>
                            <Close />
                        </Button>
                    </DialogActions>
                </Dialog>
            </>}
        </>
    );
}

export default AppSnackBar;