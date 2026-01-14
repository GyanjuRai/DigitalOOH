import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdsComponent } from "./ads/ads.component";
import { CampaignsComponent } from "./campaigns/campaigns.component";
import { LoginComponent } from "../shared/login/login.component";
import { ScreensComponent } from "./screens/screens.component";

const featureRoutes: Routes = [
    {
        path: '',
        redirectTo: 'screens',
        pathMatch: 'full'
    },
    {
        path: 'screens',
        component: ScreensComponent
    },
    {
        path: 'ads',
        component: AdsComponent
    },
    {
        path: 'campaigns',
        component: CampaignsComponent
    },
]

@NgModule({
    declarations:[
        AdsComponent,
        CampaignsComponent,
        ScreensComponent
    ],
    imports: [
        RouterModule.forChild(featureRoutes),
    ],
    providers: [

    ]
})
export class FeatureModule {}