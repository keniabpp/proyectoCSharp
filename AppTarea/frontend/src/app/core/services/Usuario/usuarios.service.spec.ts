import { TestBed } from '@angular/core/testing';
import { UsuariosService } from './usuarios.service';
import { HttpTestingController } from '@angular/common/http/testing';
import {
  mockUsuario,
  mockUsuarios,
  mockUsuarioUpdate,
} from '../../../testing/mocks/usuario.mock';
import { setupTestingModule } from '../../../testing/helpers/test-bed.helper';

describe('UsuariosService', () => {
  let service: UsuariosService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    setupTestingModule([UsuariosService]);
    service = TestBed.inject(UsuariosService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('debería obtener todos los usuarios', () => {
    service.getAllUsuarios().subscribe((usuarios) => {
      expect(usuarios).toEqual(mockUsuarios);
    });

    const req = httpMock.expectOne((r) => r.url.endsWith('/usuarios'));
    expect(req.request.method).toBe('GET');
    req.flush(mockUsuarios);
  });

  it('debería crear un usuario', () => {
    service.createUsuario(mockUsuario).subscribe((response) => {
      expect(response).toEqual(mockUsuario);
    });

    const req = httpMock.expectOne((r) => r.url.endsWith('/usuarios'));
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockUsuario);
    req.flush(mockUsuario);
  });

  it('debería eliminar un usuario por ID', () => {
    const id = mockUsuario.id_usuario!;
    service.deleteUsuarioById(id).subscribe((response) => {
      expect(response).toBeUndefined();
    });

    const req = httpMock.expectOne((r) => r.url.endsWith(`/usuarios/${id}`));
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });

  it('debería actualizar un usuario por ID', () => {
    const id = mockUsuario.id_usuario!;
    service.updateUsuario(id, mockUsuarioUpdate).subscribe((response) => {
      expect(response).toBeUndefined();
    });

    const req = httpMock.expectOne((r) => r.url.endsWith(`/usuarios/${id}`));
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(mockUsuarioUpdate);
    req.flush(null);
  });

  it('debería obtener un usuario por ID', () => {
    const id = mockUsuario.id_usuario!;
    service.getUsuarioById(id).subscribe((usuario) => {
      expect(usuario).toEqual(mockUsuario);
    });

    const req = httpMock.expectOne((r) => r.url.endsWith(`/usuarios/${id}`));
    expect(req.request.method).toBe('GET');
    req.flush(mockUsuario);
  });
});
