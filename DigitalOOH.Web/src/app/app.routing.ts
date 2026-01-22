import { ActivatedRouteSnapshot, RouterStateSnapshot, Routes } from "@angular/router";
import { AuthGuardService } from "./core/guards/auth-guard.service";
import { inject } from "@angular/core";
import { LoginComponent } from "./shared/login/login.component";
import { NonFoundComponent } from "./errors/non-found/non-found.component";
import { MainLayoutComponent } from "./shared/layouts/main-layout.component";

export const appRoutes: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        canActivate: [
            () => inject(AuthGuardService).canActivate()
        ],
        canActivateChild: [
            () => inject(AuthGuardService).canActivateChild()
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