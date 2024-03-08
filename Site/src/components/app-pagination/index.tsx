import { ArrowBackIos, ArrowForwardIos } from "@mui/icons-material";
import { Box, Button, ButtonGroup, FormControl, Grid, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import { useEffect, useState } from "react";

interface PaginationProps {
    enableNextPage: boolean,
    changePageData: (page: number, pageSize: number) => void,
    loadData: () => void
}

const AppPagination = ({ enableNextPage, changePageData, loadData }: PaginationProps) => {
    const rowsPerPage = [10, 25, 50, 100];
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [currentPageSize, setCurrentPageSize] = useState<number>(10);

    const gotToNextPage = () => {
        const nextPage = currentPage + 1;
        setCurrentPage(nextPage);
        changePageData(nextPage, currentPageSize);
    }

    const goToPreviousPage = () => {
        const previousPage = currentPage - 1;
        setCurrentPage(previousPage);
        changePageData(previousPage, currentPageSize);
    }

    const changePageSize = (selectedPageSize: string | number) => {
        if (typeof selectedPageSize === 'number') {
            changePageData(currentPage, selectedPageSize);
            setCurrentPageSize(selectedPageSize);
        }
        else {
            changePageData(currentPage, +selectedPageSize);
            setCurrentPageSize(+selectedPageSize);
        }
    }

    useEffect(() => {
        loadData();
    }, [currentPage, currentPageSize]);

    return (
        <>
            <Box padding={1}>
                <Grid container spacing={2} justifyContent="center">
                    <Grid item md={3}>
                        <FormControl sx={{ minWidth: '150px' }}>
                            <InputLabel variant="standard" htmlFor="page-size-select">Select the page size</InputLabel>
                            <Select id="page-size-select" value={currentPageSize} onChange={event => changePageSize(event.target.value)}>
                                {rowsPerPage.map(option => <MenuItem key={option} value={option}>
                                    {option}
                                </MenuItem>)}
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item md={4}>
                        <ButtonGroup variant="contained">
                            <Button disabled={currentPage == 1} onClick={goToPreviousPage}>
                                <ArrowBackIos />
                                Previous
                            </Button>
                            <Button disabled={true}>{currentPage}</Button>
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