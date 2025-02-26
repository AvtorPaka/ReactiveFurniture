import {Box, Grid2, Typography} from "@mui/material";
import React from "react";

interface ServiceItemProps {
    children?: React.ReactNode
    xsProp?: number
    mdProp?: number
    serviceName: string
    serviceDescription: string
}

function HomeServiceItem({children, xsProp = 12, mdProp = 4, serviceName, serviceDescription}: ServiceItemProps) {
    return (
        <Grid2 size={{xs: xsProp, md: mdProp}}>
            <Box sx={{
                backgroundColor: "primary.main",
                textAlign: 'center',
                p: "4%",
                border: "2px solid",
                borderColor: "divider",
                borderRadius: 2,
                transition: 'all 0.3s ease',
                '&:hover': {
                    borderRadius: 4,
                    backgroundColor: 'primary.light',
                }
            }}>
                {children}
                <Typography variant="h6" gutterBottom sx={{ fontWeight: 600 }}>
                    {serviceName}
                </Typography>
                <Typography>
                    {serviceDescription}
                </Typography>
            </Box>
        </Grid2>
    );
}

export default HomeServiceItem;