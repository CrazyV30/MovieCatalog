import { Component, signal } from '@angular/core';
import { Movie } from '../Models/Movie';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MovieShow } from '../Models/MovieShow';

@Component({
  imports: [],
  selector: 'app-movie-page',
  styleUrl: './movie-page.css',
  templateUrl: './movie-page.html',
})
export class MoviePage {
  movies = signal<MovieShow[]>([]);
  httpUrl:string = "https://localhost:7098/api/Movie";
  constructor(private http: HttpClient,private router: Router) {
    this.http.get<MovieShow[]>(this.httpUrl).subscribe((data) => {
      this.movies.set(data as MovieShow[]);
    });
  }
  onAddMovie() {
    this.router.navigate(['/addMovie']);
  }
  removeMovie(title:string){
    this.http.delete(this.httpUrl + '/' + title).subscribe(() => {
      this.movies.set(this.movies().filter(movie => movie.title !== title));
    });
  }
  logout() {
    this.router.navigate(['/']);
  }  
}
