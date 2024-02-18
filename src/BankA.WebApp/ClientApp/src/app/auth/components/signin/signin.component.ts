import { Component, Injector, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-signin-component',
  templateUrl: './signin.component.html'
})
export class SignInComponent extends BaseComponent implements OnInit {
  loginForm!: FormGroup;
  authFailed: boolean = false;
  signedIn: boolean = false;

  constructor(
    private injector: Injector,
    private authService: AuthService) {

    super(injector);
    this.authService.isSignedIn().forEach(
      isSignedIn => {
        this.signedIn = isSignedIn;
        if (this.signedIn)
          this.router.navigate(['overview']);
      });
  }

  ngOnInit(): void {
    this.authFailed = false;
    this.loginForm = this.formBuilder.group(
      {
        Username: ['demo', [Validators.required]],
        Password: ['Demo1234!', [Validators.required]]
      });
  }

  public async signIn() {
    try {

      if (this.loginForm.valid) {
        const userName = this.loginForm.get('Username')?.value;
        const password = this.loginForm.get('Password')?.value;
        await this.authService.signIn(userName, password);
        this.router.navigateByUrl('overview');
      }
      else {
        this.loginForm.markAllAsTouched();
        this.errorMessage = 'Populate required fields';
      }
    } catch (error: any) {
      this.errorMessage = error;
    } finally {
      this.loading = false;
    }
  }
}
