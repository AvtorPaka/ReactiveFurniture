import {BrowserRouter as Router, Routes, Route, Navigate} from "react-router-dom";
import {Container, Box} from '@mui/material'
import Header from "./components/Header/Header.tsx"
import HomePage from "./components/Home/HomePage.tsx"
import Login from "./components/Auth/Login.tsx"
import Register from "./components/Auth/Register.tsx";
import ProductsPage from "./components/Products/ProductsPage.tsx";
import Footer from "./components/Footer/Footer.tsx";
import ProtectedRoute from "./components/Auth/ProtectedRoute.tsx";


export default function App() {
    return (
    <Router>
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'column',
                minHeight: '100vh',
            }}
        >
        <Header/>
        <Container
            maxWidth={false}
            component="main"
            sx={{
                flex: 1
            }}
            disableGutters>
              <Routes>
                  <Route path="/" element={<HomePage/>}/>
                  <Route path="/sign-in" element={<Login/>}/>
                  <Route path="/register" element={<Register/>}/>
                  <Route path="/products"
                         element={
                      <ProtectedRoute>
                          <ProductsPage/>
                      </ProtectedRoute>
                  }
                  />

                  <Route path="*" element={
                      <Navigate
                          to="/"
                          replace
                          state={{
                              from: '404',
                              message: 'Page not found'
                          }}
                      />
                  } />
              </Routes>
        </Container>
        <Footer/>
        </Box>
    </Router>
  );
}