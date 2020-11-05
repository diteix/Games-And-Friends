import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GamesComponent } from './pages/games/games.component';
import { FriendsComponent } from './pages/friends/friends.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { LayoutComponent } from './pages/layout/layout.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './pages/login/login.component';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSelectModule } from '@angular/material/select';
import { JwtInterceptor } from './helpers/interceptors/jwt/jwt.interceptor';
import { EditDialogComponent as GameEditDialogComponent } from './pages/games/dialogs/edit-dialog/edit-dialog.component';
import { EditDialogComponent as FriendEditDialogComponent } from './pages/friends/dialogs/edit-dialog/edit-dialog.component';
import { DialogMessageComponent } from './helpers/services/dialog-message/component/dialog-message.component';
import { LendDialogComponent } from './pages/games/dialogs/lend-dialog/lend-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    GamesComponent,
    FriendsComponent,
    LayoutComponent,
    LoginComponent,
    GameEditDialogComponent,
    FriendEditDialogComponent,
    DialogMessageComponent,
    LendDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatListModule,
    MatToolbarModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    HttpClientModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatMenuModule,
    MatDialogModule,
    MatSnackBarModule,
    MatExpansionModule,
    MatSelectModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
