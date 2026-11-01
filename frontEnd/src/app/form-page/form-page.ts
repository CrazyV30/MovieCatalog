import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLogin } from '../Models/UserLogin';
import { ToastrService } from 'ngx-toastr';
@Component({
  imports: [FormsModule],
  selector: 'app-form-page',
  styleUrl: './form-page.css',
  templateUrl: './form-page.html',
})
export class FormPage {
  _httpUrl:string = "https://localhost:7098/api/User";
  constructor(
    private http: HttpClient, 
    private router: Router,
    private toastr: ToastrService
  ) {}
  userLogin:UserLogin = { Email: '', Password: '' };
  toRegister() {
    this.router.navigate(['/register']);
  }

  onLogin(form: any) {
    if (form.valid) {
      this.http.post(this._httpUrl+'/login', form.value).subscribe(
        (response) => {
          console.log('Login successful:', response);
          this.toastr.success('Login successful!', 'Success');
          this.router.navigate(['/catalog']);
        },
        (error) => {
          console.error('Login failed:', error);
          this.toastr.error('Login failed!', 'Error');
        }
      );
    } else {
      console.log('Form is invalid');
    }
  }
}
