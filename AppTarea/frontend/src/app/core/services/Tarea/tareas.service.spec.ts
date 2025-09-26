import { TestBed } from '@angular/core/testing';
import { TareasService } from './tareas.service';
import { HttpTestingController } from '@angular/common/http/testing';
import { setupTestingModule } from '../../../testing/helpers/test-bed.helper';
import {
  mockTarea,
  mockTareas,
  mockCreateTareaDTO,
  mockTareaUpdate,
  mockMoverTarea,
} from '../../../testing/mocks/tarea.mock';
import { nuevaAsignacion, nuevaAsignacionTitulo } from '../../../state/tarea.state';

describe('TareasService', () => {
  let service: TareasService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    setupTestingModule([TareasService]);
    service = TestBed.inject(TareasService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('debería notificar nueva asignación', () => {
    service.notificarNuevaAsignacion('Nueva tarea asignada');
    expect(nuevaAsignacion()).toBe(true);
    expect(nuevaAsignacionTitulo()).toBe('Nueva tarea asignada');
  });

  it('debería obtener todas las tareas', () => {
    service.getAllTareas().subscribe((tareas) => {
      expect(tareas).toEqual(mockTareas);
    });

    const req = httpMock.expectOne((r) => r.url.endsWith('/tareas'));
    expect(req.request.method).toBe('GET');
    req.flush(mockTareas);
  });

  it('debería crear una tarea', () => {
    service.createTarea(mockCreateTareaDTO).subscribe((response) => {
      expect(response).toEqual(mockTarea);
    });

    const req = httpMock.expectOne((r) => r.url.endsWith('/tareas'));
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockCreateTareaDTO);
    req.flush(mockTarea);
  });

  it('debería eliminar una tarea por ID', () => {
    const id = mockTarea.id_tarea!;
    service.deleteTareaById(id).subscribe((response) => {
      expect(response).toBeUndefined();
    });

    const req = httpMock.expectOne((r) => r.url.endsWith(`/tareas/${id}`));
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });

  it('debería actualizar una tarea por ID', () => {
    const id = mockTarea.id_tarea!;
    service.updateTarea(id, mockTareaUpdate).subscribe((response) => {
      expect(response).toEqual(mockTarea);
    });

    const req = httpMock.expectOne((r) => r.url.endsWith(`/tareas/${id}`));
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(mockTareaUpdate);
    req.flush(mockTarea);
  });

  it('debería mover una tarea a otra columna', () => {
    service.moverTarea(mockMoverTarea).subscribe((response) => {
      expect(response).toEqual({ success: true });
    });

    const req = httpMock.expectOne((r) =>
      r.url.endsWith(`/tareas/${mockMoverTarea.id_tarea}/moverTarea`)
    );
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(mockMoverTarea);
    req.flush({ success: true });
  });

  it('debería obtener tareas asignadas a un usuario', () => {
    localStorage.setItem('id', '2');

    service.getTareasByUsuario().subscribe((tareas) => {
      expect(tareas).toEqual(mockTareas);
    });

    const req = httpMock.expectOne((r) =>
      r.url.includes('/tareas/tareasAsignadas?id_usuario=2')
    );
    expect(req.request.method).toBe('GET');
    req.flush(mockTareas);
  });
});
