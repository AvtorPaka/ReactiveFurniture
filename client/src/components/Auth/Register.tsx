import {Box, Button, Divider} from "@mui/material";
import React, {useState} from "react";
import {RegisterCredentials} from "../../Interfaces/authTypes.ts";
import AuthLayout from "./AuthLayout.tsx";
import AuthLogo from "./AuthLogo.tsx";
import AuthBottomLink from "./AuthBottomLink.tsx";
import AuthTextField from "./AuthTextField.tsx";

function Register() {
    const [registerCredentials, serRegisterCredentials] = useState<RegisterCredentials>({
        userName: '',
        email: '',
        password: '',
        confirmPassword: ''
    })

    function handleOnChange(e: React.ChangeEvent<HTMLInputElement>) {
        serRegisterCredentials({
            ...registerCredentials,
            [e.target.name]: e.target.value
        });
    }

    function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        e.stopPropagation();

        if (
            registerCredentials.email.trim().length === 0
            || registerCredentials.password.trim().length === 0
            || registerCredentials.userName.trim().length === 0
            || registerCredentials.confirmPassword.trim().length == 0
        ) {
            return;
        }

        console.log("Submit credentials: " + ' ' + registerCredentials.userName + ' ' +  registerCredentials.email + " " + registerCredentials.password );
    }

    return (
        <AuthLayout>
            <AuthLogo
                logoText="Sign Up"
            />
            <Divider sx={{ width: '80%', my: 1 }} />
            <Box component="form"
                 onSubmit={handleSubmit}
                 sx={{
                     width: '95%',
                     display: 'flex',
                     flexDirection: 'column',
                     gap: 0
                 }}>
                <AuthTextField
                    variant="outlined"
                    name="userName"
                    label="Username"
                    id="userName"
                    autoComplete="Username"
                    value={registerCredentials.userName}
                    onChange={handleOnChange}
                />
                <AuthTextField
                    variant="outlined"
                    id="email"
                    label="Email Address"
                    name="email"
                    autoComplete="email"
                    value={registerCredentials.email}
                    onChange={handleOnChange}
                />
                <AuthTextField
                    variant="outlined"
                    name="password"
                    label="Password"
                    type="password"
                    id="password"
                    autoComplete="password"
                    value={registerCredentials.password}
                    onChange={handleOnChange}
                />
                <AuthTextField
                    variant="outlined"
                    name="confirmPassword"
                    label="Confirm password"
                    type="password"
                    id="confirmPassword"
                    autoComplete="password"
                    value={registerCredentials.confirmPassword}
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
                    Sign Up
                </Button>
            </Box>
            <Divider sx={{ width: '100%', my: 1 }}>or</Divider>
            <AuthBottomLink
                linkPath="/sign-in"
                linkText="Already have an account? Sign In"
            />
        </AuthLayout>
    );
}

export default Register;