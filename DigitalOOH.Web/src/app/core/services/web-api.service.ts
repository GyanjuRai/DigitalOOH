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

    get(url: string, param?: object): Observable<any> {

        let params = {};
        params = param as HttpParams;

        return this.htpp.get(`${this.apiUrl}${url}`, { params: params, withCredentials: true}).pipe(
            delay(100),
            retry(0)
        )
    }

    post(url: string, param?: object): Observable<any>{

        return this.htpp.post(`${this.apiUrl}${url}`, param as HttpParams).pipe(
            delay(100),
            retry(0)
        )
    }
}