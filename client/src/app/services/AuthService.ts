import {
    AuthenticationError,
    CheckAuthResponse,
    LoginCredentials,
    LoginResponse,
    RegisterCredentials, sessionKey
} from "../Interfaces/authTypes.ts";
import api, {ApiError, newAbortSignal} from "./api.ts";
import axios, {AxiosError} from "axios";


function validateLoginRequest(request: LoginCredentials) {
    if (!request.email.trim()) {
        throw new AuthenticationError("Email is required");
    } else if (!request.password.trim()) {
        throw new AuthenticationError("Password is required")
    }
}

function validateRegisterRequest(request: RegisterCredentials) {
    if (!request.username.trim()) {
        throw new AuthenticationError("Username is required");
    } else if (request.username.length < 8) {
        throw new AuthenticationError("Username must contain at least 8 characters");
    } else if (request.username.length > 50) {
        throw new AuthenticationError("Username must contain at most 50 characters")
    }

    if (!request.email.trim()) {
        throw new AuthenticationError("Email address is required");
    } else if (request.email.length > 100) {
        throw new AuthenticationError("Email address must contain at most 100 characters");
    }

    if (!request.password.trim()) {
        throw new AuthenticationError("Password is required");
    } else if (request.password.length < 8) {
        throw new AuthenticationError("Password must contain at least 8 characters");
    } else if (request.password.length > 50) {
        throw new AuthenticationError("Password must contain at most 50 characters");
    } else if (request.password !== request.confirmPassword) {
        throw new AuthenticationError("Password doesn't match Confirm password");
    }
}

function deleteCookie(name: string) {
    const date = new Date();

    date.setTime(date.getTime() + (-1 * 24 * 60 * 60 * 1000));

    document.cookie = name+"=; expires="+date.toUTCString()+"; path=/";
}

function ProcessError(error: unknown, endpointCustomHandler: (errorResponse: AxiosError<ApiError>) => void ) {
    if (axios.isAxiosError(error)) {
        const axError = error as AxiosError<ApiError>;

        endpointCustomHandler(error);
        if (axError.request) {
            throw new AuthenticationError("Couldn't establish connection to the server. Check your internet connection.")
        } else {
            throw new AuthenticationError("Unable to make a request:" + axError.message)
        }
    }

    throw new AuthenticationError(error instanceof Error ? error.message : "Unexpected error occurred.");
}

export const AuthService = {

    async login(request: LoginCredentials): Promise<LoginResponse> {
        try
        {
            validateLoginRequest(request);

            const response = await api.post<LoginResponse>(
                "/auth/login",
                request,
                {
                    withCredentials: true,
                    signal: newAbortSignal(5000)
                }
            )

            return response.data;
        }
        catch (error) {
            if (error instanceof AuthenticationError) {
                throw error;
            }

            const loginErrorHandler = (errorResponse: AxiosError<ApiError>) => {
                if (errorResponse.response) {
                    if (errorResponse.response.status === 404) {
                        throw new AuthenticationError("User doesnt exist.");
                    } else if (errorResponse.response.status === 400) {
                        throw new AuthenticationError("Incorrect password provided.");
                    } else if (errorResponse.response.status === 500) {
                        throw new AuthenticationError("Internal server error. Working on this.");
                    }
                }
            }

            throw ProcessError(error, loginErrorHandler);
        }
    },

    async register(request: RegisterCredentials) {
        try
        {
            validateRegisterRequest(request);

            const response = await api.post(
                "/auth/register",
                {
                    username: request.username.trim(),
                    email: request.email.trim(),
                    password: request.password.trim()
                },
                {
                    withCredentials: true,
                    signal: newAbortSignal(5000)
                }
            )

            return response.data;
        }
        catch (error)
        {
            if (error instanceof AuthenticationError) {
                throw error;
            }

            const registerErrorHandler = (errorResponse: AxiosError<ApiError>) => {
                if (errorResponse.response) {
                    if (errorResponse.response.status === 409) {
                        throw new AuthenticationError("User with such email already exist.");
                    } else if (errorResponse.response.status === 400) {
                        throw new AuthenticationError("Invalid credentials provided.");
                    } else if (errorResponse.response.status === 500) {
                        throw new AuthenticationError("Internal server error. Working on this.");
                    }
                }
            }

            throw ProcessError(error, registerErrorHandler);
        }
    },

    async checkAuth(): Promise<CheckAuthResponse> {
        try {
            const response = await api.get<CheckAuthResponse>(
                "/auth/check-auth",
                {
                    withCredentials: true,
                    signal: newAbortSignal(5000)
                }
            )

            return response.data;
        } catch (error)
        {
            const checkAuthErrorHandler = (errorResponse: AxiosError<ApiError>) => {
                if (errorResponse.response) {
                    if (errorResponse.response.status === 404) {
                        throw new AuthenticationError("User doesnt exist.");
                    } else if (errorResponse.response.status === 401) {
                        throw new AuthenticationError("User unauthenticated.");
                    } else if (errorResponse.response.status === 500) {
                        throw new AuthenticationError("Internal server error. Working on this.");
                    }
                }
            }

            throw ProcessError(error, checkAuthErrorHandler);
        }
    },

    async logout() {
        try {
            const response = await api.post(
                "/auth/logout",
                {},
                {
                    withCredentials: true,
                    signal: newAbortSignal(5000)
                }
            );

            return response.data;
        } catch (error) {
            deleteCookie(sessionKey);
            const logoutErrorHandler = (errorResponse: AxiosError<ApiError>) => {
                if (errorResponse.response) {
                    if (errorResponse.response.status === 401) {
                        throw new AuthenticationError("User already unauthenticated.");
                    } else if (errorResponse.response.status === 500) {
                        throw new AuthenticationError("Internal server error. Working on this.");
                    }
                }
            }

            throw ProcessError(error, logoutErrorHandler);
        }
    }
};


