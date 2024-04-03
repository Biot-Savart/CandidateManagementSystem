import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CandidateService } from './candidate.service';
import { IPosition } from '../../models/position';
import { ICandidate } from '../../models/candidate';
import { environment } from '../../../environments/environment.prod';

describe('CandidateService', () => {
  let service: CandidateService;
  let httpMock: HttpTestingController;
  const apiUrl = `${environment.baseUrl}/candidate`;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CandidateService]
    });
    service = TestBed.inject(CandidateService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Make sure there are no outstanding HTTP calls
  });

  it('getCandidates should return a list of candidates', () => {
    const dummyCandidates: ICandidate[] = [
      { candidateId: 1, name: 'John Doe' },
      { candidateId: 2, name: 'Jane Doe' }
    ];

    service.getCandidates().subscribe(candidates => {
      expect(candidates.length).toBe(2);
      expect(candidates).toEqual(dummyCandidates);
    });

    const req = httpMock.expectOne(`${apiUrl}`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyCandidates); // Simulate a successful response
  });

  it('addCandidate should post and return the added candidate', () => {
    const newCandidate: ICandidate = { candidateId: 3, name: 'New Candidate' };

    service.addCandidate(newCandidate).subscribe(candidate => {
      expect(candidate).toEqual(newCandidate);
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newCandidate);
    req.flush(newCandidate);
  });
  it('updateCandidate should put and return the updated candidate', () => {
    const updatedCandidate: ICandidate = { candidateId: 1, name: 'Updated Name' };

    service.updateCandidate(updatedCandidate).subscribe(candidate => {
      expect(candidate).toEqual(updatedCandidate);
    });

    const req = httpMock.expectOne(`${apiUrl}/${updatedCandidate.candidateId}`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(updatedCandidate);
    req.flush(updatedCandidate);
  });

  it('deleteCandidate should delete and return any', () => {
    const candidateId = 1;

    service.deleteCandidate(candidateId).subscribe(response => {
      expect(response).toBeNull();
    });

    const req = httpMock.expectOne(`${apiUrl}/${candidateId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null); // Simulate a successful delete with no content response
  });
});
