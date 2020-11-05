import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EMPTY } from 'rxjs';
import { catchError, filter, map, switchMap } from 'rxjs/operators';
import { DialogMessageService } from 'src/app/helpers/services/dialog-message/dialog-message.service';
import { SnackBarService, SnackBarType } from 'src/app/helpers/services/snack-bar/snack-bar.service';
import { EditDialogComponent } from './dialogs/edit-dialog/edit-dialog.component';
import { LendDialogComponent } from './dialogs/lend-dialog/lend-dialog.component';
import { GamesService } from './service/games.service';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss']
})
export class GamesComponent implements OnInit {

  public games = [];

  constructor(
    private gamesService: GamesService, 
    private dialog: MatDialog,
    private snackBarservice: SnackBarService,
    private messageService: DialogMessageService
  ) { }

  ngOnInit(): void {
    this.gamesService.getAll().subscribe(result => this.games = result);
  }

  public openAddEditDialog(index?: number, game?: any): void {
    const dialogRef = this.dialog.open(EditDialogComponent, {
      width: '400px',
      data: game
    });

    dialogRef.afterClosed()
    .pipe(
      filter(result => !!result),
      switchMap(result => {
        const setGame = { name: result };

        if (game) {
          return this.gamesService.edit(game.id, Object.assign({}, game, setGame));
        }

        return this.gamesService.add(setGame);
      }),
      catchError(error => {
        console.log(error);

        this.snackBarservice.open('An error ocurred', SnackBarType.error);

        return EMPTY;
      })
    )
    .subscribe(result => {
      let message: string;

      if (game) {
        message = 'Game edited successfully';
        this.games[index] = result;
      } else {
        message = 'Game edited successfully';
        this.games.splice(this.games.length, 0, result);
      }
      
      this.snackBarservice.open(message, SnackBarType.success);
    });
  }

  public delete(index: number, id: number): void {
    this.messageService.create("Do you want to delete this item?")
    .pipe(
      filter(result => result),
      switchMap(() => this.gamesService.delete(id)),
      catchError(error => {
        console.log(error);

        this.snackBarservice.open('An error ocurred', SnackBarType.error);

        return EMPTY;
      })
    )
    .subscribe(() => {
      this.games.splice(index, 1);
      this.snackBarservice.open('Game deleted successfully', SnackBarType.success);
    });
  }

  public openLendDialog(game: any): void {
    const dialogRef = this.dialog.open(LendDialogComponent, {
      width: '400px',
      data: game
    });

    dialogRef.afterClosed()
    .pipe(
      filter(result => !!result),
      switchMap(friend => this.gamesService.lend(game.id, friend.id).pipe(map(() => friend))),
      catchError(error => {
        console.log(error);

        this.snackBarservice.open('An error ocurred', SnackBarType.error);

        return EMPTY;
      })
    )
    .subscribe(friend => {
      game.friend = friend;
      
      this.snackBarservice.open('Game lent successfully', SnackBarType.success);
    });
  }

  public takeBack(game: any): void {
    this.messageService.create(`Take ${game.name} back?`)
    .pipe(
      filter(result => result),
      switchMap(() => this.gamesService.tackBack(game.id))
    )
    .subscribe(() => {
      game.friend = null;

      this.snackBarservice.open('Game took back successfully', SnackBarType.success);
    })
  }

}
