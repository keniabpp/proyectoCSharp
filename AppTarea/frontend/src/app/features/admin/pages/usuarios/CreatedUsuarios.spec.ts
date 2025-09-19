
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsuariosService } from '../../../../core/services/usuarios.service';
import { Usuario } from '../../../../core/models/usuario.model';
import { of, throwError } from 'rxjs';
import { CreateUsuarios } from './CreateUsuarios';

describe('CreateUsuarios', () => {
  let component: CreateUsuarios;
  let fixture: ComponentFixture<CreateUsuarios>;
  let usuariosServiceMock: jasmine.SpyObj<UsuariosService>;

  beforeEach(async () => {
    usuariosServiceMock = jasmine.createSpyObj('UsuariosService', ['createUsuario']);

    await TestBed.configureTestingModule({
      imports: [CreateUsuarios],
      providers: [
        { provide: UsuariosService, useValue: usuariosServiceMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(CreateUsuarios);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('debería agregar un usuario y emitir evento', () => {
    const nuevoUsuario: Usuario = {
      id_usuario: 1,
      nombre: 'Ana',
      apellido: 'Pérez',
      telefono: '1234567890',
      email: 'ana@example.com',
      contrasena: '123456',
      id_rol: 1
    };

    // Mock del servicio
    usuariosServiceMock.createUsuario.and.returnValue(of(nuevoUsuario));

    // Espía para el EventEmitter
    spyOn(component.usuarioCreadoEvent, 'emit');

    component.nuevoUsuario = { ...nuevoUsuario }; // asignamos al input
    component.addUsuario();

    expect(usuariosServiceMock.createUsuario).toHaveBeenCalledWith(nuevoUsuario);
    expect(component.usuarios).toContain(nuevoUsuario);
    expect(component.usuarioCreadoEvent.emit).toHaveBeenCalledWith(nuevoUsuario);

    // Después de agregar, nuevoUsuario se reinicia
    expect(component.nuevoUsuario.nombre).toBe('');
  });

  it('debería capturar error del servicio', () => {
    const mockError = {
      error: {
        errores: [{ mensaje: 'No se pudo crear usuario' }]
      }
    };

    usuariosServiceMock.createUsuario.and.returnValue(throwError(() => mockError));

    component.addUsuario();

    expect(component.errorMessage).toEqual(['No se pudo crear usuario']);
  });
});
