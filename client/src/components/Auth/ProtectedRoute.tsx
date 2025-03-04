import {useAppSelector} from "../../app/hooks.ts";
import {Navigate, useLocation} from "react-router-dom";
import React from "react";
import {Box, CircularProgress} from "@mui/material";

function ProtectedRoute( { children } : { children: React.ReactNode}) {
    const { user, isCheckLoading } = useAppSelector((state) => state.auth);
    const location = useLocation();

    if (isCheckLoading) {
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