import {Avatar, Typography} from "@mui/material";
import LogoIcon from "../Icons/LogoIcon.tsx";

function AuthLogo({logoText}: { logoText: string }) {
    return (
        <>
        <Avatar sx={{ m: 1, backgroundColor: 'secondary.main' }}>
            <LogoIcon/>
        </Avatar>
        <Typography component="h1" variant="h4" sx={{
            mb: 1,
            fontSize: { xs: '1.75rem', sm: '2rem' }
        }}>
            {logoText}
        </Typography>
        </>
    );
}

export default AuthLogo;