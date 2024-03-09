import { Backdrop, CircularProgress } from "@mui/material";
import { RootState } from "../../state/app-store";
import { useAppSelector } from "../../state/hooks/app-hooks";

const AppBackdrop = () => {
    
    const isLoading = useAppSelector((state: RootState) => state.appBackdrop);
    
    return (
        <Backdrop open={isLoading}>
            <CircularProgress color="inherit" />
        </Backdrop>
    );
}

export default AppBackdrop;