import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import { Apps, HomeOutlined } from "@mui/icons-material";
import { Backdrop, CircularProgress } from "@mui/material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import EventBusMessageTable from "../../components/eventbus-message-table";

const EventBusMessagesDashboard = () => {
    const navigateTo = useNavigate();

    const [breadcrumbItems, setBreadcrumbItems] = useState<AppBreadcrumbItem[]>([]);

    const buildbreadcrumb = () => {
        const home: AppBreadcrumbItem = {
            id: 1,
            icon: <HomeOutlined sx={{ mr: 0.5 }} />,
            text: 'Home',
            goTo: '/',
            isPage: false
        };

        const messageDashboard: AppBreadcrumbItem = {
            id: 2,
            icon: <Apps sx={{ mr: 0.5 }} />,
            text: 'Event Bus Messages Dashboard',
            goTo: '',
            isPage: true
        };

        const newList: AppBreadcrumbItem[] = [home, messageDashboard];

        setBreadcrumbItems(newList);
    }

    useEffect(() => {
        buildbreadcrumb();
    }, []);

    return (
        <>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            <EventBusMessageTable gridSize={12} showQueue={true} showFilter={true} currentQueueId={null} showActions={true} />
        </>
    );
}

export default EventBusMessagesDashboard;