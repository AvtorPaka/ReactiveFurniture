import {Container} from "@mui/material";
import HomeMainNav from "./HomeMainNav.tsx";
import HomeServiceList from "./HomeServiceList.tsx";


function HomePage() {
    return (
            <Container
                maxWidth={false}
                sx={{
                    flex: 1,
                    display: 'flex',
                    flexDirection: 'column',
                    justifyContent: 'center',
                    alignItems: 'center',
                    minHeight: {
                        xs: 'calc(100vh - 112px)',
                        md: 'calc(100vh - 128px)'
                    },
                    py: {
                        xs: 4,
                        sm: 6,
                        md: 8
                    },
                    gap: 3
                }}
                disableGutters
            >
                <HomeMainNav/>
                <HomeServiceList/>
            </Container>
    );
}

export default HomePage;