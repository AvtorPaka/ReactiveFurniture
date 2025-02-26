import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import {CssBaseline} from "@mui/material";
import {ThemeProvider} from "@mui/material/styles";
import theme from "./theme.tsx"
import App from './App.tsx'

const rootElement = document.getElementById('root')!;

createRoot(rootElement).render(
  <StrictMode>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <App/>
      </ThemeProvider>
  </StrictMode>,
)
