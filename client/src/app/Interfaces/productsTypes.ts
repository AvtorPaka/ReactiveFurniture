
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
    PriceMinRange?: number,
    PriceMaxRange?: number,
    ReleaseYearMinRange?: number,
    ReleaseYearMaxRange?: number
}

export interface ProductsFilterViolations {
    priceViolation: string | null,
    dateViolation: string | null
}


export class GetProductsError extends Error {
    constructor(public message: string) {
        super(message);
        this.name = "GetProductsError";
    }
}