import { ActivatedRouteSnapshot, RouterStateSnapshot, Routes } from "@angular/router";
import { AuthGuardService } from "./core/guards/auth-guard.service";
import { inject } from "@angular/core";
import { LoginComponent } from "./shared/login/login.component";
import { NonFoundComponent } from "./errors/non-found/non-found.component";

export const appRoutes: Routes = [
    {
        path: '',
        canActivate: [
            (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(AuthGuardService).canActivate(route, state)
        ],
        canActivateChild: [
            (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(AuthGuardService).canActivateChild(route, state)
        ],
        loadChildren: () => import('./features/features.module').then(m => m.FeatureModule)
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: '**',
        component: NonFoundComponent
    }
]