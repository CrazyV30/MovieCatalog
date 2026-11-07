import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  imports: [FormsModule],
  selector: 'app-register-page',
  styleUrl: './register-page.css',
  templateUrl: './register-page.html',
})
export class RegisterPage {
  _httpUrl:string = "https://localhost:7098/api/User";
  constructor(
    private http: HttpClient, 
    private router: Router,
    private toastr: ToastrService
  ) {}
  userData = {
    Email: '',
    Password: '',
    ConfirmPassword: '',
    Username: '',
    Surname: ''
  }
  onRegister(form: any) {
    if (form.valid) {
      console.log('Registering user:', form.value);
      this.http.post(this._httpUrl+'/register', form.value ).subscribe(
        (response) => {
          console.log('Registration successful:', response);
          this.toastr.success('Registration successful!', 'Success');
          this.router.navigate(['/']);
        },
        (error) => {
          console.error('Registration failed:', error);
          this.toastr.error('Registration failed!', 'Error');
        }
      );
    } else {
      console.log('Form is invalid');
    }
  }
  toLogin() {
    this.router.navigate(['/']);
  }
}
