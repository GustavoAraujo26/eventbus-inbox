import { createTheme } from "@mui/material";

const InboxTheme = createTheme({
    components: {
        MuiDrawer: {
            styleOverrides: {
                paper: {
                    backgroundColor: "#415a77",
                    color: "#e0e1dd"
                }
            }
        },
        MuiAppBar: {
            styleOverrides: {
                colorPrimary: {
                    backgroundColor: "#1b263b"
                }
            }
        }
    }
});

export default InboxTheme;