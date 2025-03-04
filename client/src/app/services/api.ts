import axios from "axios";

const apiUrl: string = import.meta.env.VITE_API_BASE_URL || "http://localhost:7179";

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

export function newAbortSignal(timeoutMs: number) {
    const abortController = new AbortController();
    setTimeout(() => abortController.abort(), timeoutMs || 0);

    return abortController.signal;
}

export default api;