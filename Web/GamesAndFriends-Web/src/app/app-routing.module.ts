import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticationGuard } from './helpers/guards/authentication/authentication.guard';
import { FriendsComponent } from './pages/friends/friends.component';
import { GamesComponent } from './pages/games/games.component';
import { LayoutComponent } from './pages/layout/layout.component';
import { LoginComponent } from './pages/login/login.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthenticationGuard],
    children: [
      {
        path: '',
        redirectTo: 'games',
        pathMatch: 'full'
      },
      {
        path: 'games',
        component: GamesComponent
      },
      {
        path: 'friends',
        component: FriendsComponent
      }
    ]
  },
  {
    path: 'login',
    component: LoginComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
