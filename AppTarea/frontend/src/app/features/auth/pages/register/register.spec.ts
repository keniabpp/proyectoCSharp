import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Register } from './register';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';
import { Login } from '../login/login';
import { of, throwError } from 'rxjs';
import { mockRegisterResponse, mockRegisterUser } from '../../../../testing/mocks/auth.mock';

describe('Register', () => {
  let component: Register;
  let fixture: ComponentFixture<Register>;
  let routerMock: jasmine.SpyObj<Router>;
  let authServiceMock: jasmine.SpyObj<AuthService>;


  beforeEach(async () => {
    // Mock de AuthService
    authServiceMock = jasmine.createSpyObj('AuthService', ['register']);
    // Mock de Router
    routerMock = jasmine.createSpyObj('Router', ['navigate']);
    await TestBed.configureTestingModule({
      imports: [
        Register,
        Login,
        CommonModule,
        FormsModule,
        HttpClientModule
      ],

      providers: [
        { provide: AuthService, useValue: authServiceMock },
        { provide: Router, useValue: routerMock }
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(Register);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('registrar y redirigir al login', () => {
    component.usuario = mockRegisterUser;
    authServiceMock.register.and.returnValue(of(mockRegisterResponse));

    component.register();

    expect(authServiceMock.register).toHaveBeenCalledWith(mockRegisterUser);
    expect(routerMock.navigate).toHaveBeenCalledWith(['/login']);
  });

  it('mensaje error', () => {
    const mockError = {
      error: {
        errores: [{ mensaje: 'No se pudo registrar al usuario' }]
      }
    };

    authServiceMock.register.and.returnValue(throwError(mockError));
    component.register();
    expect(component.errorMessage).toEqual(['No se pudo registrar al usuario']);
  });
});
