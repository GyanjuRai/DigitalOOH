import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NonFoundComponent } from "./non-found/non-found.component";
import { SomethingWrongComponent } from "./something-wrong/something-wrong.component";

const errorRoutes: Routes = [
    {
        path: 'somethingwrong',
        component: SomethingWrongComponent
    },
    {
        path: 'notfound',
        component: NonFoundComponent
    }
]

@NgModule({
    declarations: [
        NonFoundComponent,
        SomethingWrongComponent
    ],
    imports: [
        RouterModule.forChild(errorRoutes),
    ]
})
export class ErrorModule {}