import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { DialogMessageComponent } from './component/dialog-message.component';

@Injectable({
  providedIn: 'root'
})
export class DialogMessageService {

  constructor(private dialog: MatDialog) { }

  public create(message: string): Observable<boolean> {
    const dialogRef = this.dialog.open(DialogMessageComponent, {
      width: '400px',
      data: message
    });

    return dialogRef.afterClosed();
  }
}
