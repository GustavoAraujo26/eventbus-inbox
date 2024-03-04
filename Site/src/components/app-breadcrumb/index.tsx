import { Breadcrumbs, Container, Paper, Typography } from "@mui/material";
import AppBreadcrumbItem from "../../interfaces/app-breadcrumb-item";
import { Link as RouterLink } from "react-router-dom";
import { Link } from "@mui/icons-material";

interface BreadcrumbProps {
    breadcrumbItems: AppBreadcrumbItem[]
}

const AppBreadcrumb = ({ breadcrumbItems }: BreadcrumbProps) => {
    const buildCurrentPage = (item: AppBreadcrumbItem) => {
        return (<Typography key={item.id} sx={{ display: 'flex', alignItems: 'center' }}>
            {item.icon}
            {item.text}
        </Typography>);
    }

    const buildPreviousPage = (item: AppBreadcrumbItem) => {
        return (
            <Link key={item.id} component={RouterLink} to={item.goTo} sx={{ display: 'flex', alignItems: 'center', textDecoration: 'none' }}>
                {item.icon}
                {item.text}
            </Link>
        );
    }

    const buildbreadcrumbItem = (item: AppBreadcrumbItem) => {
        if (item.isPage === true) {
            return buildCurrentPage(item);
        }
        else {
            return buildPreviousPage(item);
        }
    }

    return (
        <>
            <Paper elevation={3}>
                <Container sx={{ padding: 1 }}>
                    {breadcrumbItems.length > 0 ? <Breadcrumbs separator=">">
                        {breadcrumbItems.map(item => buildbreadcrumbItem(item))}
                    </Breadcrumbs> : null}
                </Container>
            </Paper>
            <br />
        </>
    );
}

export default AppBreadcrumb;