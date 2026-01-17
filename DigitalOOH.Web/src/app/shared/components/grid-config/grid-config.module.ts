import { NgModule } from "@angular/core";
import { GridComponent } from "./grid-config.component";
import { CommonModule } from "@angular/common";

@NgModule({
    declarations: [
        GridComponent
    ],
    imports: [
        CommonModule
    ],
    exports: [
        GridComponent
    ]
})
export class GridModule {}
