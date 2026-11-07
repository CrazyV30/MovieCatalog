import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { MovieDisplay } from '../Models/MovieDisplay';

@Component({
  imports: [FormsModule, CommonModule],
  selector: 'app-one-movie-page',
  styleUrl: './one-movie-page.css',
  templateUrl: './one-movie-page.html',
})
export class OneMoviePage {
  movie = signal<MovieDisplay[]>([]);
  route = inject(ActivatedRoute);
  httpUrl:string = "https://localhost:7098/api/Movie";
  constructor(private http: HttpClient,private router: Router) {
    const movieId = this.route.snapshot.paramMap.get('id');
    if (movieId) {
      this.http.get<MovieDisplay[]>(this.httpUrl + '/' + movieId).subscribe((data) => {
        this.movie.set(data as MovieDisplay[]);
      });
    }
  }
  goBack() {
    this.router.navigate(['/catalog']);
  }
  edit(id:number) {
    this.router.navigate(['/edit', id]);
  }
}
