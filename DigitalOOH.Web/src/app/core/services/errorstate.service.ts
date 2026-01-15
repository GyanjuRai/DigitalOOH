import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class ErrorStateHandlerService {
    private _hasError = false;
    private _server = false;

    get hasError() {
        return this._hasError;
    }

    trigger(server: boolean) {
        this._hasError = true;
        this._server = server;        
    }

    getServer() {
        return this._server;
    }

    reset() {
        this._hasError = false;
    }
}