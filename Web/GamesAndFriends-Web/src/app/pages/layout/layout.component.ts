import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/helpers/services/authentication/authentication.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  public menuList = [];

  constructor(private authenticationService: AuthenticationService) { }

  ngOnInit(): void {
    this.menuList.push(
      {
        path: 'games',
        label: 'Games'
      },
      {
        path: 'friends',
        label: 'Friends'
      }
    );
  }

  public logout(): void {
    this.authenticationService.logout();
  }

}
