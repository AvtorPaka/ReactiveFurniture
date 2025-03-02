import { createRoot } from 'react-dom/client'
import {CssBaseline} from "@mui/material";
import {ThemeProvider} from "@mui/material/styles";
import theme from "./theme.tsx"
import App from './App.tsx'
import {Provider} from "react-redux";
import {store} from "./app/store.ts";

const rootElement = document.getElementById('root')!;

createRoot(rootElement).render(
      <Provider store={store}>
          <ThemeProvider theme={theme}>
            <CssBaseline />
            <App/>
          </ThemeProvider>
      </Provider>
)
