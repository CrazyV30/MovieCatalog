import { Injectable, signal } from '@angular/core';
import { User } from '../Models/User';
import { HttpClient, HttpParams } from '@angular/common/http';
import { UserLogin } from '../Models/UserLogin';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
    currentUser = signal<User | null>(null);
    httpUrl:string = "https://localhost:7098/api/User";
    constructor(private http:HttpClient) {
        const savedUser = localStorage.getItem('user');
        if (savedUser) {
        this.currentUser.set(JSON.parse(savedUser));
        }
    }

    login(user: string): void {
        this.http.get<User>(this.httpUrl+"/" + user).subscribe((currUser)=>{
            this.currentUser.set(currUser as User);
        })
    }

    logout(): void {
        this.currentUser.set(null);
    }
}
