import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  error: string | undefined;

  constructor(private authService: AuthService, private router: Router) { }

  onSubmit() {
    this.authService.login(this.username, this.password).subscribe({
      next: () => {
        // Navigate to the home page (or some other page) after successful login
        this.router.navigate(['/candidates']);
      },
      error: (error: any) => {
        this.error = 'Invalid username or password';
        // Handle login error, show a message to the user
        console.error('Login error:', error);
      }
    });
  }
}
