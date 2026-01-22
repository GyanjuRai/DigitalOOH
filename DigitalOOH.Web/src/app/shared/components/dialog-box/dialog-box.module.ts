import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { DialogBoxComponent } from './dialog-box.component';
import { MatSelect, MatOption, MatSelectModule } from "@angular/material/select";
import { MatOptionModule } from '@angular/material/core';
import { A11yModule, CdkTrapFocus } from '@angular/cdk/a11y';

@NgModule({
  declarations: [
    DialogBoxComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatSelectModule,
    MatOptionModule,
    CdkTrapFocus
],
  exports: [
    DialogBoxComponent
  ]
})
export class DialogBoxModule { }
