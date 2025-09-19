// src/app/services/auth.service.spec.ts
import { AuthService } from './auth.service';
import { HttpTestingController } from '@angular/common/http/testing';

import {
  mockUser,
  mockRegisterResponse,
  mockLoginResponse,
  mockLogoutResponse
} from '../../testing/mocks/auth.mock';
import { environment } from '../../../environments/environment';
import { TestBed } from '@angular/core/testing';
import { setupTestingModule } from '../../testing/helpers/test-bed.helper';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;
  const apiUrl = `${environment.apiUrl}/usuarios`;

  beforeEach(() => {
    setupTestingModule([AuthService]);
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('debería registrar un usuario', () => {
    service.register(mockUser).subscribe(response => {
      expect(response).toEqual(mockRegisterResponse);
    });

    const req = httpMock.expectOne(`${apiUrl}/register`);
    expect(req.request.method).toBe('POST');
    req.flush(mockRegisterResponse);
  });

  it('debería hacer login', () => {
    service.login(mockUser).subscribe(response => {
      expect(response).toEqual(mockLoginResponse);
    });

    const req = httpMock.expectOne(`${apiUrl}/login`);
    expect(req.request.method).toBe('POST');
    expect(req.request.withCredentials).toBeTrue();
    req.flush(mockLoginResponse);
  });

  it('debería hacer logout', () => {
    service.logout().subscribe(response => {
      expect(response).toEqual(mockLogoutResponse);
    });

    const req = httpMock.expectOne(`${apiUrl}/logout`);
    expect(req.request.method).toBe('POST');
    req.flush(mockLogoutResponse);
  });
});
