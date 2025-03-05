import {configureStore} from "@reduxjs/toolkit";
import authReducer from "./storeSlices/authSlice.ts";
import productsReducer from "./storeSlices/productsSlice.ts"


export const store = configureStore({
    reducer: {
        auth: authReducer,
        products: productsReducer
    }
})

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;