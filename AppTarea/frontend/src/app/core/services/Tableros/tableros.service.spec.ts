
import { TestBed } from '@angular/core/testing';
import { TablerosService } from './tableros.service';
import { HttpTestingController } from '@angular/common/http/testing';

import {
  mockTablero,
  mockTableros,
  mockCreateTableroDTO,
  mockTableroUpdate,
} from '../../../testing/mocks/tablero.mock';
import { setupTestingModule } from '../../../testing/helpers/test-bed.helper';

describe('TablerosService', () => {
  let service: TablerosService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    setupTestingModule([TablerosService]);
    service = TestBed.inject(TablerosService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('debería obtener todos los tableros', () => {
    service.getAllTableros().subscribe((tableros) => {
      expect(tableros).toEqual(mockTableros);
    });

    const req = httpMock.expectOne((r) => r.url.endsWith('/tableros'));
    expect(req.request.method).toBe('GET');
    req.flush(mockTableros);
  });

  it('debería crear un tablero', () => {
    service.createTablero(mockCreateTableroDTO).subscribe((response) => {
      expect(response).toEqual(mockTablero);
    });

    const req = httpMock.expectOne((r) => r.url.endsWith('/tableros'));
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockCreateTableroDTO);
    req.flush(mockTablero);
  });

  it('debería eliminar un tablero por ID', () => {
    const id = mockTablero.id_tablero!;
    service.deleteTableroById(id).subscribe((response) => {
      expect(response).toBeUndefined();
    });

    const req = httpMock.expectOne((r) => r.url.endsWith(`/tableros/${id}`));
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });

  it('debería actualizar un tablero por ID', () => {
    const id = mockTablero.id_tablero!;
    service.updateTablero(id, mockTableroUpdate).subscribe((response) => {
      expect(response).toBeUndefined();
    });

    const req = httpMock.expectOne((r) => r.url.endsWith(`/tableros/${id}`));
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(mockTableroUpdate);
    req.flush(null);
  });
});
