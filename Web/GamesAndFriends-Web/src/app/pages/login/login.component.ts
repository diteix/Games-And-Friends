import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/helpers/services/authentication/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public form: FormGroup;

  private returnUrl: string;

  constructor(
    formBuilder: FormBuilder, 
    private authenticationService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    if (this.authenticationService.currentUser) { 
      this.router.navigate(['/']);
    }

    this.form = formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  public onSubmit() {
    if (this.form.invalid) {
      return;
    }

    this.authenticationService.login(this.form.get('username').value, this.form.get('password').value)
    .subscribe(() => this.router.navigate([this.returnUrl]));
  }

}
