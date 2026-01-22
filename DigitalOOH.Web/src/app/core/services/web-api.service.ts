import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConst } from "../../app.const";
import { delay, Observable, retry } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export class WebApiService {

    private apiUrl: string;

    constructor(private htpp: HttpClient) {
        this.apiUrl = `${AppConst?.data?.apiBaseUrl}${AppConst?.data?.apiSegment}`;
    }

    get<T>(url: string, param?: object): Observable<T> {

        let params = {};
        params = param as HttpParams;

        return this.htpp.get<T>(`${this.apiUrl}${url}`, { params: params, withCredentials: true}).pipe(
            retry(0)
        );
    }

    post<T>(url: string, param?: object): Observable<T> {

        return this.htpp.post<T>(`${this.apiUrl}${url}`, param, { withCredentials: true } ).pipe(
            retry(0)
        );
    }

    put<T>(url: string, param?: object): Observable<T> {
        return this.htpp.put<T>(`${this.apiUrl}${url}`, param, { withCredentials: true }).pipe(
            retry(0)
        );
    }

    delete<T>(url: string, params?: object): Observable<T> {
        
        return this.htpp.delete<T>(`${this.apiUrl}${url}/${params}`, { withCredentials: true }).pipe(
            retry(0)
        );
    }
}