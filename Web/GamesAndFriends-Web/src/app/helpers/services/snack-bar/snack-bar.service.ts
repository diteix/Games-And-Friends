import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

export enum SnackBarType {
  success = 'snack-success',
  error = 'snack-error'
}

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {

  constructor(private snackBar: MatSnackBar) { }

  public open(message: string, type: SnackBarType): void {
    this.snackBar.open(message, null, { duration: 2000, panelClass: [type] });
  }
}
