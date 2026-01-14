import { ErrorHandler, Injectable, Injector } from "@angular/core";
import { Router } from "@angular/router";
import { environment } from "../../../env/environment";

@Injectable({
    providedIn: 'root'
})
export class GlobalErrorHandler implements ErrorHandler {

    constructor(private injector: Injector){}

    handleError(error: any): void {
        const router = this.injector.get(Router);

        if(!environment.production) {
            console.error(error);
        }

        router.navigate(['/error/somethingwrong']);
    }
}