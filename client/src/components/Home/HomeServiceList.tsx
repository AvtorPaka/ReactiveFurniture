import {Box, Container, Grid2} from "@mui/material";
import HomeServiceItem from "./HomeServiceItem.tsx";
import homeServiceItems from "./homeServiceItems.ts";

function HomeServiceList() {
    return (
        <Box sx={{
            backgroundColor: "inherit",
            py: "2%"
        }}>
            <Container maxWidth="md">
                <Grid2 container spacing={2}>
                    {homeServiceItems.map(
                        (item) => {

                            const IconComponent = item.icon;

                            return (
                                <HomeServiceItem
                                    key={item.id}
                                    serviceName={item.serviceName}
                                    serviceDescription={item.serviceDescription}>
                                    <IconComponent
                                        sx={{
                                            fontSize: 45,
                                            color: "inherit",
                                            mb: 1
                                        }}

                                    />
                                </HomeServiceItem>
                            );
                        }
                    )}
                </Grid2>
            </Container>
        </Box>
    );
}

export default HomeServiceList;