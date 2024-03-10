import { useEffect, useState } from "react";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import { Apps, HomeOutlined } from "@mui/icons-material";
import AppBreadcrumb from "../../components/app-breadcrumb";
import EventBusMessageTable from "../../components/eventbus-message-table";
import { useAppDispatch, useAppSelector } from "../../state/hooks/app-hooks";
import { setEventBusMessageListRequest } from "../../state/slices/eventbus-message/eventbus-message-list-request-slice";
import { RootState } from "../../state/app-store";

const EventBusMessagesDashboard = () => {
    const dispatch = useAppDispatch();
    
    const [breadcrumbItems, setBreadcrumbItems] = useState<AppBreadcrumbItem[]>([]);
    const [showTable, setShowTable] = useState<boolean>(false);

    const messageRequest = useAppSelector((state: RootState) => state.eventbusMessageListRequest);

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
        dispatch(setEventBusMessageListRequest({
            queueId: null,
            page: 1,
            pageSize: 10,
            creationDateSearch: null,
            updateDateSearch: null,
            statusToSearch: null,
            typeMatch: null
        }));
        buildbreadcrumb();
    }, []);

    useEffect(() => {
        setShowTable(true);
    }, [messageRequest]);

    return (
        <>
            <AppBreadcrumb breadcrumbItems={breadcrumbItems} />
            {showTable && <EventBusMessageTable gridSize={12} showQueue={true} showFilter={true} showActions={true} />}
        </>
    );
}

export default EventBusMessagesDashboard;