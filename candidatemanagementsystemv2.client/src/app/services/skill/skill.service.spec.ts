import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { SkillService } from './skill.service';
import { ISkill, ISkillCandidatesCount } from '../../models/skill';

describe('SkillService', () => {
  let service: SkillService;
  let httpMock: HttpTestingController;
  const apiUrl = 'https://localhost:7017/api/skill';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [SkillService]
    });
    service = TestBed.inject(SkillService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Ensure there are no outstanding HTTP calls
  });

  it('getSkills should return an array of skills', () => {
    const dummySkills: ISkill[] = [
      { skillId: 1, name: 'JavaScript', yearsOfExperience: 1 },
      { skillId: 2, name: 'Angular', yearsOfExperience: 2 }
    ];

    service.getSkills().subscribe(skills => {
      expect(skills.length).toBe(2);
      expect(skills).toEqual(dummySkills);
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(dummySkills); // Simulate a successful response
  });

  it('getSkillById should return a single skill', () => {
    const expectedSkill: ISkill = { skillId: 1, name: 'JavaScript', yearsOfExperience: 1 };

    service.getSkillById(1).subscribe(skill => {
      expect(skill).toEqual(expectedSkill);
    });

    const req = httpMock.expectOne(`${apiUrl}/1`);
    expect(req.request.method).toBe('GET');
    req.flush(expectedSkill); // Simulate a successful response
  });

  it('getSkillCandidatesCount should return an array of skill candidates count', () => {
    const dummyCounts: ISkillCandidatesCount[] = [
      { name: 'JavaScript', candidateCount: 10 },
      { name: 'Angular', candidateCount: 8 }
    ];

    service.getSkillCandidatesCount().subscribe(counts => {
      expect(counts.length).toBe(2);
      expect(counts).toEqual(dummyCounts);
    });

    const req = httpMock.expectOne(`${apiUrl}/skill-candidates-count`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyCounts); // Simulate a successful response
  });

});
