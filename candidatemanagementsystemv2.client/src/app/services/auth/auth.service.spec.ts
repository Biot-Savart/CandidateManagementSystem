// authService.spec.ts
import { TestBed } from '@angular/core/testing';
import { AuthService } from './auth.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;
  const apiUrl = 'https://localhost:7017'; // Adjust API URL

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [AuthService]
    });
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Verifies that no requests are outstanding.
    localStorage.removeItem('currentUser'); // Clean up local storage
  });

  it('should store user details in localStorage on successful login', () => {
    const mockUser = { username: 'test', authdata: window.btoa('test:test') };
    debugger;
    service.login('admin', 'password').subscribe(user => {
      expect(user).toEqual(mockUser);
      expect(localStorage.getItem('currentUser')).toEqual(JSON.stringify(mockUser));
    });

    const req = httpMock.expectOne(`${apiUrl}/api/Auth/login`);
    expect(req.request.method).toBe('POST');
    req.flush(mockUser); // Simulate a successful login response
  });

  it('should remove user from localStorage on logout', () => {
    // Pre-set a user in local storage to simulate a logged-in user
    localStorage.setItem('currentUser', JSON.stringify({ username: 'test' }));

    service.logout();

    expect(localStorage.getItem('currentUser')).toBeNull();
    expect(service.currentUserValue).toBeNull();
  });

  // Add more tests as needed
});
