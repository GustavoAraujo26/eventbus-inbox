import { Backdrop, CircularProgress } from "@mui/material";
import { useSelector } from "react-redux";
import { RootState } from "../../state/app-store";

const AppBackdrop = () => {
    
    const isLoading = useSelector((state: RootState) => state.appBackdrop);
    
    return (
        <Backdrop open={isLoading}>
            <CircularProgress color="inherit" />
        </Backdrop>
    );
}

export default AppBackdrop;