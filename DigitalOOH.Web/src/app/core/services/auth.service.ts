import { Injectable } from "@angular/core";
import { AppConst } from "../../app.const";
import { JwtHelperService } from '@auth0/angular-jwt';
import * as CryptoJS from "crypto-js";
import { loginResponse } from "../models/account.model";

@Injectable({
    providedIn: 'root'
})

export class AuthService {
    apiUrl: string;
    storageKey: string;
    secretKey: string;
    jwtHelper: JwtHelperService;

    constructor() {
        this.apiUrl = `${AppConst?.data?.apiBaseUrl}${AppConst?.data?.apiSegment}`;
        this.storageKey = AppConst?.data?.storageKey;
        this.secretKey = AppConst?.data?.secretKey;
        this.jwtHelper = new JwtHelperService();
    }

    setSession(session: loginResponse){
        const user = this.jwtHelper.decodeToken(session.token ?? '');
        this.setLocalStorage('id', user?.id);
        this.setLocalStorage('email', user?.email);
        this.setLocalStorage('token',  session.token);
    }

    setLocalStorage(key: string, value: any) {
        const data = localStorage.getItem(this.storageKey);

        if(data) {
            let dataDec = this.decrypt(data);
            let dataJson = JSON.parse(dataDec ?? '{}');
            dataJson = Object.assign({}, {[key]: value});
            localStorage.setItem(this.storageKey, this.encrypt(JSON.stringify(dataJson)));
        } else {
            let dataJson = Object.assign({}, {[key]: value});
            localStorage.setItem(this.storageKey, this.encrypt(JSON.stringify(dataJson)));
        }
    }

    getLocalStorage(key: string): any {
        const data = localStorage.getItem(this.storageKey);

        if(data) {
            let dataDec = this.decrypt(data);
            let dataJson = JSON.parse(dataDec ?? '{}');
            return dataJson[key] || null;
        } else {
            return null;
        }
    }

    encrypt(value: string): any {
        if(value) {
            return CryptoJS.AES.encrypt(value, this.secretKey);
        } else {
            return null;
        }
    }

    decrypt(value: string): any {
        if(value) {
            let bytes = CryptoJS.AES.decrypt(value, this.secretKey);
            let decryptedText = bytes.toString(CryptoJS.enc.Utf8);
            return decryptedText;
        } else {
            return null;
        }
    }

    isAuthenticated(): boolean {
        let token = (this.getLocalStorage('token') ?? '') as string;
        if(token) {
            return this.jwtHelper.isTokenExpired(token);
        } else {
            return false;
        }
    }

    clearAuth() {
        localStorage.clear()
    }
}