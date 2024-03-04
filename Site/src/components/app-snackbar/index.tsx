import { Button, IconButton, Snackbar } from "@mui/material";
import { ApiResponse } from "../../interfaces/api-response"
import AppSnackbarResponse from "../../interfaces/requests/app-snackbar-response";
import { useEffect, useState } from "react";
import { Close } from "@mui/icons-material";

interface SnackbarProps {
    response: AppSnackbarResponse | undefined
}

const AppSnackBar = ({ response } : SnackbarProps) => {
    
    const [showSnackbar, setShowSnackbar] = useState(false);

    useEffect(() => {
        if (typeof response !== 'undefined'){
            setShowSnackbar(true);
        }
    }, [response])
    
    return (
        <>
            { response && <Snackbar open={showSnackbar} 
                autoHideDuration={5000} 
                message={response.message} 
                color={ response.success ? 'success' : 'error' }
                anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
                onClose={() => setShowSnackbar(false)}
                action={<>
                    <Button color="primary" size="small">View</Button>
                    <IconButton onClick={() => setShowSnackbar(false)}>
                        <Close/>
                    </IconButton>
                </>} /> }
        </>
    );
}

export default AppSnackBar;