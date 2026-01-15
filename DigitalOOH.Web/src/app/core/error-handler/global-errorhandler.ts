import { ErrorHandler, Injectable, Injector, OnInit } from "@angular/core";
import { environment } from "../../../env/environment";
import { ErrorStateHandlerService } from "../services/errorstate.service";

@Injectable({
    providedIn: 'root'
})
export class GlobalErrorHandler implements ErrorHandler {

    constructor(private errorState: ErrorStateHandlerService){}

    handleError(error: any): void {

        if(!environment.production) {
            console.error(error);
        }

        this.errorState.trigger(false);    
    }
}