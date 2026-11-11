import { CommonModule } from '@angular/common';
import { Component, inject, signal, ɵallowSanitizationBypassAndThrow } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { MovieDisplay } from '../Models/MovieDisplay';
import { AuthService } from '../Services/auth-service';
import { Comment } from '../Models/Comment';

@Component({
  imports: [FormsModule, CommonModule],
  selector: 'app-one-movie-page',
  styleUrl: './one-movie-page.css',
  templateUrl: './one-movie-page.html',
})
export class OneMoviePage {
  authService = inject(AuthService);
  movie = signal<MovieDisplay|undefined>(undefined);
  route = inject(ActivatedRoute);
  httpUrl:string = "https://localhost:7098/api/Movie";

  comments = signal<Comment[]>([]);
  constructor(private http: HttpClient,private router: Router) {
    const movieId = this.route.snapshot.paramMap.get('id');
    if (movieId) {
      this.http.get<MovieDisplay>(this.httpUrl + '/' + movieId).subscribe((data) => {
        this.movie.set(data as MovieDisplay);
        this.comments.set(data.comments || []);
      });
    }
  }
  edit(id:number) {
    this.router.navigate(['/edit', id]);
  }
  toForm(){
    this.router.navigate(["/login"]);
  }
  //work with comments
  
  newCommentText = "";
  editText = "";
  httpUrlComment = 'https://localhost:7098/api/Comment';
  isMyComment(authorUsername: string): boolean {
    const currentUser = this.authService.currentUser();
    return !!currentUser && currentUser.username === authorUsername;
  }

  // Створення нового коментаря
  onAddComment(form: NgForm, id:number): void {
    const currentUser = this.authService.currentUser();
    if (!currentUser || form.invalid) return;

    const newComment: Comment = {
      movieId: id,
      authorName: currentUser.username,
      text: this.newCommentText
    };
    this.http.post(this.httpUrlComment, newComment).subscribe({
      error: (err)=> console.log(err)
    });
    this.comments()?.unshift(newComment);
    this.newCommentText = '';
    form.resetForm();
  }

  // Увімкнення режиму редагування
  startEdit(comment: Comment): void {
    // Закриваємо редагування на інших коментарях, якщо вони були відкриті
    this.comments()!.forEach(c => c.isEditing = false);
    
    comment.isEditing = true;
    this.editText = comment.text;
  }

  // Скасування редагування
  cancelEdit(comment: Comment): void {
    comment.isEditing = false;
    this.editText = '';
  }

  // Збереження відредагованого коментаря
  saveEdit(comment: Comment): void {
    if (!this.editText.trim()) return;

    
    this.http.put(this.httpUrlComment+"/"+comment.id, {Text:this.editText}).subscribe({
      next:()=>{
        comment.text = this.editText.trim();
        comment.isEditing = false;
        this.editText = '';
      },
      error:(err)=>console.log(err)      
    });
  }

  // Видалення коментаря
  onDeleteComment(commentToDelete: Comment): void {
    if (confirm('Ви дійсно бажаєте видалити свій коментар?')) {
      this.comments.set(this.comments()!.filter(c => c.id !== commentToDelete.id));
      
      this.http.delete(this.httpUrlComment+"/"+commentToDelete.id).subscribe({
        error: (err)=> console.log(err)
      });
    }
  }
}
