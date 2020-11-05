import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EMPTY } from 'rxjs';
import { catchError, filter, switchMap, tap } from 'rxjs/operators';
import { DialogMessageService } from 'src/app/helpers/services/dialog-message/dialog-message.service';
import { SnackBarService, SnackBarType } from 'src/app/helpers/services/snack-bar/snack-bar.service';
import { EditDialogComponent } from './dialogs/edit-dialog/edit-dialog.component';
import { FriendsService } from './service/friends.service';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.scss']
})
export class FriendsComponent implements OnInit {

  public friends = [];
  public isExpasionDisabled = false;

  constructor(
    private friendsService: FriendsService,
    private dialog: MatDialog,
    private snackBarservice: SnackBarService,
    private messageService: DialogMessageService
  ) { }

  ngOnInit(): void {
    this.friendsService.getAll().subscribe(result => this.friends = result);
  }

  public openDialog(index?: number, friend?: any): void {
    const dialogRef = this.dialog.open(EditDialogComponent, {
      width: '400px',
      data: friend
    });

    dialogRef.afterClosed()
    .pipe(
      tap(() => this.isExpasionDisabled = false),
      filter(result => !!result),
      switchMap(result => {
        const setFriend = { name: result };

        if (friend) {
          return this.friendsService.edit(friend.id, Object.assign({}, friend, setFriend));
        }

        return this.friendsService.add(setFriend);
      }),
      catchError(error => {
        console.log(error);

        this.snackBarservice.open('An error ocurred', SnackBarType.error);

        return EMPTY;
      })
    )
    .subscribe(result => {
      let message: string;

      if (friend) {
        message = 'Friend edited successfully';
        this.friends[index] = result;
      } else {
        message = 'Friend edited successfully';
        this.friends.splice(this.friends.length, 0, result);
      }
      
      this.snackBarservice.open(message, SnackBarType.success);
    });
  }

  public delete(index: number, id: number): void {
    this.messageService.create("Do you want to delete this item?")
    .pipe(
      tap(() => this.isExpasionDisabled = false),
      filter(result => result),
      switchMap(() => this.friendsService.delete(id)),
      catchError(error => {
        console.log(error);

        this.snackBarservice.open('An error ocurred', SnackBarType.error);

        return EMPTY;
      })
    )
    .subscribe(() => {
      this.friends.splice(index, 1);
      this.snackBarservice.open('Friend deleted successfully', SnackBarType.success);
    });
  }

  public borrowedGames(friend: any): string {
    return friend.games?.map(s => s.name).join(', ');
  }

}
