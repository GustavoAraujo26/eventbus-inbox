import { ArrowBackIos, ArrowForwardIos } from "@mui/icons-material";
import { Box, Button, ButtonGroup, FormControl, Grid, InputLabel, MenuItem, Select, TextField } from "@mui/material";

interface PaginationProps {
    enableNextPage: boolean,
    changePageData: (page: number, pageSize: number) => void,
    pageData: { currentPage: number, currentPageSize: number }
}

const AppPagination = ({ enableNextPage, changePageData, pageData }: PaginationProps) => {
    const rowsPerPage = [10, 25, 50, 100];

    const gotToNextPage = () => {
        const nextPage = pageData.currentPage + 1;
        changePageData(nextPage, pageData.currentPageSize);
    }

    const goToPreviousPage = () => {
        const previousPage = pageData.currentPage - 1;
        changePageData(previousPage, pageData.currentPageSize);
    }

    const changePageSize = (selectedPageSize: string | number) => {
        if (typeof selectedPageSize === 'number') {
            changePageData(1, selectedPageSize);
        }
        else {
            changePageData(1, +selectedPageSize);
        }
    }

    return (
        <>
            <Box padding={1}>
                <Grid container spacing={2} justifyContent="center">
                    <Grid item md={3}>
                        <FormControl sx={{ minWidth: '150px' }}>
                            <InputLabel variant="standard" htmlFor="page-size-select">Select the page size</InputLabel>
                            <Select id="page-size-select" value={pageData.currentPageSize} onChange={event => changePageSize(event.target.value)}>
                                {rowsPerPage.map(option => <MenuItem key={option} value={option}>
                                    {option}
                                </MenuItem>)}
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item md={4}>
                        <ButtonGroup variant="contained">
                            <Button disabled={pageData.currentPage == 1} onClick={goToPreviousPage}>
                                <ArrowBackIos />
                                Previous
                            </Button>
                            <Button disabled={true}>{pageData.currentPage}</Button>
                            <Button onClick={gotToNextPage} disabled={!enableNextPage}>
                                <ArrowForwardIos />
                                Next
                            </Button>
                        </ButtonGroup>
                    </Grid>
                </Grid>
            </Box>
        </>
    );
}

export default AppPagination;