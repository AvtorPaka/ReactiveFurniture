import {Box, Container, Typography} from '@mui/material'

function Footer() {
    return (
        <Box sx={{
            backgroundColor: 'primary.dark',
            color: 'inherit',
            borderTop: "2px solid",
            borderColor: "divider",
            py: "1%",
            mt: 'auto'
        }}>
            <Container maxWidth="lg">
                <Typography textAlign="center">
                    © {new Date().getFullYear()} Reactive Furniture. All rights reserved.
                </Typography>
            </Container>
        </Box>
    );
}

export default Footer;