import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { SnackbarComponent, SnackbarData } from '../../shared/components/snackbar/snackbar.component';

export type SnackbarType = 'success' | 'error' | 'warning' | 'info';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
  private defaultDuration = 3000;

  constructor(private snackBar: MatSnackBar) {}

  show(message: string, type: SnackbarType = 'info', duration?: number): void {
    const config: MatSnackBarConfig = {
      duration: duration !== undefined ? duration : this.defaultDuration,
      horizontalPosition: 'end',
      verticalPosition: 'top',
      panelClass: [`snackbar-${type}`],
      data: {
        message: message,
        type: type
      } as SnackbarData
    };

    this.snackBar.openFromComponent(SnackbarComponent, config);
  }

  success(message: string, duration?: number): void {
    this.show(message, 'success', duration);
  }

  error(message: string, duration?: number): void {
    this.show(message, 'error', duration);
  }

  warning(message: string, duration?: number): void {
    this.show(message, 'warning', duration);
  }

  info(message: string, duration?: number): void {
    this.show(message, 'info', duration);
  }
}
