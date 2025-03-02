import {useAppSelector} from "../../app/hooks.ts";
import {Navigate, useLocation} from "react-router-dom";
import React from "react";
import {Box, CircularProgress} from "@mui/material";

function ProtectedRoute( { children } : { children: React.ReactNode}) {
    const { user, isLoading } = useAppSelector((state) => state.auth);
    const location = useLocation();

    if (isLoading) {
        return (
            <Box
                display="flex"
                justifyContent="center"
                alignItems="center"
                minHeight="100vh"
            >
                <CircularProgress color="secondary"/>
            </Box>
        );
    }

    if (!user) {
        return <Navigate to="/sign-in" state={{ from: location }} replace />;
    }

    return children;
}

export default ProtectedRoute;