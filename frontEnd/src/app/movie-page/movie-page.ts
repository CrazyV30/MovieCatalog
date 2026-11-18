import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MovieDisplay } from '../Models/MovieDisplay';
import { AuthService } from '../Services/auth-service';
import { finalize } from 'rxjs';

@Component({
  imports: [],
  selector: 'app-movie-page',
  styleUrl: './movie-page.css',
  templateUrl: './movie-page.html',
})
export class MoviePage {
  authService = inject(AuthService);
  movies = signal<MovieDisplay[]>([]);
  httpUrl:string = "https://localhost:7098/api/Movie";
  activeQuery = signal<string | null>(null);
  isLoading = signal<boolean>(true);
  constructor( private http: HttpClient, private router: Router, private route: ActivatedRoute ) {
    this.route.queryParams.subscribe((params) => {
      const category = params['category'];
      const query = params['q'];
      this.activeQuery.set(query || null);

      this.loadMovies(category, query);
    });
  }
  loadMovies(category?: string, query?: string) {
    this.isLoading.set(true);

    if (!query) {
      this.http
        .get<MovieDisplay[]>(this.httpUrl)
        .pipe(finalize(() => this.isLoading.set(false)))
        .subscribe({
          next: (data) => this.movies.set(data),
          error: (err) => console.error('Помилка завантаження фільмів:', err),
        });
      return;
    }

    let httpParams = new HttpParams();

    switch (category) {
      case 'title':
        httpParams = httpParams.set('Title', query);
        break;
      case 'genre':
        httpParams = httpParams.set('Genre', query);
        break;
      case 'year':
        if (!isNaN(Number(query))) {
          httpParams = httpParams.set('Year', Number(query));
        }
        break;
      case 'country':
        httpParams = httpParams.set('Country', query);
        break;
      case 'keyword':
        httpParams = httpParams.set('KeyWord', query); 
        break;
      default:
        httpParams = httpParams.set('Title', query);
        break;
    }

    this.http
      .get<MovieDisplay[]>(`${this.httpUrl}/search`, { params: httpParams })
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: (data) => this.movies.set(data),
        error: (err) => {
          console.error('Помилка пошуку:', err);
          this.movies.set([]);
        },
      });
  }
  clearSearch() {
    this.router.navigate(['/']);
  }
  removeMovie(id:number){
    this.http.delete(this.httpUrl + '/' + id).subscribe(() => {
      this.movies.set(this.movies().filter(movie => movie.id !== id));
    });
  }
  viewMovieDetails(id:number) {
    this.router.navigate(['/movie', id]);
  }
}
