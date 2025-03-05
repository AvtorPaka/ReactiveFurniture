import {Alert, AlertTitle, Box, Button, CircularProgress, Divider} from "@mui/material";
import React, {useEffect, useState} from "react";
import {LoginCredentials} from "../../app/Interfaces/authTypes.ts";
import AuthLayout from "./AuthLayout.tsx";
import AuthLogo from "./AuthLogo.tsx";
import AuthTextField from "./AuthTextField.tsx";
import AuthBottomLink from "./AuthBottomLink.tsx";
import {useAppDispatch, useAppSelector} from "../../app/hooks.ts";
import {clearError, loginAsync} from "../../app/storeSlices/authSlice.ts";
import {useNavigate} from "react-router-dom";


function Login() {
    const [loginCredentials, setLoginCredentials] = useState<LoginCredentials>({
        email: '',
        password: ''
    });
    const dispatch = useAppDispatch();
    const { user, isLoading, error } = useAppSelector((state) => state.auth);

    const navigate = useNavigate();

    useEffect(() => {
        if (user) {
            navigate("/");
        }
    }, [user, navigate]);

    useEffect(() => {
        dispatch(clearError());
    }, [dispatch]);


    function handleOnChange(e: React.ChangeEvent<HTMLInputElement>) {
        setLoginCredentials({
           ...loginCredentials,
           [e.target.name]: e.target.value
        });

        if (error && (loginCredentials.email || loginCredentials.password)) {
            dispatch(clearError());
        }
    }

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        e.stopPropagation();

        await dispatch(loginAsync(loginCredentials));
    }


    return (
        <AuthLayout>
            <AuthLogo logoText="Sign In"/>
            <Divider sx={{ width: '80%', my: 1 }} />
            <Box component="form"
                 onSubmit={handleSubmit}
                 sx={{
                     width: '95%',
                     display: 'flex',
                     flexDirection: 'column',
                     gap: 2
            }}>

                {error && (
                    <Alert
                        severity="error"
                        sx={{
                            border: "1px solid",
                            borderColor: "divider",
                            '& .MuiAlert-message': {
                                overflow: 'hidden'
                            }
                        }}
                    >
                        <AlertTitle>Sign-in Failed.</AlertTitle>
                        {error}
                    </Alert>
                )}

                <AuthTextField
                    variant="outlined"
                    id="email"
                    label="Email Address"
                    name="email"
                    autoComplete="email"
                    value={loginCredentials.email}
                    onChange={handleOnChange}
                    disabled={isLoading}
                />
                <AuthTextField
                    variant="outlined"
                    name="password"
                    label="Password"
                    type="password"
                    id="password"
                    autoComplete="password"
                    value={loginCredentials.password}
                    onChange={handleOnChange}
                    disabled={isLoading}
                />

                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    size="large"
                    sx={{
                        border: "1px solid",
                        borderColor: "divider",
                        mt: 1,
                        py: 1.5,
                        fontSize: '1rem',
                    }}
                    disabled={isLoading
                        || !loginCredentials.email.trim()
                        || !loginCredentials.password.trim()}
                >
                    {isLoading ? <CircularProgress size={20} color="secondary"/> : "Sign In"}
                </Button>
                <Divider sx={{ width: '100%', my: 1 }}>or</Divider>
                <AuthBottomLink
                    linkPath="/register"
                    linkText="Don't have an account? Sign Up"
                />
            </Box>
        </AuthLayout>
    )
}

export default Login;