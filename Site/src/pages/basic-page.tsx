import { styled, useTheme } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import CssBaseline from '@mui/material/CssBaseline';
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';
import { useState } from 'react';
import { Collapse, Container, ThemeProvider, Link } from '@mui/material';
import { Outlet } from 'react-router-dom';
import InboxTheme from '../themes';
import { Add, Apps, ExpandLess, ExpandMore, Home, Send } from '@mui/icons-material';
import { Link as RouterLink } from "react-router-dom";

const drawerWidth = 240;

const Main = styled('main', { shouldForwardProp: (prop) => prop !== 'open' })<{
    open?: boolean;
}>(({ theme, open }) => ({
    flexGrow: 1,
    padding: theme.spacing(3),
    transition: theme.transitions.create('margin', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    marginLeft: `-${drawerWidth}px`,
    ...(open && {
        transition: theme.transitions.create('margin', {
            easing: theme.transitions.easing.easeOut,
            duration: theme.transitions.duration.enteringScreen,
        }),
        marginLeft: 0,
    }),
}));

interface AppBarProps extends MuiAppBarProps {
    open?: boolean;
}

const AppBar = styled(MuiAppBar, {
    shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({ theme, open }) => ({
    transition: theme.transitions.create(['margin', 'width'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    ...(open && {
        width: `calc(100% - ${drawerWidth}px)`,
        marginLeft: `${drawerWidth}px`,
        transition: theme.transitions.create(['margin', 'width'], {
            easing: theme.transitions.easing.easeOut,
            duration: theme.transitions.duration.enteringScreen,
        }),
    }),
}));

const DrawerHeader = styled('div')(({ theme }) => ({
    display: 'flex',
    alignItems: 'center',
    padding: theme.spacing(0, 1),
    // necessary for content to be below app bar
    ...theme.mixins.toolbar,
    justifyContent: 'flex-end',
    textAlign: 'center'
}));

const BasicPage = () => {
    const theme = useTheme();
    const [open, setOpen] = useState(true);

    const [queueSubMenuOpen, setQueueSubMenuOpen] = useState(false);
    const [messageSubMenuOpen, setMessageSubMenuOpen] = useState(false);

    const handleDrawerOpen = () => {
        setOpen(true);
    }

    const handleDrawerClose = () => {
        setOpen(false);
    }

    return (
        <>
            <ThemeProvider theme={InboxTheme}>
                <Box sx={{ display: 'flex' }}>
                    <CssBaseline />
                    <AppBar position="fixed" open={open}>
                        <Toolbar>
                            <IconButton
                                color="inherit"
                                aria-label="open drawer"
                                onClick={handleDrawerOpen}
                                edge="start"
                                sx={{ mr: 2, ...(open && { display: 'none' }) }}
                            >
                                <MenuIcon />
                            </IconButton>
                            <Typography variant="h6" noWrap component="div">
                                Event Bus Inbox
                            </Typography>
                        </Toolbar>
                    </AppBar>
                    <Drawer
                        sx={{
                            width: drawerWidth,
                            flexShrink: 0,
                            '& .MuiDrawer-paper': {
                                width: drawerWidth,
                                boxSizing: 'border-box',
                            },
                        }}
                        variant="persistent"
                        anchor="left"
                        open={open}
                    >
                        <DrawerHeader>
                            <IconButton onClick={handleDrawerClose}>
                                {theme.direction === 'ltr' ? <ChevronLeftIcon /> : <ChevronRightIcon />}
                            </IconButton>
                        </DrawerHeader>
                        <Divider />
                        <List>
                            <Link component={RouterLink} to="/" sx={{ textDecoration: 'none' }}>
                                <ListItemButton sx={{ pl: 4, color: 'white' }}>
                                    <ListItemIcon>
                                        <Home />
                                    </ListItemIcon>
                                    <ListItemText primary="Home" />
                                </ListItemButton>
                            </Link>
                            <ListItemButton onClick={() => setQueueSubMenuOpen(!queueSubMenuOpen)}>
                                <ListItemIcon>
                                    <InboxIcon />
                                </ListItemIcon>
                                <ListItemText primary="Queues" />
                                {queueSubMenuOpen ? <ExpandLess /> : <ExpandMore />}
                            </ListItemButton>
                            <Collapse in={queueSubMenuOpen} timeout="auto" unmountOnExit>
                                <List component="div" disablePadding>
                                    <Link component={RouterLink} to="/eventbus-queues/dashboard" sx={{ textDecoration: 'none' }}>
                                        <ListItemButton sx={{ pl: 4, color: 'white' }}>
                                            <ListItemIcon>
                                                <Apps />
                                            </ListItemIcon>
                                            <ListItemText primary="Dashboard" />
                                        </ListItemButton>
                                    </Link>
                                    <Link component={RouterLink} to="/eventbus-queues/new" sx={{ textDecoration: 'none' }}>
                                        <ListItemButton sx={{ pl: 4, color: 'white' }}>
                                            <ListItemIcon>
                                                <Add />
                                            </ListItemIcon>
                                            <ListItemText primary="Add New" />
                                        </ListItemButton>
                                    </Link>
                                </List>
                            </Collapse>

                            <ListItemButton onClick={() => setMessageSubMenuOpen(!messageSubMenuOpen)}>
                                <ListItemIcon>
                                    <MailIcon />
                                </ListItemIcon>
                                <ListItemText primary="Messages" />
                                {messageSubMenuOpen ? <ExpandLess /> : <ExpandMore />}
                            </ListItemButton>
                            <Collapse in={messageSubMenuOpen} timeout="auto" unmountOnExit>
                                <List component="div" disablePadding>
                                    <Link component={RouterLink} to="/eventbus-messages/dashboard" sx={{ textDecoration: 'none' }}>
                                        <ListItemButton sx={{ pl: 4, color: 'white' }}>
                                            <ListItemIcon>
                                                <Apps />
                                            </ListItemIcon>
                                            <ListItemText primary="Dashboard" />
                                        </ListItemButton>
                                    </Link>
                                    <Link component={RouterLink} to="/eventbus-messages/new" sx={{ textDecoration: 'none' }}>
                                        <ListItemButton sx={{ pl: 4, color: 'white' }}>
                                            <ListItemIcon>
                                                <Add />
                                            </ListItemIcon>
                                            <ListItemText primary="Add New" />
                                        </ListItemButton>
                                    </Link>
                                    <Link component={RouterLink} to="/eventbus-messages/new" sx={{ textDecoration: 'none' }}>
                                        <ListItemButton sx={{ pl: 4, color: 'white' }}>
                                            <ListItemIcon>
                                                <Send />
                                            </ListItemIcon>
                                            <ListItemText primary="Send" />
                                        </ListItemButton>
                                    </Link>
                                </List>
                            </Collapse>
                        </List>
                    </Drawer>
                    <Main open={open}>
                        <DrawerHeader />
                        <Container>
                            <Outlet></Outlet>
                        </Container>
                    </Main>
                </Box>
            </ThemeProvider>
        </>
    );
}

export default BasicPage;