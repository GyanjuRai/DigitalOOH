import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdsComponent } from "./ads/ads.component";
import { CampaignsComponent } from "./campaigns/campaigns.component";
import { ScreensComponent } from "./screens/screens.component";
import { GridModule } from "../shared/components/grid-config/grid-config.module";
import { DialogBoxModule } from "../shared/components/dialog-box/dialog-box.module";
import { ConfirmationDialogModule } from "../shared/components/confirmation-dialog/confirmation-dialog.module";
import { MatIconModule } from "@angular/material/icon";
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatDialogModule } from "@angular/material/dialog";
import { MatSelectModule } from "@angular/material/select";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatNativeDateModule } from "@angular/material/core";
import { ReactiveFormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";

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
        CommonModule,
        ReactiveFormsModule,
        RouterModule.forChild(featureRoutes),
        GridModule,
        DialogBoxModule,
        ConfirmationDialogModule,
        MatIconModule,
        MatTooltipModule,
        MatDialogModule,
        MatSelectModule,
        MatDatepickerModule,
        MatNativeDateModule,
    ],
    providers: [
        
    ]
})
export class FeatureModule {}