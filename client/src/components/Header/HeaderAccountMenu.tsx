import {IconButton, ListItemIcon, Menu, MenuItem, Tooltip} from "@mui/material";
import AccountCircleOutlinedIcon from "@mui/icons-material/AccountCircleOutlined";
import LogoutIcon from "@mui/icons-material/Logout";
import React, {useState} from "react";

function HeaderAccountMenu() {
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

    const open = Boolean(anchorEl);

    function handleOpenAccountMenu(e: React.MouseEvent<HTMLButtonElement>) {
        e.stopPropagation();
        setAnchorEl(e.currentTarget);
    }

    function handleCloseAccountMenu(e: React.MouseEvent<HTMLElement>) {
        e.stopPropagation();
        console.log("close")
        setAnchorEl(null);
    }

    function handleLogout() {
        console.log("logout")
    }

    return (
        <>
            <Tooltip title="Open menu" arrow={true}>
                <IconButton
                    id="account-button"
                    aria-controls={open ? 'account-menu' : undefined}
                    aria-haspopup="true"
                    aria-expanded={open ? 'true' : undefined}
                    color="inherit"
                    size="large"
                    sx={{p: 0}}
                    onClick={(e) => handleOpenAccountMenu(e)}>
                    <AccountCircleOutlinedIcon fontSize="inherit"/>
                </IconButton>
            </Tooltip>
            <Menu
                id="account-menu"
                anchorEl={anchorEl}
                open={open}
                onClose={handleCloseAccountMenu}
            >
                <MenuItem
                    onClick={(e) => {
                        handleCloseAccountMenu(e);
                        handleLogout();}}
                >
                    <ListItemIcon>
                        <LogoutIcon fontSize="small"/>
                    </ListItemIcon>
                    Sign out
                </MenuItem>
            </Menu>
        </>
    )
}

export default HeaderAccountMenu;