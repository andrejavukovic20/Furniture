import { Injectable } from '@angular/core';
import { BehaviorSubject, map, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {  Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl= environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  
  /*loadCurrentUser(token: string) {
    let headers = new HttpHeaders();
    headers = headers.set('Auth', `Bearer ${token}`);
  
    return this.http.get<User>(this.baseUrl + 'auth', { headers }).pipe(
      tap((user: User) => {
        localStorage.setItem('token', token);
        this.currentUserSource.next(user);
      })
    );
  }*/
  loadCurrentUser(token: string) {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
  
    // Store the token in local storage
    localStorage.setItem('token', token);
  
    // Load the user from local storage if available
    const user = localStorage.getItem('user');
    if (user) {
      this.currentUserSource.next(JSON.parse(user));
    }
  
    return this.http.get<User>(this.baseUrl + 'auth/CurrentUser', { headers }).pipe(
      tap((user: User) => {
        // Update the current user
        this.currentUserSource.next(user);
      })
    );
  }
  

  login(values: any) {
    return this.http.post<string>(this.baseUrl + 'auth/login', values, { responseType: 'text' as 'json' }).pipe(
      tap((token: string) => {
        localStorage.setItem('token', token);
        this.currentUserSource.next({ token });
      })
    );
  }
  logout() {
    // Clear the token and user data from local storage
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  
    // Clear the current user
    this.currentUserSource.next(null);
  }
  
 /* logout(){
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }*/

  checkUsernameExists(username: string){
    return this.http.get<boolean>(this.baseUrl + 'auth/usernameExists?username=' + username)
  }
}

