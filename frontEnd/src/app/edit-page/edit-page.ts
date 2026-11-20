import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { MovieUpdate } from '../Models/MovieUpdate';
import { MovieDisplay } from '../Models/MovieDisplay';

@Component({
  imports: [FormsModule, CommonModule],
  selector: 'app-edit-page',
  styleUrl: './edit-page.css',
  templateUrl: './edit-page.html',
})
export class EditPage {
  currentYear = new Date().getFullYear();
  serverError = signal<string | null>(null);
  movie = signal<MovieDisplay|undefined>(undefined);
  httpUrl:string = "https://localhost:7098/api/Movie";
  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) { }
  ngOnInit(){
    const movieId = this.route.snapshot.paramMap.get('id');
        if (movieId) {
          this.http.get<MovieDisplay>(this.httpUrl + '/' + movieId).subscribe((data) => {
            this.movie.set(data as MovieDisplay);
          });
        }
  }
  cancel(){
    this.router.navigate(['/movie', this.movie()?.id]);
  }
  onSubmit(form: NgForm) {
    if (form.invalid) {
      form.control.markAllAsTouched(); // Підсвітить усі незаповнені/невалідні поля
      this.serverError.set('Будь ласка, виправте помилки перед збереженням.');
      return;
    }

    this.serverError.set(null);

    // Ваш існуючий запит на оновлення:
    const updatedMovie: MovieUpdate = form.value;
    this.http.put(this.httpUrl + '/' + this.movie()?.id, updatedMovie).subscribe({
      next: () => {
        // успішне збереження
        this.router.navigate(['/movie', updatedMovie.Id]);
      },
      error: (err) => {
        // Якщо бекенд повернув 400 Bad Request від FluentValidation
        if (err.status === 400 && err.error?.errors) {
          const messages = Object.values(err.error.errors).flat().join(' ');
          this.serverError.set(`Помилка валідації: ${messages}`);
        } else {
          this.serverError.set('Не вдалося зберегти зміни. Спробуйте пізніше.');
        }
      }
    });
  }
  // onSubmit(form: any) {
  //   if (this.movie()) {
  //     const updatedMovie: MovieUpdate = form.value;
  //     this.http.put(this.httpUrl + '/' + this.movie()?.id, updatedMovie).subscribe({
  //       error: (err) => {
  //         console.error('Помилка при оновленні:', err);
  //       }
  //     });
      
  //     this.router.navigate(['/movie', updatedMovie.Id]);
  //   }
  // }
}
