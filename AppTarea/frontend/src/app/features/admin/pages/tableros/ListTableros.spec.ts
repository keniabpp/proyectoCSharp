import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { TablerosService } from '../../../../core/services/tableros.service';
import { of, throwError } from 'rxjs';
import Swal from 'sweetalert2';
import { ListTableros } from './ListTableros';
import { mockTablero, mockTableros } from '../../../../testing/mocks/tablero.mock';

describe('ListTableros', () => {
  let component: ListTableros;
  let fixture: ComponentFixture<ListTableros>;
  let tablerosServiceMock: jasmine.SpyObj<TablerosService>;

  beforeEach(async () => {
    tablerosServiceMock = jasmine.createSpyObj('TablerosService', ['getAllTableros', 'deleteTableroById']);

    await TestBed.configureTestingModule({
      imports: [ListTableros],
      providers: [
        { provide: TablerosService, useValue: tablerosServiceMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ListTableros);
    component = fixture.componentInstance;

    // Evitar popups reales de Swal
    spyOn(Swal, 'fire').and.returnValue(Promise.resolve({ isConfirmed: true } as any));
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('debería cargar tableros al iniciar', () => {
    tablerosServiceMock.getAllTableros.and.returnValue(of(mockTableros));

    component.ngOnInit();

    expect(tablerosServiceMock.getAllTableros).toHaveBeenCalled();
    expect(component.tableros).toEqual(mockTableros);
  });

  it('emitirTablero debe emitir tablero seleccionado', () => {
    spyOn(component.tableroSeleccionado, 'emit');

    component.emitirTablero(mockTablero);

    expect(component.tableroSeleccionado.emit).toHaveBeenCalledWith(mockTablero);
  });

  it('deleteTablero debe llamar al servicio y actualizar lista', async () => {
    tablerosServiceMock.getAllTableros.and.returnValue(of(mockTableros));
    tablerosServiceMock.deleteTableroById.and.returnValue(of(void 0));

    await component.deleteTablero(mockTablero.id_tablero);

    expect(tablerosServiceMock.deleteTableroById).toHaveBeenCalledWith(mockTablero.id_tablero);
    expect(tablerosServiceMock.getAllTableros).toHaveBeenCalled(); 
  });

  it('listTableros debe manejar error', () => {
    const consoleSpy = spyOn(console, 'error');
    tablerosServiceMock.getAllTableros.and.returnValue(throwError(() => ({ message: 'Error' })));

    component.listTableros();

    expect(consoleSpy).toHaveBeenCalledWith('Error al cargar tableros:', { message: 'Error' });
  });
});
