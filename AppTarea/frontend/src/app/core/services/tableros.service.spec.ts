
import { TablerosService } from './tableros.service';
import { setupTestingModule } from '../../testing/helpers/test-bed.helper';
import { HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { environment } from '../../../environments/environment';
import { mockTablero, mockTableros, mockCreateTableroDTO, mockTableroUpdate } from '../../testing/mocks/tablero.mock';

describe('TablerosService', () => {
  let service: TablerosService;
  let httpMock: HttpTestingController;
  const apiUrl = `${environment.apiUrl}/tableros`;

  beforeEach(() => {
    setupTestingModule([TablerosService]);
    service = TestBed.inject(TablerosService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('debería obtener todos los tableros', () => {
    service.getAllTableros().subscribe(tableros => {
      expect(tableros).toEqual(mockTableros);
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockTableros);
  });

  it('debería crear un tablero', () => {
    service.createTablero(mockCreateTableroDTO).subscribe(tablero => {
      expect(tablero).toEqual(mockTablero);
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('POST');
    req.flush(mockTablero);
  });

  it('debería actualizar un tablero', () => {
    service.updateTablero(mockTablero.id_tablero, mockTableroUpdate).subscribe(res => {
      expect(res).toBeNull(); // PUT devuelve void
    });

    const req = httpMock.expectOne(`${apiUrl}/${mockTablero.id_tablero}`);
    expect(req.request.method).toBe('PUT');
    req.flush(null);
  });

  it('debería eliminar un tablero', () => {
    service.deleteTableroById(mockTablero.id_tablero).subscribe(res => {
      expect(res).toBeNull(); // DELETE devuelve void
    });

    const req = httpMock.expectOne(`${apiUrl}/${mockTablero.id_tablero}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });
});
