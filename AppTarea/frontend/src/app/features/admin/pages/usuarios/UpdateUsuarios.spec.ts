
import { TestBed, ComponentFixture } from '@angular/core/testing';

import { UsuariosService } from '../../../../core/services/usuarios.service';
import { of, throwError } from 'rxjs';
import { mockUsuario, mockUsuarioUpdate } from '../../../../testing/mocks/usuario.mock';
import { UpdateUsuarios } from './UpdateUsuarios';

describe('UpdateUsuarios', () => {
  let component: UpdateUsuarios;
  let fixture: ComponentFixture<UpdateUsuarios>;
  let usuariosServiceMock: jasmine.SpyObj<UsuariosService>;

  beforeEach(async () => {
    usuariosServiceMock = jasmine.createSpyObj('UsuariosService', ['updateUsuario']);

    await TestBed.configureTestingModule({
      imports: [UpdateUsuarios],
      providers: [
        { provide: UsuariosService, useValue: usuariosServiceMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(UpdateUsuarios);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('cargarUsuarioParaEditar asigna correctamente el usuario', () => {
    component.cargarUsuarioParaEditar(mockUsuario);
    expect(component.usuarioActualizado.nombre).toBe(mockUsuario.nombre);
    expect(component.idUsuarioEditando).toBe(mockUsuario.id_usuario!);
  });

  it('updateUsuario llama al servicio y emite evento', () => {
    component.idUsuarioEditando = mockUsuario.id_usuario!;
    component.usuarioActualizado = mockUsuarioUpdate;
    spyOn(component.usuarioActualizadoEvent, 'emit');

    usuariosServiceMock.updateUsuario.and.returnValue(of(undefined));

    component.updateUsuario();

    expect(usuariosServiceMock.updateUsuario).toHaveBeenCalledWith(
      mockUsuario.id_usuario!,
      mockUsuarioUpdate
    );
    expect(component.usuarioActualizadoEvent.emit).toHaveBeenCalled();
  });

  it('updateUsuario maneja errores correctamente', () => {
    const errorResponse = { error: { mensaje: 'Error al actualizar' } };
    component.idUsuarioEditando = mockUsuario.id_usuario!;
    component.usuarioActualizado = mockUsuarioUpdate;

    usuariosServiceMock.updateUsuario.and.returnValue(throwError(() => errorResponse));

    component.updateUsuario();

    expect(component.errorMessage).toEqual(['Error al actualizar']);
  });
});
