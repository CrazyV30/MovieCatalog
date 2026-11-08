import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
@Component({
  imports: [FormsModule],
  selector: 'app-adding-movie-page',
  styleUrl: './adding-movie-page.css',
  templateUrl: './adding-movie-page.html',
})
export class AddingMoviePage {
  _httpUrl:string = "https://localhost:7098/api/Movie";
  movieData = {
    Title: '',
    Description: '',
    PhotoPath: '',
    Runtime: '',
    Director: '',
    ReleaseYear: 0,
    MoviePath:''
  };
  constructor(
    private http: HttpClient, 
    private router: Router,
    private toastr: ToastrService
  ) {}

  onAddMovie(form: any) {
    if (form.valid) {
      this.http.post(this._httpUrl, this.movieData).subscribe(
        (response) => {
          console.log('Movie added successfully:', response);
          this.toastr.success('Movie added successfully!', 'Success');
          this.router.navigate(['/']);
        },
        (error) => {
          console.error('Failed to add movie:', error);
          this.toastr.error('Failed to add movie!', 'Error');
        }
      );
    } else {
      console.log('Form is invalid');
    }
  }
  cancel() {
    this.router.navigate(['/']);
  }
}
