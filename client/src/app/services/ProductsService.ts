import {GetProductsError, Product, ProductsFilter} from "../Interfaces/productsTypes.ts";
import axios, {AxiosError} from "axios";
import api, {ApiError, newAbortSignal} from "./api.ts";
import {AuthenticationError} from "../Interfaces/authTypes.ts";

function ConstructRequestUrl(request: ProductsFilter): string {
    let delimiter: string = '?';
    let requestUrl: string = "/goods/get-furniture";

    if (request.Name) {
        requestUrl = requestUrl + delimiter + `Name=${request.Name}`;
        delimiter = "&";
    }

    if (request.PriceMinRange) {
        requestUrl = requestUrl + delimiter + `PriceMinRange=${request.PriceMinRange}`;
        delimiter = "&";
    }

    if (request.PriceMaxRange) {
        requestUrl = requestUrl + delimiter + `PriceMaxRange=${request.PriceMaxRange}`;
        delimiter = "&";
    }

    if (request.ReleaseYearMinRange) {
        requestUrl = requestUrl + delimiter + `ReleaseDateMinRange=${request.ReleaseYearMinRange}-01-01`;
        delimiter = "&";
    }
    if (request.ReleaseYearMaxRange) {
        requestUrl = requestUrl + delimiter + `ReleaseDateMaxRange=${request.ReleaseYearMaxRange}-01-01`;
    }

    return requestUrl;
}

export const ProductsService =  {
    async getProducts(request: ProductsFilter): Promise<Product[]> {
        try {
            const requestUrl = ConstructRequestUrl(request);

            const response = await api.get<Product[]>(
                requestUrl,
                {
                    withCredentials: true,
                    signal: newAbortSignal(5000)
                }
            )

            return response.data.map(
                (p: Product) => {
                    return {
                        ...p,
                        release_date: p.release_date.split("T")[0]
                    }
                }
            );
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                const axError = error as AxiosError<ApiError>;

                if (axError.response) {
                    if (axError.response.status === 401) {
                        throw new AuthenticationError("User unauthenticated.");
                    } else if (axError.response.status === 400) {
                        throw new GetProductsError("Invalid products filter parameters.");
                    } else if (axError.response.status === 500) {
                        throw new GetProductsError("Internal server error. Working on this.");
                    }
                }
                if (axError.request) {
                    throw new GetProductsError("Couldn't establish connection to the server. Check your internet connection.")
                } else {
                    throw new GetProductsError("Unable to make a request:" + axError.message)
                }
            }

            throw new GetProductsError(error instanceof Error ? error.message : "Unexpected error occurred.");
        }
    }
}