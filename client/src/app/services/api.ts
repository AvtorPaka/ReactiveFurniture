import axios from "axios";
import {apiUrl} from "../Interfaces/authTypes.ts";

const api = axios.create({
    baseURL: apiUrl,
    headers: {
        "Content-Type": "application/json",
    },
    withCredentials: true
})

export interface ApiError {
    status_code: number,
    exceptions: string[]
}


export default api;