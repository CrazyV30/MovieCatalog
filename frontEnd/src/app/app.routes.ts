import { Routes } from '@angular/router';
import { FormPage } from './form-page/form-page';
import { RegisterPage } from './register-page/register-page';
import { MoviePage } from './movie-page/movie-page';
import { AddingMoviePage } from './adding-movie-page/adding-movie-page';
import { OneMoviePage } from './one-movie-page/one-movie-page';
import { EditPage } from './edit-page/edit-page';

export const routes: Routes = [
    {path:'', component:MoviePage},
    {path:'login', component:FormPage},
    {path:'register', component:RegisterPage},
    {path:'addMovie', component:AddingMoviePage},
    {path:'movie/:id', component:OneMoviePage},
    {path: 'edit/:id', component:EditPage}
];
