import {Box, Container, SelectChangeEvent, useMediaQuery, useTheme} from "@mui/material";
import {useAppDispatch} from "../../app/hooks.ts";
import { ProductsFilter} from "../../app/Interfaces/productsTypes.ts";
import {getProductsAsync} from "../../app/storeSlices/productsSlice.ts";
import React, {useEffect, useState} from "react";
import ProductsFilterSidebar from "./ProductsFilterSidebar.tsx";
import ProductsList from "./ProductsList.tsx";

function ProductsPage() {
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('md'));
    const [sidebarOpen, setSidebarOpen] = useState(!isMobile);
    const [filters, setFilters] = useState<ProductsFilter>({
        Name: null
    });
    const [productsPerPageAmount, setProductsPerPageAmount] = useState(20);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [sortBehaviour, setSortBehaviour] = useState<string>("n");

    const dispatch = useAppDispatch();

    useEffect(() => {
        const fetchProducts = async (request: ProductsFilter) => {
            await dispatch(getProductsAsync(request)).unwrap();
        }

        fetchProducts(filters).catch(console.error);
        setCurrentPage(1);
    }, [dispatch, filters]);

    const handlePageChange = (event: React.ChangeEvent<unknown>, page: number) => {
        event.stopPropagation();
        setCurrentPage(page);
    };

    const handleSortSelect = (event: SelectChangeEvent) => {
        setSortBehaviour(event.target.value as string);
    };

    return (
        <Container maxWidth="xl" disableGutters>
        <Box
            minHeight="85vh"
            sx={{
                display: 'flex',
                flexDirection: "row",
                height: '100%'
        }}>
            <ProductsFilterSidebar
                filters={filters}
                onApply={setFilters}
                open={sidebarOpen}
                onClose={() => setSidebarOpen(false)}
                isMobile={isMobile}
                productsPerPage={productsPerPageAmount}
                onSliderChange={ (event: Event, newValue: number | number[]) => {
                    event.stopPropagation()
                    setProductsPerPageAmount(newValue as number);
                }}
                sortBehaviour={sortBehaviour}
                onSortChange={handleSortSelect}
            />

            <Box sx={{
                flexGrow: 1,
                my: "1%",
                mx: "1%"
            }}>
                <ProductsList
                    sortBehaviour={sortBehaviour}
                    onOpenSidebar ={() => setSidebarOpen(true)}
                    isMobile={isMobile}
                    productsPerPage={productsPerPageAmount}
                    currentPage={currentPage}
                    onPageChange={handlePageChange}
                />
            </Box>
        </Box>
        </Container>
    );
}

export default ProductsPage;