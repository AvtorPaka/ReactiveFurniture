import {ProductsFilter, ProductsFilterViolations} from "../../app/Interfaces/productsTypes.ts";
import React, {useState} from "react";
import CloseIcon from '@mui/icons-material/Close';
import {
    Alert,
    AlertTitle,
    Box,
    Button,
    Drawer,
    IconButton, ListItemIcon, MenuItem, Select,
    SelectChangeEvent,
    Slider,
    TextField,
    Typography
} from "@mui/material";
import {useAppSelector} from "../../app/hooks.ts";
import ArrowUpwardIcon from '@mui/icons-material/ArrowUpward';
import ArrowDownwardIcon from '@mui/icons-material/ArrowDownward';

interface ProductsFilterProps {
    open: boolean;
    onClose: () => void;
    filters: ProductsFilter;
    onApply: (filters: ProductsFilter) => void;
    isMobile: boolean;
    productsPerPage: number,
    onSliderChange: (event: Event, newValue: number | number[]) => void;
    sortBehaviour: string,
    onSortChange: (event: SelectChangeEvent) => void;
}

function ProductsFilterSidebar({ open, onClose, filters, onApply, isMobile, productsPerPage, onSliderChange, sortBehaviour, onSortChange}: ProductsFilterProps) {
    const { isLoading } = useAppSelector((state) => state.products);
    const [localFilters, setLocalFilters] = useState<ProductsFilter>(filters);
    const [ filterViolations, setFilterViolations] = useState<ProductsFilterViolations>({
            priceViolation: null,
            dateViolation: null
    });

    function isValidFilter(): boolean {
        const curFilterViolations: ProductsFilterViolations =  {
            priceViolation: null,
            dateViolation: null
        }

        if (localFilters.PriceMinRange && localFilters.PriceMinRange < 0) {
            curFilterViolations.priceViolation = "Minimum price must be greater that zero."
        }
        if (localFilters.PriceMinRange && !localFilters.PriceMaxRange) {
            curFilterViolations.priceViolation = "Maximum price must be set."
        }
        if (localFilters.PriceMaxRange && localFilters.PriceMaxRange < 0) {
            curFilterViolations.priceViolation = "Maximum price must be greater that zero."
        }
        if (localFilters.PriceMinRange && localFilters.PriceMaxRange) {
            if (localFilters.PriceMaxRange < localFilters.PriceMinRange) {
                curFilterViolations.priceViolation = "Minimum price must be less than maximum price."
            }
        }

        if (localFilters.ReleaseYearMinRange && localFilters.ReleaseYearMaxRange
            && localFilters.ReleaseYearMinRange > localFilters.ReleaseYearMaxRange) {
            curFilterViolations.dateViolation = "Minimum release year must be less than maximum release year."
        }


        setFilterViolations(curFilterViolations);

        return (!curFilterViolations.dateViolation && !curFilterViolations.priceViolation);
    }

    const handleApply = () => {

        if (!isValidFilter()) {
            return;
        }

        onApply(localFilters);
        if (isMobile) onClose();
    };

    function handleOnChange(e: React.ChangeEvent<HTMLInputElement>) {
        setLocalFilters({
            ...localFilters,
            [e.target.name]: e.target.value
        });

        if (e.target.name === "ReleaseYearMinRange" || e.target.name === "ReleaseYearMaxRange") {
            setFilterViolations({
                ...filterViolations,
                dateViolation: null
            })
        }

        if (e.target.name === "PriceMaxRange" || e.target.name === "PriceMinRange") {
            setFilterViolations({
                ...filterViolations,
                priceViolation: null
            })
        }
    }

    return (
        <Drawer
            variant={isMobile ? "temporary" : "persistent"}
            open={isMobile ? open : true}
            onClose={onClose}
            anchor="left"
            sx={{
                width: 300,
                flexShrink: 0,
                '& .MuiDrawer-paper': {
                    width: 300,
                    boxSizing: 'border-box',
                    borderRight: "1px solid",
                    borderLeft: "1px solid",
                    borderColor: "divider",
                    p: 2,
                    position: 'relative',
                    height: "100vh",
                    ...(isMobile ? {} : {
                        position: 'sticky',
                        top: 0,
                        left: 0,
                        zIndex: 1
                    })
                }
            }}
        >
            <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
                <Typography variant="h6">Filter</Typography>
                {isMobile && <IconButton onClick={onClose}><CloseIcon /></IconButton>}
            </Box>

            <TextField
                fullWidth
                name="Name"
                label="Product Name"
                value={localFilters.Name || ''}
                onChange={handleOnChange}
                sx={{
                    mb: 2
                }}
            />

            <Typography variant="subtitle2" mb={1}>Price Range</Typography>
            {
                filterViolations.priceViolation &&
                <Alert
                    severity="error"
                    sx={{
                        border: "1px solid",
                        borderColor: "divider",
                        '& .MuiAlert-message': {
                            overflow: 'hidden',
                        },
                        mb: 1,
                        pb: 0
                    }}
                >
                    <AlertTitle>{filterViolations.priceViolation}</AlertTitle>
                </Alert>
            }
            <Box display="flex" gap={2} mb={3}>
                <TextField
                    type="number"
                    label="Min"
                    name="PriceMinRange"
                    error={!!filterViolations.priceViolation}
                    value={localFilters.PriceMinRange || ''}
                    onChange={handleOnChange}
                />
                <TextField
                    type="number"
                    label="Max"
                    name="PriceMaxRange"
                    error={!!filterViolations.priceViolation}
                    value={localFilters.PriceMaxRange || ''}
                    onChange={handleOnChange}
                />
            </Box>

            <Typography variant="subtitle2" mb={1}>Release Year</Typography>
            {
                filterViolations.dateViolation &&
                <Alert
                    severity="error"
                    sx={{
                        border: "1px solid",
                        borderColor: "divider",
                        '& .MuiAlert-message': {
                            overflow: 'hidden',
                        },
                        mb: 1,
                        pb: 0
                    }}
                >
                    <AlertTitle>{filterViolations.dateViolation}</AlertTitle>
                </Alert>
            }
            <Box display="flex" gap={2} mb={2}>
                <TextField
                    type="number"
                    label="From"
                    name="ReleaseYearMinRange"
                    error={!!filterViolations.dateViolation}
                    value={localFilters.ReleaseYearMinRange || ''}
                    onChange={handleOnChange}
                />
                <TextField
                    type="number"
                    label="To"
                    name="ReleaseYearMaxRange"
                    error={!!filterViolations.dateViolation}
                    value={localFilters.ReleaseYearMaxRange || ''}
                    onChange={handleOnChange}
                />
            </Box>

            <Button
                fullWidth
                variant="contained"
                onClick={handleApply}
                disabled={isLoading
                    || !!filterViolations.dateViolation
                    || !!filterViolations.priceViolation
                }
                sx={{
                    mb: 2,
                    border: "1px solid",
                    borderColor: "divider",
                    mt: 1,
                    fontSize: "0.9rem"
                }}
            >
                Apply
            </Button>

            <Typography variant="subtitle2" mb={1}>Products on page</Typography>
            <Slider
                color="secondary"
                value={productsPerPage}
                onChange={onSliderChange}
                aria-labelledby="input-slider"
                valueLabelDisplay="auto"
                marks
                shiftStep={20}
                step={10}
                min={20}
                max={50}
            />

            <Typography variant="subtitle2" my={2}>Order</Typography>
            <Select
                variant="standard"
                labelId="sort-select"
                id="sort-select"
                value={sortBehaviour}
                label="Ordef"
                onChange={onSortChange}
            >
                <MenuItem value="n">None</MenuItem>
                <MenuItem value="pa">
                    <ListItemIcon>
                        <ArrowUpwardIcon fontSize="small"/>
                    </ListItemIcon>
                    Price
                </MenuItem>
                <MenuItem value="pd">
                    <ListItemIcon>
                        <ArrowDownwardIcon fontSize="small"/>
                    </ListItemIcon>
                    Price
                </MenuItem>
                <MenuItem value="da">
                    <ListItemIcon>
                        <ArrowUpwardIcon fontSize="small"/>
                    </ListItemIcon>
                    Release date
                </MenuItem>
                <MenuItem value="dd">
                    <ListItemIcon>
                        <ArrowDownwardIcon fontSize="small"/>
                    </ListItemIcon>
                    Release date
                </MenuItem>
                <MenuItem value="na">
                    <ListItemIcon>
                        <ArrowUpwardIcon fontSize="small"/>
                    </ListItemIcon>
                    Name
                </MenuItem>
                <MenuItem value="nd">
                    <ListItemIcon>
                        <ArrowDownwardIcon fontSize="small"/>
                    </ListItemIcon>
                    Name
                </MenuItem>
            </Select>
        </Drawer>
    );
}

export default ProductsFilterSidebar;