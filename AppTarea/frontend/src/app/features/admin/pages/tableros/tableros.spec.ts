
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Tableros } from './tableros';
import { TablerosService } from '../../../../core/services/Tableros/tableros.service';
import { of, throwError } from 'rxjs';
import {
  mockTablero,
  mockTableros,
  mockCreateTableroDTO,
  mockTableroUpdate,
} from '../../../../testing/mocks/tablero.mock';
import Swal from 'sweetalert2';

jest.mock('sweetalert2', () => ({
  fire: jest.fn().mockResolvedValue({ isConfirmed: true }),
}));

describe('Tableros Component', () => {
  let component: Tableros;
  let fixture: ComponentFixture<Tableros>;
  let tablerosService: jest.Mocked<TablerosService>;

  beforeEach(() => {
    const tablerosServiceMock = {
      getAllTableros: jest.fn(),
      createTablero: jest.fn(),
      updateTablero: jest.fn(),
      deleteTableroById: jest.fn(),
    };

    TestBed.configureTestingModule({
      imports: [Tableros],
      providers: [
        { provide: TablerosService, useValue: tablerosServiceMock },
      ],
    });

    fixture = TestBed.createComponent(Tableros);
    component = fixture.componentInstance;
    tablerosService = TestBed.inject(TablerosService) as jest.Mocked<TablerosService>;
  });

  it('debería cargar tableros al iniciar', () => {
    tablerosService.getAllTableros.mockReturnValue(of(mockTableros));
    component.ngOnInit();
    expect(component.tableros).toEqual(mockTableros);
  });

 

  it('debería manejar errores al agregar tablero', () => {
    const errorResponse = {
      error: { errores: [{ mensaje: 'Nombre requerido' }] },
    };
    tablerosService.createTablero.mockReturnValue(throwError(() => errorResponse));

    component.nuevoTablero = {
      nombre: '',
      creado_por: 1,
      id_rol: 1,
    };
    component.addTablero();

    expect(component.errorMessage).toEqual(['Nombre requerido']);
  });

  

  it('debería actualizar tablero correctamente', () => {
    tablerosService.updateTablero.mockReturnValue(of(undefined));
    tablerosService.getAllTableros.mockReturnValue(of(mockTableros));

    component.idTableroEditando = mockTablero.id_tablero!;
    component.tableroActualizado = mockTableroUpdate;
    component.updateTablero();

    expect(tablerosService.updateTablero).toHaveBeenCalledWith(
      mockTablero.id_tablero,
      mockTableroUpdate
    );
  });

  it('debería eliminar tablero', async () => {
    tablerosService.deleteTableroById.mockReturnValue(of(undefined));
    tablerosService.getAllTableros.mockReturnValue(of(mockTableros));

    await component.deleteTablero(mockTablero.id_tablero!);

    expect(Swal.fire).toHaveBeenCalled();
    expect(tablerosService.deleteTableroById).toHaveBeenCalledWith(mockTablero.id_tablero);
  });
});
