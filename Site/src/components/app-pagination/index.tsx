import { ArrowBackIos, ArrowForwardIos } from "@mui/icons-material";
import { Box, Button, ButtonGroup, FormControl, Grid, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import { useEffect, useState } from "react";

interface PaginationProps {
    rowsFounded: boolean,
    changePageData: (page: number, pageSize: number) => void
}

const AppPagination = ({ rowsFounded, changePageData }: PaginationProps) => {
    const rowsPerPage = [10, 25, 50, 100];
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [currentPageSize, setCurrentPageSize] = useState<number>(10);

    const gotToNextPage = () => {
        const nextPage = currentPage + 1;
        setCurrentPage(nextPage);
    }

    const goToPreviousPage = () => {
        const previousPage = currentPage - 1;
        setCurrentPage(previousPage);
    }

    const changePageSize = (selectedPageSize: string | number) => {
        if (typeof selectedPageSize === 'number') {
            setCurrentPageSize(selectedPageSize);
        }
        else {
            setCurrentPageSize(+selectedPageSize);
        }
    }

    useEffect(() => {
        changePageData(currentPage, currentPageSize);
    }, [currentPage, currentPageSize]);

    return (
        <>
            <Box padding={1}>
                <Grid container spacing={2} justifyContent="center">
                    <Grid item md={2}>
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
                        <ButtonGroup variant="outlined">
                            <Button disabled={currentPage == 1} onClick={goToPreviousPage}>
                                <ArrowBackIos />
                                Previous
                            </Button>
                            <Button disabled={true}>{currentPage}</Button>
                            <Button onClick={gotToNextPage} disabled={!rowsFounded}>
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