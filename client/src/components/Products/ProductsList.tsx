import {Box, Button, CircularProgress, Alert, Grid2, AlertTitle, Pagination} from '@mui/material';
import FilterListIcon from '@mui/icons-material/FilterList';
import ProductItem from './ProductItem';
import { useAppSelector } from '../../app/hooks';
import React from "react";

interface ProductListProps {
    onOpenSidebar: () => void;
    isMobile: boolean;
    productsPerPage: number,
    currentPage: number,
    onPageChange: (event: React.ChangeEvent<unknown>, page: number) => void,
    sortBehaviour: string
}


function ProductsList({ onOpenSidebar, isMobile, productsPerPage, currentPage, onPageChange, sortBehaviour }: ProductListProps) {
    const { products, isLoading, error } = useAppSelector((state) => state.products);


    const totalProducts = products.length;
    const totalPages = Math.ceil(totalProducts / productsPerPage);
    const startIndex = (currentPage - 1) * productsPerPage;
    const endIndex = startIndex + productsPerPage;

    const productsCopy = products.slice();
    if (sortBehaviour === "pa") {
        productsCopy .sort((a, b) => a.price - b.price);
    } else if (sortBehaviour === "pd") {
        productsCopy .sort((a, b) => b.price - a.price);
    } else if (sortBehaviour === "da") {
        productsCopy .sort(function (a, b) {
            return Date.parse(a.release_date) - Date.parse(b.release_date);
        });
    } else if (sortBehaviour === "dd") {
        productsCopy .sort(function (a, b) {
            return Date.parse(b.release_date) - Date.parse(a.release_date);
        });
    } else if (sortBehaviour === "na") {
        productsCopy .sort((a,b) => (a.name > b.name) ? 1 : ((b.name > a.name) ? -1 : 0))
    } else if (sortBehaviour === "nd") {
        productsCopy .sort((a,b) => (a.name < b.name) ? 1 : ((b.name < a.name) ? -1 : 0))
    }

    const currentProducts = productsCopy.slice(startIndex, endIndex);

    if (isLoading) {
        return (
            <Box minHeight="85vh" display="flex" justifyContent="center" alignItems="center">
                <CircularProgress color="secondary"/>
            </Box>
        );
    }

    if (error) {
        return (
            <>
            {isMobile && (
                <Button
                    startIcon={<FilterListIcon />}
                    onClick={onOpenSidebar}
                    sx={{ mb: 2 }}
                    color="secondary"
                >
                    Show Filters
                </Button>
            )}


            <Box minHeight="85vh" display="flex" justifyContent="center" alignItems="center">
                <Alert
                    severity="warning"
                    sx={{
                        border: "1px solid",
                        borderColor: "divider",
                        '& .MuiAlert-message': {
                            overflow: 'hidden',
                        },

                    }}
                >
                    <AlertTitle>Unable to show products.</AlertTitle>
                    {error}
                </Alert>
            </Box>
            </>
        );
    }

    return (
            <Box sx={{
                display: 'flex',
                flexDirection: 'column',
                height: '100%',
                minHeight: '70vh'
            }}>
            {isMobile && (
                <Button
                    startIcon={<FilterListIcon />}
                    onClick={onOpenSidebar}
                    sx={{ mb: 2 }}
                    color="secondary"
                >
                    Show Filters
                </Button>
            )}

            <Grid2
                container
                spacing={3}
                sx={{
                    flexGrow: 1,
                    overflow: 'auto',
                    pb: 4,
                    justifyContent: "center",
                    alignContent: 'flex-start'
                }}
            >
                {currentProducts.map((product) => (
                    <ProductItem key={product.id} product={product} />
                ))}
            </Grid2>

            <Box
                sx={{
                    position: 'sticky',
                    bottom: 0,
                    display: 'flex',
                    justifyContent: "center",
                    backgroundColor: 'background.default',
                    zIndex: 1,
                    pt: 2,
                    pb: 2,
                    borderTop: '1px solid',
                    borderColor: 'divider'
            }}
            ><Pagination
                count={totalPages}
                page={currentPage}
                onChange={onPageChange}
                color="secondary"
                size={isMobile ? "small" : "medium"}
                sx={{
                    '& .MuiPaginationItem-root': {
                        color: 'text.primary',
                        '&.Mui-selected': {
                            fontWeight: 'bold',
                        }
                    }
            }}
            />
            </Box>

        </Box>
    );
}

export default ProductsList;