
// src/app/services/usuarios.service.spec.ts
import { HttpTestingController } from "@angular/common/http/testing";
import { TestBed } from "@angular/core/testing";
import { UsuariosService } from "./usuarios.service";
import { environment } from "../../../environments/environment";
import { setupTestingModule } from "../../testing/helpers/test-bed.helper";
import { mockUsuario, mockUsuarios, mockUsuarioUpdate } from '../../testing/mocks/usuario.mock';

describe('UsuariosService', () => {
  let service: UsuariosService;
  let httpMock: HttpTestingController;
  const apiUrl = `${environment.apiUrl}/usuarios`;

  beforeEach(() => {
    setupTestingModule([UsuariosService]);
    service = TestBed.inject(UsuariosService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('debería obtener todos los usuarios', () => {
    service.getAllUsuarios().subscribe(usuarios => {
      expect(usuarios).toEqual(mockUsuarios); // array completo
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockUsuarios);
  });

  it('debería crear un usuario', () => {
    service.createUsuario(mockUsuario).subscribe(usuario => {
      expect(usuario).toEqual(mockUsuario);
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('POST');
    req.flush(mockUsuario);
  });

  it('debería actualizar un usuario', () => {
    service.updateUsuario(mockUsuario.id_usuario!, mockUsuarioUpdate).subscribe(res => {
      expect(res).toBeNull(); // PUT devuelve void
    });

    const req = httpMock.expectOne(`${apiUrl}/${mockUsuario.id_usuario}`);
    expect(req.request.method).toBe('PUT');
    req.flush(null);
  });

  it('debería eliminar un usuario', () => {
    service.deleteUsuarioById(mockUsuario.id_usuario!).subscribe(res => {
      expect(res).toBeNull(); 
    });

    const req = httpMock.expectOne(`${apiUrl}/${mockUsuario.id_usuario}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });

  it('debería obtener un usuario por id', () => {
    service.getUsuarioById(mockUsuario.id_usuario!).subscribe(usuario => {
      expect(usuario).toEqual(mockUsuario);
    });

    const req = httpMock.expectOne(`${apiUrl}/${mockUsuario.id_usuario}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockUsuario);
  });
});
