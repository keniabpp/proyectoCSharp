
import { TestBed, ComponentFixture } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { UpdateTableros } from './UpdateTableros';
import { TablerosService } from '../../../../core/services/tableros.service';
import { mockTablero, mockTableroUpdate } from '../../../../testing/mocks/tablero.mock';

describe('UpdateTableros', () => {
  let component: UpdateTableros;
  let fixture: ComponentFixture<UpdateTableros>;
  let tablerosServiceMock: jasmine.SpyObj<TablerosService>;

  beforeEach(async () => {
    tablerosServiceMock = jasmine.createSpyObj('TablerosService', ['updateTablero']);

    await TestBed.configureTestingModule({
      imports: [UpdateTableros],
      providers: [
        { provide: TablerosService, useValue: tablerosServiceMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(UpdateTableros);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('cargarTableroParaEditar asigna correctamente el tablero', () => {
    component.cargarTableroParaEditar(mockTablero);
    expect(component.tableroActualizado.nombre).toBe(mockTablero.nombre);
    expect(component.idTableroEditando).toBe(mockTablero.id_tablero!);
  });

  it('updateTablero llama al servicio y emite evento', () => {
    component.idTableroEditando = mockTablero.id_tablero!;
    component.tableroActualizado = mockTableroUpdate;
    spyOn(component.tableroActualizadoEvent, 'emit');

    tablerosServiceMock.updateTablero.and.returnValue(of(undefined));

    component.updateTablero();

    expect(tablerosServiceMock.updateTablero).toHaveBeenCalledWith(
      mockTablero.id_tablero!,
      mockTableroUpdate
    );
    expect(component.tableroActualizadoEvent.emit).toHaveBeenCalled();
  });

  it('updateTablero maneja errores correctamente', () => {
    const errorResponse = { error: { mensaje: 'Error al actualizar' } };
    component.idTableroEditando = mockTablero.id_tablero!;
    component.tableroActualizado = mockTableroUpdate;

    tablerosServiceMock.updateTablero.and.returnValue(throwError(() => errorResponse));

    component.updateTablero();

    expect(component.errorMessage).toEqual(['Error al actualizar']);
  });
});
