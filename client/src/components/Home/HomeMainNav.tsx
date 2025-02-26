import { Box, Button, Container, Typography} from '@mui/material';
import { Link } from 'react-router-dom';
import { ArrowForward } from '@mui/icons-material';

function HomeMainNav( {logged = true}) {
    return (
        <Box
            sx={{
                backgroundColor: 'inherit',
                py: "2%",
                textAlign: 'center',
            }}
        >
            <Container maxWidth="md">
                <Typography variant="h2" component="h1" gutterBottom sx={{ fontWeight: 700 }}>
                    Reactive Furniture for Modern Living
                </Typography>
                <Typography variant="h5" sx={{ mb: 4 }}>
                    Discover our collection of premium furniture
                </Typography>
                <Button
                    variant="contained"
                    component={Link}
                    to="/products"
                    color="secondary"
                    size="large"
                    disabled={!logged}
                    endIcon={<ArrowForward />}
                    sx={{
                        px: 6,
                        py: 2,
                        fontSize: '1.1rem',
                        '&:hover': { transform: 'scale(1.05)' },
                        transition: 'transform 0.3s',
                    }}
                >
                    Shop Now
                </Button>
            </Container>
        </Box>
    );
}

export default HomeMainNav;