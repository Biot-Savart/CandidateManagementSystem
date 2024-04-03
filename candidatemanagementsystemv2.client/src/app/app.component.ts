import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { Subscription } from 'rxjs';

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

  constructor(private http: HttpClient, private mediaObserver: MediaObserver) {
    this.watcher = mediaObserver.asObservable().subscribe((changes) => {
      // Assuming you want to track if the screen is large
      this.activeMediaQuery = changes.filter(change => change.mqAlias === 'lg').length > 0 ? 'large' : 'small';
      // Do something with this information
    });
}

  ngOnInit() {
  }

  ngOnDestroy() {
    this.watcher.unsubscribe();
  }

  title = 'candidatemanagementsystemv2.client';
}
