import {
    AppBar,
    Toolbar,
    Typography,
    Button,
    Container,
    Box,
} from '@mui/material';
import LoginIcon from '@mui/icons-material/Login';
import PersonAddOutlinedIcon from '@mui/icons-material/PersonAddOutlined';
import { Link } from "react-router-dom";
import LogoIcon from "../Icons/LogoIcon.tsx";
import HeaderAccountMenu from "./HeaderAccountMenu.tsx";

function Header ( {logged = false} ) {

    return (
        <AppBar
            position="sticky"
            sx={{
                borderBottom: '2px solid',
                borderColor: "divider",
                backgroundColor: "primary"
            }}
        >
            <Container maxWidth="xl">
                <Toolbar disableGutters>
                    <Button
                        component={Link}
                        to="/"
                        variant="text"
                        color="inherit"
                        sx={{
                            display: 'flex',
                            alignItems: 'center',
                            textTransform: 'none',
                            '&:hover': {
                                backgroundColor: 'rgba(255, 255, 255, 0.08)'
                            },
                            '& .MuiTypography-root': {
                                fontSize: { xs: '1rem', sm: '1.25rem' }
                            }
                        }}
                    >
                        <LogoIcon sx={{ fontSize: 35 }} />
                        <Typography variant="h6">
                            Reactive Furniture
                        </Typography>
                    </Button>
                    <Box sx={{
                        ml: 'auto',
                        display: 'flex',
                        alignItems: 'center',
                        gap: [1, 1.5]
                    }}>
                        {logged ?
                            (
                                <HeaderAccountMenu/>
                            ) :
                            (
                                <Box sx={{display:"flex", gap: [1, 1.5]}}>
                                <Button
                                    color="inherit"
                                    variant="contained"
                                    sx={{
                                        fontSize: { xs: '0.8rem', sm: '0.85rem' }
                                    }}
                                    startIcon={<LoginIcon/>}
                                    component={Link}
                                    to="/sign-in"
                                >
                                    Sign In
                                </Button>
                                <Button
                                    color="inherit"
                                    variant="contained"
                                    sx={{
                                        fontSize: { xs: '0.8rem', sm: '0.85rem' }
                                    }}
                                    component={Link}
                                    startIcon={<PersonAddOutlinedIcon/>}
                                    to="/register"
                                >
                                    Register
                                </Button>
                                </Box>
                            )
                        }
                        </Box>
                </Toolbar>
            </Container>
        </AppBar>
    );
}

export default Header;