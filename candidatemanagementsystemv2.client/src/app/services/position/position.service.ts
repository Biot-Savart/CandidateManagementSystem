import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { env } from 'process';
import { Observable } from 'rxjs';
import { IPosition } from '../../models/position';


@Injectable({
  providedIn: 'root'
})
export class PositionService {
  private apiUrl = 'https://localhost:7017/api/Position'; // Adjust API URL

  constructor(private http: HttpClient) { }

  getPositions(): Observable<IPosition[]> {
    return this.http.get<IPosition[]>(this.apiUrl);
  }

  getPositionById(id: number): Observable<IPosition> {
    return this.http.get<IPosition>(`${this.apiUrl}/${id}`);
  }
}
