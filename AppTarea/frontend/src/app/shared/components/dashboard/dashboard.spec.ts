import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Dashboard } from './dashboard';
import { setupTestingModule } from '../../../testing/helpers/test-bed.helper';
import { TareasService } from '../../../core/services/tareas.service';
import { TablerosService } from '../../../core/services/tableros.service';
import { of } from 'rxjs';
import { mockTablero, mockTableros } from '../../../testing/mocks/tablero.mock';
import { mockTarea, mockTareas } from '../../../testing/mocks/tarea.mock';

describe('Dashboard', () => {
  let component: Dashboard;
  let fixture: ComponentFixture<Dashboard>;
  let tareasServiceMock: jasmine.SpyObj<TareasService>;
  let tablerosServiceMock: jasmine.SpyObj<TablerosService>;

  beforeEach(async () => {
    tareasServiceMock = jasmine.createSpyObj('TareasService', ['getAllTareas']);
    tablerosServiceMock = jasmine.createSpyObj('TablerosService', ['getAllTableros']);

    setupTestingModule([
      { provide: TareasService, useValue: tareasServiceMock },
      { provide: TablerosService, useValue: tablerosServiceMock }
    ]);

    await TestBed.configureTestingModule({
      imports: [Dashboard]
    }).compileComponents();

    fixture = TestBed.createComponent(Dashboard);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('debería cargar tableros y seleccionarlo y q muestre las tareas q hay en ese tablero ', () => {
    tablerosServiceMock.getAllTableros.and.returnValue(of(mockTableros));
    tareasServiceMock.getAllTareas.and.returnValue(of(mockTareas));

    component.ngOnInit();

    expect(tablerosServiceMock.getAllTableros).toHaveBeenCalled();
    expect(component.tableros).toEqual(mockTableros);
    expect(component.tableroSeleccionado).toBe(mockTableros[0].id_tablero);
    expect(tareasServiceMock.getAllTareas).toHaveBeenCalled();
    expect(component.tareas.every(t => t.id_tablero === component.tableroSeleccionado)).toBeTrue();
  });

  it('debería filtrar tareas por tablero', () => {
    tareasServiceMock.getAllTareas.and.returnValue(of(mockTareas));
    component.tableroSeleccionado = mockTablero.id_tablero;

    component.cargarTareasDelTablero();

    expect(tareasServiceMock.getAllTareas).toHaveBeenCalled();
    expect(component.tareas.every(t => t.id_tablero === mockTablero.id_tablero)).toBeTrue();
  });

  it(' debería agregar tarea a la lista', () => {
    component.tareas = [];
    component.agregarTareaDesdeFormulario(mockTarea);

    expect(component.tareas).toContain(mockTarea);
  });
});
