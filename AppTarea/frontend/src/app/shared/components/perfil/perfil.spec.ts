import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Perfil } from './perfil';
import { setupTestingModule } from '../../../testing/helpers/test-bed.helper';
import { UsuariosService } from '../../../core/services/usuarios.service';
import { of, throwError } from 'rxjs';
import { mockUsuario, mockUsuarioUpdate } from '../../../testing/mocks/usuario.mock';
import Swal from 'sweetalert2';

describe('Perfil', () => {
  let component: Perfil;
  let fixture: ComponentFixture<Perfil>;
  let usuariosServiceMock: jasmine.SpyObj<UsuariosService>;

  beforeEach(async () => {
    usuariosServiceMock = jasmine.createSpyObj('UsuariosService', ['getUsuarioById', 'updateUsuario']);

    setupTestingModule([
      { provide: UsuariosService, useValue: usuariosServiceMock }
    ]);

    await TestBed.configureTestingModule({
      imports: [Perfil]
    }).compileComponents();

    fixture = TestBed.createComponent(Perfil);
    component = fixture.componentInstance;

    // Simular SweetAlert
    spyOn(Swal, 'fire').and.returnValue(Promise.resolve({ isConfirmed: true } as any));
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('cargar el perfil si hay ID en localStorage', () => {
    spyOn(localStorage, 'getItem').and.returnValue(String(mockUsuario.id_usuario));
    usuariosServiceMock.getUsuarioById.and.returnValue(of(mockUsuario));

    component.ngOnInit();

    expect(usuariosServiceMock.getUsuarioById).toHaveBeenCalledWith(mockUsuario.id_usuario!);
    expect(component.idUsuarioEditando).toBe(mockUsuario.id_usuario!);
    expect(component.usuarioActualizado.nombre).toBe(mockUsuario.nombre);
  });

  it('ngOnInit debería manejar error si falla la carga', () => {
    spyOn(localStorage, 'getItem').and.returnValue(String(mockUsuario.id_usuario));
    const consoleSpy = spyOn(console, 'error');
    usuariosServiceMock.getUsuarioById.and.returnValue(throwError(() => ({ error: 'fallo' })));

    component.ngOnInit();

    expect(consoleSpy).toHaveBeenCalled();
    expect(component.errorMessage).toEqual(['No se pudo cargar tu perfil']);
  });

  it('updateUsuario debería llamar al servicio y mostrar Swal', () => {
    component.idUsuarioEditando = mockUsuario.id_usuario!;
    component.usuarioActualizado = mockUsuarioUpdate;
    usuariosServiceMock.updateUsuario.and.returnValue(of(undefined));

    component.updateUsuario();

    expect(usuariosServiceMock.updateUsuario).toHaveBeenCalledWith(mockUsuario.id_usuario!, mockUsuarioUpdate);
    expect(Swal.fire).toHaveBeenCalled();
    expect(component.errorMessage.length).toBe(0);
  });

  

  it('updateUsuario debería manejar error con mensaje único', () => {
    const errorResponse = {
      error: {
        mensaje: 'Error al actualizar'
      }
    };

    component.idUsuarioEditando = mockUsuario.id_usuario!;
    component.usuarioActualizado = mockUsuarioUpdate;
    usuariosServiceMock.updateUsuario.and.returnValue(throwError(() => errorResponse));

    component.updateUsuario();

    expect(component.errorMessage).toEqual(['Error al actualizar']);
  });

  
});
