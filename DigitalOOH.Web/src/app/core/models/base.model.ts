export interface responseModel<T> {
    message: string;
    type: string;
    data: T;
}