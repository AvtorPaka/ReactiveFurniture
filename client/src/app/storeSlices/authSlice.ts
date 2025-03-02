import {AuthState, LoginCredentials, RegisterCredentials} from "../Interfaces/authTypes.ts";
import {createAsyncThunk, createSlice} from "@reduxjs/toolkit";
import {AuthService} from "../services/AuthService.ts";

const initialState: AuthState = {
    user: null,
    isLoading: false,
    isCheckLoading: false,
    error: null
};

export const loginAsync = createAsyncThunk(
    'auth/login',
    async (request: LoginCredentials, { rejectWithValue }) => {
        try {
            return await AuthService.login(request);
        }
        catch (error) {
            return rejectWithValue(error instanceof Error ? error.message : "Sign-in failed");
        }
    }
)

export const registerAsync = createAsyncThunk(
    'auth/register',
    async (request: RegisterCredentials, { rejectWithValue }) => {
        try {
            return await AuthService.register(request);
        }
        catch (error) {
            return rejectWithValue(error instanceof Error ? error.message : "Sign-up failed");
        }
    }
)

export const logoutAsync= createAsyncThunk(
    'auth/logout',
    async (_, { rejectWithValue }) => {
        try {
            return await AuthService.logout();
        }
        catch (error) {
            return rejectWithValue(error instanceof Error ? error.message : "Logout failed");
        }
    }
)

export const checkAuthAsync = createAsyncThunk(
    'auth/check',
    async (_, {rejectWithValue}) => {
        try {
            return await AuthService.checkAuth();
        }
        catch (error) {
            return rejectWithValue(error instanceof Error ? error.message : "Authentication failed");
        }
    }
)

export const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        clearError: (state) => {
            state.error = null;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(loginAsync.pending, (state) => {
                state.isLoading = true;
                state.error = null;
            })
            .addCase(loginAsync.fulfilled, (state, action) => {
                state.isLoading = false;
                state.error = null;
                state.user = action.payload;
            })
            .addCase(loginAsync.rejected, (state, action) => {
                state.isLoading = false;
                state.error = action.payload as string;
            })
            .addCase(registerAsync.pending, (state) => {
                state.isLoading = true;
                state.error = null;
            })
            .addCase(registerAsync.fulfilled, (state) => {
                state.isLoading = false;
                state.error = null;
            })
            .addCase(registerAsync.rejected, (state, action) => {
                state.isLoading = false;
                state.error = action.payload as string;
            })
            .addCase(checkAuthAsync.pending, (state) => {
                state.isCheckLoading = true;
                state.error = null;
            })
            .addCase(checkAuthAsync.fulfilled, (state, action) => {
                state.isCheckLoading = false;
                state.error = null;
                state.user = action.payload;
            })
            .addCase(checkAuthAsync.rejected, (state, action) => {
                state.isCheckLoading = false;
                state.user = null;
                state.error = action.payload as string;
            })
            .addCase(logoutAsync.pending, (state) => {
                state.isLoading = true;
                state.error = null;
            })
            .addCase(logoutAsync.fulfilled, (state) => {
                state.isLoading = false;
                state.error = null;
                state.user = null;
            })
            .addCase(logoutAsync.rejected, (state, action) => {
                state.isLoading = false;
                state.error = action.payload as string;
                state.user = null;
            })
    }
})

export const { clearError } = authSlice.actions;
export default authSlice.reducer;