import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Movie } from '../Models/Movie';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { MovieUpdate } from '../Models/MovieUpdate';

@Component({
  imports: [FormsModule, CommonModule],
  selector: 'app-edit-page',
  styleUrl: './edit-page.css',
  templateUrl: './edit-page.html',
})
export class EditPage {
  movie = signal<MovieUpdate|undefined>(undefined);
  httpUrl:string = "https://localhost:7098/api/Movie";
  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) {
    const movieId = this.route.snapshot.paramMap.get('id');
    if (movieId) {
      this.http.get<MovieUpdate>(this.httpUrl + '/' + movieId).subscribe((data) => {
        this.movie.set(data as MovieUpdate);
      });
    }
  }
  cancel(){
    this.router.navigate(['/movie', this.movie()!.Id]);
  }
  onSubmit(form: any) {
    if (this.movie()) {
      const updatedMovie: MovieUpdate = {
        Id: this.movie()!.Id,
        Title: form.value.title,
        Description: form.value.description,
        PhotoPath: form.value.photoPath,
        Runtime: form.value.runtime,
        Director: form.value.director
      };
      this.http.put(this.httpUrl + '/' + updatedMovie.Id, updatedMovie).subscribe(() => {
        this.router.navigate(['/movie', updatedMovie.Id]);
      });
    }
  }
}
