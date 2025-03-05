import {Card, CardContent, Typography, Box, Grid2, Divider} from '@mui/material';
import { Product } from '../../app/Interfaces/productsTypes';

interface ProductItemProps {
    product: Product;
}

function ProductItem({ product }: ProductItemProps) {
    return (
        <Grid2
            key={product.id}
            size={{
                xs:12,
                sm: 6,
                md: 4,
                lg: 3
            }}
        >
        <Card sx={{
            height: '100%',
            display: 'flex',
            flexDirection: 'column',
            border: "1px solid",
            borderColor: "divider",
            borderRadius: 2,
            pb: 0,
            transition: 'all 0.3s ease',
            '&:hover': {
                borderRadius: 4,
                borderColor: "#333333",
            }
        }}>
            <CardContent sx={{ flexGrow: 1 }}>
                <Box display="flex" flexDirection="column" justifyContent="space-between" alignItems="center" sx={{mb: 1}}>
                    <Typography
                        gutterBottom variant="h6"
                        component="div"
                        sx={{
                            fontSize: "0.95rem"
                        }}
                    >
                        {product.name}
                    </Typography>
                    <Divider sx={{ width: '100%', my: 1 }} />
                </Box>
                <Box display="flex" justifyContent="space-between">
                    <Typography variant="body2" sx={{ color: 'text' }}>
                        Price:
                    </Typography>
                    <Typography variant="body1" sx={{ fontWeight: 500 }}>
                        {product.price.toFixed(2)} руб.
                    </Typography>
                </Box>

                <Box display="flex" justifyContent="space-between">
                    <Typography variant="body2" sx={{ color: 'text' }}>
                        Released:
                    </Typography>
                    <Typography variant="body1" >
                        {product.release_date.split("T")[0]}
                    </Typography>
                </Box>
            </CardContent>
        </Card>
        </Grid2>
    );
}

export default ProductItem;