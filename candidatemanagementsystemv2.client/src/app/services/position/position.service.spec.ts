import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PositionService } from './position.service';
import { IPosition } from '../../models/position';

describe('PositionService', () => {
  let service: PositionService;
  let httpMock: HttpTestingController;
  const apiUrl = 'https://localhost:7017/api/Position';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PositionService]
    });
    service = TestBed.inject(PositionService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Ensure there are no outstanding HTTP calls
  });

  it('getPositions should return an array of positions', () => {
    const dummyPositions: IPosition[] = [
      { positionId: 1, title: 'Developer' },
      { positionId: 2, title: 'Project Manager' }
    ];

    service.getPositions().subscribe(positions => {
      expect(positions.length).toBe(2);
      expect(positions).toEqual(dummyPositions);
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(dummyPositions); // Simulate a successful response
  });

  it('getPositionById should return a single position', () => {
    const expectedPosition: IPosition = { positionId: 1, title: 'Developer' };

    service.getPositionById(1).subscribe(position => {
      expect(position).toEqual(expectedPosition);
    });

    const req = httpMock.expectOne(`${apiUrl}/1`);
    expect(req.request.method).toBe('GET');
    req.flush(expectedPosition); // Simulate a successful response
  });

});
