import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { FriendsService } from 'src/app/pages/friends/service/friends.service';

@Component({
  selector: 'app-lend-dialog',
  templateUrl: './lend-dialog.component.html',
  styleUrls: ['./lend-dialog.component.scss']
})
export class LendDialogComponent implements OnInit {

  public formControl: FormControl;
  public game: any;
  public friends$: Observable<any>

  constructor(
    private friendsService: FriendsService,
    @Inject(MAT_DIALOG_DATA) private data: any
  ) {}

  ngOnInit(): void {
    this.friends$ = this.friendsService.getAll();
    this.formControl = new FormControl(null, Validators.required);
    this.game = this.data;
  }

}
