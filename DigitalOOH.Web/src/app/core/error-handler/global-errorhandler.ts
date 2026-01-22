import { ErrorHandler, Injectable, Injector, OnInit } from "@angular/core";
import { environment } from "../../../env/environment";
import { ErrorStateHandlerService } from "../services/errorstate.service";
import { HttpErrorResponse } from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class GlobalErrorHandler implements ErrorHandler {

    constructor(
        private errorState: ErrorStateHandlerService,
    ){}

    handleError(error: any): void {
        
        // sometimes error is wrapped in a 'rejection' object
        const chunk = error.rejection ? error.rejection : error; // unwrap zone.js error if necessary
        if(!environment.production) {
            console.error(chunk);
        }

        if(chunk instanceof HttpErrorResponse) {
            return;
        }

        this.errorState.trigger(true); // For everything(code crashes etc.) trigger the error state
    }
}