
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Usuarios } from './usuarios';
import { UsuariosService } from '../../../../core/services/Usuario/usuarios.service';
import { of, throwError } from 'rxjs';
import {
  mockUsuario,
  mockUsuarios,
  mockUsuarioUpdate,
} from '../../../../testing/mocks/usuario.mock';
import Swal from 'sweetalert2';

jest.mock('sweetalert2', () => ({
  fire: jest.fn().mockResolvedValue({ isConfirmed: true }),
}));

describe('Usuarios Component', () => {
  let component: Usuarios;
  let fixture: ComponentFixture<Usuarios>;
  let usuariosService: jest.Mocked<UsuariosService>;

  beforeEach(() => {
    const usuariosServiceMock = {
      getAllUsuarios: jest.fn(),
      createUsuario: jest.fn(),
      updateUsuario: jest.fn(),
      deleteUsuarioById: jest.fn(),
    };

    TestBed.configureTestingModule({
      imports: [Usuarios],
      providers: [
        { provide: UsuariosService, useValue: usuariosServiceMock },
      ],
    });

    fixture = TestBed.createComponent(Usuarios);
    component = fixture.componentInstance;
    usuariosService = TestBed.inject(UsuariosService) as jest.Mocked<UsuariosService>;
  });

  it('debería cargar usuarios al iniciar', () => {
    usuariosService.getAllUsuarios.mockReturnValue(of(mockUsuarios));
    component.ngOnInit();
    expect(component.usuarios).toEqual(mockUsuarios);
  });

  it('debería agregar un nuevo usuario ', () => {
    const usuarioVacio = {
      nombre: '',
      apellido: '',
      telefono: '',
      email: '',
      contrasena: '',
      id_rol: 0,
    };

    component.nuevoUsuario = { ...usuarioVacio };
    usuariosService.createUsuario.mockReturnValue(of(mockUsuario));
    usuariosService.getAllUsuarios.mockReturnValue(of(mockUsuarios));

    component.addUsuario();

    expect(usuariosService.createUsuario).toHaveBeenCalledWith(usuarioVacio);
    expect(component.nuevoUsuario).toEqual(usuarioVacio);
  });

  it('debería manejar errores al agregar usuario', () => {
    const errorResponse = {
      error: { errores: [{ mensaje: 'Email inválido' }] },
    };
    usuariosService.createUsuario.mockReturnValue(throwError(() => errorResponse));

    component.nuevoUsuario = {
      nombre: '',
      apellido: '',
      telefono: '',
      email: '',
      contrasena: '',
      id_rol: 0,
    };
    component.addUsuario();

    expect(component.errorMessage).toEqual(['Email inválido']);
  });

  

  it('debería actualizar usuario ', () => {
    usuariosService.updateUsuario.mockReturnValue(of(undefined));
    usuariosService.getAllUsuarios.mockReturnValue(of(mockUsuarios));

    component.idUsuarioEditando = mockUsuario.id_usuario!;
    component.usuarioActualizado = mockUsuarioUpdate;
    component.updateUsuario();

    expect(usuariosService.updateUsuario).toHaveBeenCalledWith(
      mockUsuario.id_usuario,
      mockUsuarioUpdate
    );
  });

  it('debería eliminar usuario', async () => {
    usuariosService.deleteUsuarioById.mockReturnValue(of(undefined));
    usuariosService.getAllUsuarios.mockReturnValue(of(mockUsuarios));

    await component.deleteUsuario(mockUsuario.id_usuario!);

    expect(Swal.fire).toHaveBeenCalled();
    expect(usuariosService.deleteUsuarioById).toHaveBeenCalledWith(mockUsuario.id_usuario);
  });
});
