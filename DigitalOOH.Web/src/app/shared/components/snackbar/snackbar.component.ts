import { Component, Inject } from '@angular/core';
import { MAT_SNACK_BAR_DATA, MatSnackBarRef } from '@angular/material/snack-bar';

export interface SnackbarData {
  message: string;
  type: 'success' | 'error' | 'warning' | 'info';
}

@Component({
  selector: 'app-snackbar',
  templateUrl: './snackbar.component.html',
  styleUrl: './snackbar.component.css'
})
export class SnackbarComponent {
  message: string;
  type: 'success' | 'error' | 'warning' | 'info';

  iconMap = {
    success: 'check_circle',
    error: 'error',
    warning: 'warning',
    info: 'info'
  };

  constructor(
    public snackBarRef: MatSnackBarRef<SnackbarComponent>,
    @Inject(MAT_SNACK_BAR_DATA) public data: SnackbarData
  ) {
    this.message = data.message;
    this.type = data.type;
  }

  getIcon(): string {
    return this.iconMap[this.type];
  }

  onClose(): void {
    this.snackBarRef.dismiss();
  }
}
