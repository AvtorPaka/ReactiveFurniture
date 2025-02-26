import {Box, Button,  Divider} from "@mui/material";
import React, {useState} from "react";
import {LoginCredentials} from "../../Interfaces/authTypes.ts";
import AuthLayout from "./AuthLayout.tsx";
import AuthLogo from "./AuthLogo.tsx";
import AuthTextField from "./AuthTextField.tsx";
import AuthBottomLink from "./AuthBottomLink.tsx";


function Login() {
    const [loginCredentials, setLoginCredentials] = useState<LoginCredentials>({
        email: '',
        password: ''
    });

    function handleOnChange(e: React.ChangeEvent<HTMLInputElement>) {
        setLoginCredentials({
           ...loginCredentials,
           [e.target.name]: e.target.value
        });
    }

    function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        e.stopPropagation();

        if (loginCredentials.email.trim().length === 0 || loginCredentials.password.trim().length === 0) {
            return;
        }

        console.log("Submit credentials: " + loginCredentials.email + " " + loginCredentials.password );
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
                <AuthTextField
                    variant="outlined"
                    id="email"
                    label="Email Address"
                    name="email"
                    autoComplete="email"
                    value={loginCredentials.email}
                    onChange={handleOnChange}
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
                >
                    Sign In
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