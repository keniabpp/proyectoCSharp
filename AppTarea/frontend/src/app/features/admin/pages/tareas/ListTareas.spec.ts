import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { ListTareas } from './ListTareas';
import { TareasService } from '../../../../core/services/tareas.service';
import { of, throwError } from 'rxjs';
import Swal from 'sweetalert2';
import { mockTarea, mockTareas } from '../../../../testing/mocks/tarea.mock';

describe('ListTareas', () => {
  let component: ListTareas;
  let fixture: ComponentFixture<ListTareas>;
  let tareasServiceMock: jasmine.SpyObj<TareasService>;

  beforeEach(async () => {
    tareasServiceMock = jasmine.createSpyObj('TareasService', ['getAllTareas', 'deleteTareaById']);

    await TestBed.configureTestingModule({
      imports: [ListTareas],
      providers: [
        { provide: TareasService, useValue: tareasServiceMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ListTareas);
    component = fixture.componentInstance;

    // Evitar popups reales de Swal
    spyOn(Swal, 'fire').and.returnValue(Promise.resolve({ isConfirmed: true } as any));
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('debería cargar tareas al iniciar', () => {
    tareasServiceMock.getAllTareas.and.returnValue(of(mockTareas));

    component.ngOnInit();

    expect(tareasServiceMock.getAllTareas).toHaveBeenCalled();
    expect(component.tareas).toEqual(mockTareas);
  });

  it('emitirTarea debe emitir tarea seleccionada', () => {
    spyOn(component.tareaSeleccionada, 'emit');

    component.emitirTarea(mockTarea);

    expect(component.tareaSeleccionada.emit).toHaveBeenCalledWith(mockTarea);
  });

  it('deleteTarea debe llamar al servicio y actualizar lista', fakeAsync(() => {
    tareasServiceMock.getAllTareas.and.returnValue(of(mockTareas));
    tareasServiceMock.deleteTareaById.and.returnValue(of(void 0));

    component.deleteTarea(mockTarea.id_tarea);
    tick(); // Esperar resolución de Swal

    expect(tareasServiceMock.deleteTareaById).toHaveBeenCalledWith(mockTarea.id_tarea);
    expect(tareasServiceMock.getAllTareas).toHaveBeenCalled();
  }));

  it('listTareas debe manejar error', () => {
    const consoleSpy = spyOn(console, 'error');
    tareasServiceMock.getAllTareas.and.returnValue(throwError(() => ({ message: 'Error' })));

    component.listTareas();

    expect(consoleSpy).toHaveBeenCalledWith('Error al cargar tareas:', { message: 'Error' });
  });

  it('deleteTarea  manejar error al eliminar', fakeAsync(() => {
    const consoleSpy = spyOn(console, 'error');
    tareasServiceMock.deleteTareaById.and.returnValue(throwError(() => ({ message: 'Falló' })));

    component.deleteTarea(mockTarea.id_tarea);
    tick();

    expect(consoleSpy).toHaveBeenCalledWith('Error al eliminar:', { message: 'Falló' });
  }));
});
