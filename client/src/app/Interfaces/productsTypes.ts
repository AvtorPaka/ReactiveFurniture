
export interface Product {
    id: number,
    price: number,
    name: string,
    release_date: string
}

export interface ProductsState {
    products: Product[],
    isLoading: boolean,
    error: string | null
}

export interface ProductsFilter {
    Name: string | null,
    PriceMinRange: number | null,
    PriceMaxRange: number | null,
    ReleaseYearMinRange: number | null,
    ReleaseYearMaxRange: number | null
}


export class GetProductsError extends Error {
    constructor(public message: string) {
        super(message);
        this.name = "GetProductsError";
    }
}