import { Injectable } from "@angular/core";
import { WebApiService } from "./web-api.service";
import { loginResponse, userLoginParam } from "../models/account.model";
import { Observable } from "rxjs";
import { responseModel } from "../models/base.model";

@Injectable({
    providedIn: 'root'
})

export class AccountService {

    constructor(private _webService: WebApiService) {}

    login(param: userLoginParam): Observable<responseModel<loginResponse>> {
        return this._webService.post('Account/Login', param);
    }
}