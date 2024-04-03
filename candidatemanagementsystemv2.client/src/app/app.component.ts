import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';
import { Subscription } from 'rxjs';
import { AuthService } from './services/auth/auth.service';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  public forecasts: WeatherForecast[] = [];
  watcher: Subscription;
  activeMediaQuery = '';
  loggedIn = false;
  private authSubscription: Subscription | undefined;

  constructor(private http: HttpClient, private mediaObserver: MediaObserver, private authService: AuthService) {
    this.watcher = mediaObserver.asObservable().subscribe((changes) => {
      // Assuming you want to track if the screen is large
      this.activeMediaQuery = changes.filter(change => change.mqAlias === 'lg').length > 0 ? 'large' : 'small';
      // Do something with this information
    });
}

  ngOnInit() {
    this.authSubscription = this.authService.currentUserOb.subscribe(currentUser => {
      if (!currentUser || currentUser.length === 0) {
        this.loggedIn = false;
        return;
      }        

      if (currentUser && typeof (currentUser) === 'string')
        currentUser = JSON.parse(currentUser);

      this.loggedIn = currentUser && currentUser.authdata ? true : false;
    });
  }

  ngOnDestroy() {
    this.watcher.unsubscribe();
    this.authSubscription?.unsubscribe();
  }

  logout() {
    this.authService.logout();
  }

  title = 'Candidate Management System';
}
