import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, Observable, throwError } from "rxjs";
import { ErrorStateHandlerService } from "../services/errorstate.service";
import { SnackbarService } from "../services/snackbar.service";

@Injectable({
    providedIn: 'root'
})
export class HttpErrorInterceptor implements HttpInterceptor {
    constructor(
        private router: Router,
        private errorState: ErrorStateHandlerService,
        private snackbarService: SnackbarService
    ) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError((error: HttpErrorResponse) => {

                // Unathorized
                if(error.status === 401) {
                    this.snackbarService.error('Session expired. Please log in again.');
                    this.router.navigate(['/login']);
                }
                else if(error.status >= 400 && error.status < 500) {
                    this.snackbarService.warning(error.error?.message);
                }

                // Internal Server Error
                if(error.status >= 500) {
                    this.errorState.trigger(true);
                }

                return throwError(() => error);
            })
        );
    }
}