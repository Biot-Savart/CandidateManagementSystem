import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { env } from 'process';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.prod';
import { IPosition } from '../../models/position';


@Injectable({
  providedIn: 'root'
})
export class PositionService {
  private apiUrl = `${environment.baseUrl}/Position`; // Adjust API URL

  constructor(private http: HttpClient) { }

  getPositions(): Observable<IPosition[]> {
    return this.http.get<IPosition[]>(this.apiUrl);
  }

  getPositionById(id: number): Observable<IPosition> {
    return this.http.get<IPosition>(`${this.apiUrl}/${id}`);
  }
}
