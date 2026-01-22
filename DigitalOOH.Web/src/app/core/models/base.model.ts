export interface responseModel<T> {
    message: string;
    type: string;
    data: T;
}

export interface gridResponse<T> {
    data: T[];
    totalRows: number;
}