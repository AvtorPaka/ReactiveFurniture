
export interface AuthState {
    user: AppUser | null,
    isLoading: boolean
    isCheckLoading: boolean
    error: string | null
}

export interface AppUser {
    username: string,
    email: string
}

export interface LoginCredentials {
    email: string,
    password: string
}

export interface RegisterCredentials {
    username: string,
    email: string,
    password: string
    confirmPassword: string
}

export interface RegisterCredentialsViolations {
    username: string | null,
    email: string | null,
    password: string | null,
}

export interface LoginResponse {
    username: string
    email: string
}

export interface CheckAuthResponse {
    username: string,
    email: string
}

export class AuthenticationError extends Error {
    constructor(public message: string) {
        super(message);
        this.name = "AuthenticationError";
    }
}

export const apiUrl: string = import.meta.env.VITE_API_BASE_URL || "http://localhost:7179";

export const sessionKey = "rf-session-id";