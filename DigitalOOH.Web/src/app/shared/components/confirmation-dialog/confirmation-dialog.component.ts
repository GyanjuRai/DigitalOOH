import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

export interface ConfirmationData {
  title?: string;
  message: string;
  confirmLabel?: string;
  cancelLabel?: string;
  confirmColor?: 'primary' | 'warn' | 'danger';
}

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrl: './confirmation-dialog.component.css'
})
export class ConfirmationDialogComponent {
  title: string;
  message: string;
  confirmLabel: string;
  cancelLabel: string;
  confirmColor: string;

  constructor(
    public dialogRef: MatDialogRef<ConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmationData
  ) {
    this.title = data.title || 'Confirm Action';
    this.message = data.message;
    this.confirmLabel = data.confirmLabel || 'Confirm';
    this.cancelLabel = data.cancelLabel || 'Cancel';
    this.confirmColor = data.confirmColor === 'danger' ? 'danger' : 'warn';
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }
}
