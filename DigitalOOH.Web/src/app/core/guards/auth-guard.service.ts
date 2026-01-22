import { Injectable } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from "@angular/router";

@Injectable({
    providedIn: 'root'
})
export class AuthGuardService {
    constructor(
        private auth: AuthService,
        private router: Router
    ) {}

    canActivate(): boolean {
        return this.canAccess();
    }

    canActivateChild(): boolean {
        return this.canActivate();
    }

    private canAccess(): boolean {
        if(!this.auth.isAuthenticated()){
            this.router.navigate(['/login']);
            // TODO: Snackbar implementation
            return false;
        }
        return true
    }
}