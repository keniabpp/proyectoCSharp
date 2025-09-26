
import { TestBed } from '@angular/core/testing';
import { AuthService } from './auth.service';
import { HttpTestingController } from '@angular/common/http/testing';

import {
  mockUser,
  mockLoginResponse,
  mockRegisterUser,
  mockRegisterResponse,
  mockLogoutResponse,
} from '../../../testing/mocks/auth.mock';
import { setupTestingModule } from '../../../testing/helpers/test-bed.helper';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    setupTestingModule([AuthService]);
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Verifica que no haya peticiones pendientes
  });

  it('debería registrar un usuario correctamente', () => {
    service.register(mockRegisterUser).subscribe((response) => {
      expect(response).toEqual(mockRegisterResponse);
    });

    const req = httpMock.expectOne((r) => r.url.includes('/register'));
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockRegisterUser);
    req.flush(mockRegisterResponse);
  });

  it('debería iniciar sesión con credenciales válidas', () => {
    service.login(mockUser).subscribe((response) => {
      expect(response).toEqual(mockLoginResponse);
    });

    const req = httpMock.expectOne((r) => r.url.includes('/login'));
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockUser);
    expect(req.request.withCredentials).toBe(true);
    req.flush(mockLoginResponse);
  });

  it('debería cerrar sesión correctamente', () => {
    service.logout().subscribe((response) => {
      expect(response).toEqual(mockLogoutResponse);
    });

    const req = httpMock.expectOne((r) => r.url.includes('/logout'));
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({});
    expect(req.request.withCredentials).toBe(true);
    req.flush(mockLogoutResponse);
  });
});
