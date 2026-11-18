import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../Services/auth-service';

@Component({
  imports: [FormsModule, RouterLink, RouterLinkActive],
  selector: 'app-header',
  styleUrl: './header.css',
  templateUrl: './header.html',
})
export class Header {
  authService = inject(AuthService);
  router = inject(Router);
  selectedCategory:string='';
  searchQuery:string = '';

  getPlaceholder(): string {
    const placeholders: Record<string, string> = {
      title: 'Введіть назву фільму...',
      genre: 'Наприклад: Бойовик, Драма, Фантастика...',
      year: 'Введіть рік випуску (напр. 2024)...',
      country: 'Введіть країну (напр. Україна, США)...',
      keyword: 'Введіть ключове слово або тег...'
    };
    return placeholders[this.selectedCategory] || 'Пошук фільмів...';
  }

  onSearch(): void {
    const trimmedQuery = this.searchQuery.trim();
    
    this.router.navigate(['/'], {
      queryParams: {
        category: trimmedQuery ? this.selectedCategory : null,
        q: trimmedQuery || null
      },
      queryParamsHandling: 'merge'
    });
  }
}
