import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ConfirmationDialogComponent } from './confirmation-dialog.component';

@NgModule({
  declarations: [
    ConfirmationDialogComponent
  ],
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule
  ],
  exports: [
    ConfirmationDialogComponent
  ]
})
export class ConfirmationDialogModule { }
