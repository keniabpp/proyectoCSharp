import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Login } from './login';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AuthService } from '../../../../core/services/auth.service';
import { Router } from '@angular/router';
import { of } from 'rxjs/internal/observable/of';
import { throwError } from 'rxjs/internal/observable/throwError';
import { mockLoginResponse, mockUser } from '../../../../testing/mocks/auth.mock';

describe('Login', () => {
  let component: Login;
  let fixture: ComponentFixture<Login>;
  let authServiceMock: jasmine.SpyObj<AuthService>;
  let routerMock: jasmine.SpyObj<Router>;
  


  beforeEach(async () => {
    // Mock de AuthService
    authServiceMock = jasmine.createSpyObj('AuthService', ['login']);
    // Mock de Router
    routerMock = jasmine.createSpyObj('Router', ['navigate']);
    
    await TestBed.configureTestingModule({
      imports: [
        Login,
        CommonModule,
        FormsModule,
        

      ],
      
      providers: [
        { provide: AuthService, useValue: authServiceMock },
        { provide: Router, useValue: routerMock }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Login);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  

  it('llamar al inicio de sesion', () => {
    component.credentials = mockUser;
    authServiceMock.login.and.returnValue(of(mockLoginResponse));

    component.login();

    expect(authServiceMock.login).toHaveBeenCalledWith(mockUser);
    expect(routerMock.navigate).toHaveBeenCalledWith(['/admin']);
  });

  it('mensaje error', () => {
    const mockError = {
      error: {
        errores: [{ mensaje: 'Credenciales inválidas' }]
      }
    };

    authServiceMock.login.and.returnValue(throwError(mockError));
    component.login();
    expect(component.errorMessage).toEqual(['Credenciales inválidas']);
  });
});
