import {TextField, TextFieldProps} from "@mui/material";

function AuthTextField(props: TextFieldProps) {
    return (
        <TextField
            margin="normal"
            fullWidth
            autoFocus
            {...props}
        />
    );
}

export default AuthTextField;