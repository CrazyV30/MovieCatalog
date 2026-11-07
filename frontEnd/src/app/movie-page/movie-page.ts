import { Component, signal } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MovieDisplay } from '../Models/MovieDisplay';

@Component({
  imports: [],
  selector: 'app-movie-page',
  styleUrl: './movie-page.css',
  templateUrl: './movie-page.html',
})
export class MoviePage {
  movies = signal<MovieDisplay[]>([]);
  httpUrl:string = "https://localhost:7098/api/Movie";
  constructor(private http: HttpClient,private router: Router) {
    this.http.get<MovieDisplay[]>(this.httpUrl).subscribe((data) => {
      this.movies.set(data as MovieDisplay[]);
    });
  }
  onAddMovie() {
    this.router.navigate(['/addMovie']);
  }
  removeMovie(id:number){
    this.http.delete(this.httpUrl + '/' + id).subscribe(() => {
      this.movies.set(this.movies().filter(movie => movie.id !== id));
    });
  }
  logout() {
    this.router.navigate(['/']);
  }  
  viewMovieDetails(id:number) {
    this.router.navigate(['/movie', id]);
  }
}
