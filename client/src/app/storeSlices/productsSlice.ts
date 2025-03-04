import {ProductsFilter, ProductsState} from "../Interfaces/productsTypes.ts";
import {createAsyncThunk, createSlice} from "@reduxjs/toolkit";
import {ProductsService} from "../services/ProductsService.ts";

const initialState: ProductsState =  {
    products: [],
    isLoading: false,
    error: null
};

export const getProductsAsync = createAsyncThunk(
    'products/get',
    async (request: ProductsFilter, { rejectWithValue }) => {
        try {
            return await ProductsService.getProducts(request);
        }
        catch (error) {
            return rejectWithValue(error instanceof Error ? error.message : "Unable to get products list.");
        }
    }
)

export const productsSlice = createSlice({
    name: "products",
    initialState,
    reducers: {
        clearError: (state) => {
            state.error = null;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getProductsAsync.pending, (state) => {
                state.isLoading = true;
                state.error = null;
            })
            .addCase(getProductsAsync.fulfilled, (state, action) => {
                state.isLoading = false;
                state.error = null;
                state.products = action.payload;
            })
            .addCase(getProductsAsync.rejected, (state, action) => {
                state.isLoading = false;
                state.error = action.payload as string;
            })
    }
});

export const { clearError } = productsSlice.actions;
export default productsSlice.reducer;