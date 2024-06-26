import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { error } from 'console';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<any>;
  public currentUser: Observable<any>;

  constructor(private http: HttpClient, private router: Router) {
    this.currentUserSubject = new BehaviorSubject<any>(localStorage.getItem('currentUser') ?? '');
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue() {
    return this.currentUserSubject.value;
  }

  public get currentUserOb(): Observable<any> {
    return this.currentUserSubject.asObservable();
  }

  login(username: string, password: string) {
    const headers = new HttpHeaders({ 'Authorization': 'Basic ' + btoa(username + ':' + password) });
    return this.http.post<any>(`${environment.baseUrl}/Auth/login`, {}, { headers: headers })
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
          // Handle the error here
          console.error('Login error:', error);

          // Optionally transform error before rethrowing or return a new observable
          // e.g., return throwError(() => new Error('Custom error message'));
          return throwError(() => error); // Rethrow the received error or emit a new one
        })
      );
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);

    // Navigate to login or home page
    this.router.navigate(['/login']);
  }
}
