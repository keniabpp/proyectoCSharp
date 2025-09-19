// src/core/services/tareas.service.spec.ts

import { TareasService } from './tareas.service';
import { HttpTestingController } from '@angular/common/http/testing';
import { environment } from '../../../environments/environment';
import { mockTarea, mockTareas, mockCreateTareaDTO, mockTareaUpdate, mockMoverTarea } from '../../testing/mocks/tarea.mock';
import { setupTestingModule } from '../../testing/helpers/test-bed.helper';
import { TestBed } from '@angular/core/testing';

describe('TareasService', () => {
  let service: TareasService;
  let httpMock: HttpTestingController;
  const apiUrl = `${environment.apiUrl}/tareas`;

  beforeEach(() => {
    setupTestingModule([TareasService]);
    service = TestBed.inject(TareasService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('debería obtener todas las tareas', () => {
    service.getAllTareas().subscribe(tareas => {
      expect(tareas).toEqual(mockTareas);
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockTareas);
  });

  it('debería crear una tarea', () => {
    service.createTarea(mockCreateTareaDTO).subscribe(tarea => {
      expect(tarea).toEqual(mockTarea);
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('POST');
    req.flush(mockTarea);
  });

  it('debería eliminar una tarea por id', () => {
    service.deleteTareaById(mockTarea.id_tarea).subscribe(res => {
      expect(res).toBeNull();
    });

    const req = httpMock.expectOne(`${apiUrl}/${mockTarea.id_tarea}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });

  it('debería actualizar una tarea', () => {
    service.updateTarea(mockTarea.id_tarea, mockTareaUpdate).subscribe(res => {
      expect(res).toEqual(mockTarea);
    });

    const req = httpMock.expectOne(`${apiUrl}/${mockTarea.id_tarea}`);
    expect(req.request.method).toBe('PUT');
    req.flush(mockTarea);
  });

  it('debería mover una tarea', () => {
    service.moverTarea(mockMoverTarea).subscribe(res => {
      expect(res).toEqual({ success: true });
    });

    const req = httpMock.expectOne(`${apiUrl}/${mockMoverTarea.id_tarea}/moverTarea`);
    expect(req.request.method).toBe('PUT');
    req.flush({ success: true });
  });

  it('debería obtener tareas asignadas por usuario', () => {
    spyOn(localStorage, 'getItem').and.returnValue('1');

    service.getTareasByUsuario().subscribe(tareas => {
      expect(tareas).toEqual(mockTareas);
    });

    const req = httpMock.expectOne(`${apiUrl}/tareasAsignadas?id_usuario=1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockTareas);
  });
});

