import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { error } from 'console';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<any>;
  public currentUser: Observable<any>;
  private apiUrl = 'https://localhost:7017'; // Adjust API URL

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<any>(localStorage.getItem('currentUser') ?? '');
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue() {
    return this.currentUserSubject.value;
  }

  login(username: string, password: string) {
    const headers = new HttpHeaders({ 'Authorization': 'Basic ' + btoa(username + ':' + password) });
    return this.http.post<any>(`${this.apiUrl}/api/Auth/login`, {}, { headers: headers })
      .pipe(
        map(user => {
        // store user details and basic auth credentials in local storage to keep user logged in between page refreshes
        user.authdata = window.btoa(username + ':' + password);
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      },
        ),
        catchError(error => {
          debugger;
          // Handle the error here
          console.error('Login error:', error);

          // Optionally transform error before rethrowing or return a new observable
          // e.g., return throwError(() => new Error('Custom error message'));
          return throwError(() => error); // Rethrow the received error or emit a new one
        })
      );
  }

  logout() {
    debugger;
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
}
