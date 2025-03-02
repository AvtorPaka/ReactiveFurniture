import React, {useEffect} from "react";
import {useAppDispatch, useAppSelector} from "../../app/hooks.ts";
import {Box, CircularProgress} from "@mui/material";
import {checkAuthAsync} from "../../app/storeSlices/authSlice.ts";
import {sessionKey} from "../../app/Interfaces/authTypes.ts";

function checkCookieExists(cookieName: string): boolean {
    if (typeof document === 'undefined') return false;
    return document.cookie
        .split(';')
        .some(cookie => cookie.trim().startsWith(`${cookieName}=`));
}

function PersistAuth( { children } : { children: React.ReactNode}) {
    const dispatch = useAppDispatch();
    const { isCheckLoading } = useAppSelector((state) => state.auth);

    useEffect(() => {
        const checkAuth = async() => {
            if (!checkCookieExists(sessionKey)) {
                return;
            }

            await dispatch(checkAuthAsync());
        }

        checkAuth().catch(console.error);
    }, [dispatch]);

    if (isCheckLoading) {
        return (
            <Box minHeight="90vh" display="flex" justifyContent="center" alignItems="center">
                <CircularProgress color="secondary"/>
            </Box>
        );
    }

    return (
      <>
          {children}
      </>
    );
}

export default PersistAuth;