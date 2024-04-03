import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { env } from 'process';
import { Observable } from 'rxjs';
import { ICandidate } from '../../models/candidate'
import { IPosition } from '../../models/position';


@Injectable({
  providedIn: 'root'
})
export class CandidateService {
  private apiUrl = 'https://localhost:7017/api/candidate'; // Adjust API URL

  constructor(private http: HttpClient) { }

  getCandidates(): Observable<ICandidate[]> {
    return this.http.get<ICandidate[]>(this.apiUrl);
  }

  addCandidate(candidate: ICandidate): Observable<ICandidate> {
    return this.http.post<ICandidate>(this.apiUrl, candidate);
  }

  updateCandidate(candidate: ICandidate): Observable<ICandidate> {
    return this.http.put<ICandidate>(`${this.apiUrl}/${candidate.candidateId}`, candidate);
  }

  deleteCandidate(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getCandidateById(id: number): Observable<ICandidate> {
    return this.http.get<ICandidate>(`${this.apiUrl}/${id}`);
  }

  getPositions(): Observable<IPosition[]> {
    return this.http.get<IPosition[]>(`https://localhost:7017/api/Position`);
  }
}
