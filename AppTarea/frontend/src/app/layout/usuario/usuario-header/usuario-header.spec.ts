import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { UsuarioHeader } from './usuario-header';

describe('UsuarioHeader', () => {
  let component: UsuarioHeader;
  let fixture: ComponentFixture<UsuarioHeader>;
  let routerMock: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    routerMock = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      imports: [UsuarioHeader],
      providers: [
        { provide: Router, useValue: routerMock },
        { provide: ActivatedRoute, useValue: {} } // ✅ Mock vacío para evitar el error
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(UsuarioHeader);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('logout debería eliminar token y navegar a /login', () => {
    spyOn(localStorage, 'removeItem');

    component.logout();

    expect(localStorage.removeItem).toHaveBeenCalledWith('token');
    expect(routerMock.navigate).toHaveBeenCalledWith(['/login']);
  });
});
