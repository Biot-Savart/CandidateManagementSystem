import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { env } from 'process';
import { Observable } from 'rxjs';
import { ISkill, ISkillCandidatesCount } from '../../models/skill';


@Injectable({
  providedIn: 'root'
})
export class SkillService {
  private apiUrl = 'https://localhost:7017/api/skill'; // Adjust API URL

  constructor(private http: HttpClient) { }

  getSkills(): Observable<ISkill[]> {
    return this.http.get<ISkill[]>(this.apiUrl);
  }

  getSkillById(id: number): Observable<ISkill> {
    return this.http.get<ISkill>(`${this.apiUrl}/${id}`);
  }

  getSkillCandidatesCount(): Observable<ISkillCandidatesCount[]> {
    return this.http.get<any[]>(`${this.apiUrl}/skill-candidates-count`);
  }
}
