import { ActivatedRouteSnapshot, RouterStateSnapshot, Routes } from "@angular/router";
import { AuthGuardService } from "./core/guards/auth-guard.service";
import { inject } from "@angular/core";
import { LoginComponent } from "./shared/login/login.component";

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
        path: 'error',
        loadChildren: () => import('./errors/errors.module').then(m => m.ErrorModule)
    },
    {
        path: '**',
        redirectTo: 'error/notfound'
    }
]