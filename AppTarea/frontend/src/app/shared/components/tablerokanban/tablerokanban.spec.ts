import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { Tablerokanban } from './tablerokanban';
import { setupTestingModule } from '../../../testing/helpers/test-bed.helper';
import { TareasService } from '../../../core/services/tareas.service';
import { TablerosService } from '../../../core/services/tableros.service';
import { mockTarea } from '../../../testing/mocks/tarea.mock';
import { of, throwError } from 'rxjs';
import Swal from 'sweetalert2';
import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { Tarea } from '../../../core/models/tarea.model';

describe('Tablerokanban', () => {
  let component: Tablerokanban;
  let fixture: ComponentFixture<Tablerokanban>;
  let tareasServiceMock: jasmine.SpyObj<TareasService>;
  let tablerosServiceMock: jasmine.SpyObj<TablerosService>;

  beforeEach(async () => {
    tareasServiceMock = jasmine.createSpyObj('TareasService', ['moverTarea', 'deleteTareaById']);
    tablerosServiceMock = jasmine.createSpyObj('TablerosService', ['getAllTableros']);

    setupTestingModule([
      { provide: TareasService, useValue: tareasServiceMock },
      { provide: TablerosService, useValue: tablerosServiceMock }
    ]);

    await TestBed.configureTestingModule({
      imports: [Tablerokanban]
    }).compileComponents();

    fixture = TestBed.createComponent(Tablerokanban);
    component = fixture.componentInstance;
    component.tareas = [mockTarea];
    fixture.detectChanges();

    // ✅ Evitar que Swal muestre popups reales
    spyOn(Swal, 'fire').and.returnValue(Promise.resolve({ isConfirmed: true } as any));
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('ngOnInit debería inicializar connectedDropLists', () => {
    component.ngOnInit();
    expect(component.connectedDropLists).toEqual([
      'columna-1',
      'columna-2',
      'columna-3'
    ]);
  });

  it('obtenerTareas debería filtrar por columna', () => {
    const tareas = component.obtenerTareas(mockTarea.id_columna);
    expect(tareas).toEqual([mockTarea]);
  });

  it('moverTarea debería actualizar columna si es diferente', () => {
    const event = {
      item: { data: { ...mockTarea, id_columna: 1 } }
    } as CdkDragDrop<Tarea[]>;

    tareasServiceMock.moverTarea.and.returnValue(of({}));

    component.moverTarea(event, 2);

    expect(tareasServiceMock.moverTarea).toHaveBeenCalled();
  });

  it('moverTarea no debe hacer nada si la columna es igual', () => {
    const event = {
      item: { data: { ...mockTarea, id_columna: 1 } }
    } as CdkDragDrop<Tarea[]>;

    component.moverTarea(event, 1);

    expect(tareasServiceMock.moverTarea).not.toHaveBeenCalled();
  });

  it('moverTarea debe manejar error con Swal', fakeAsync(() => {
    const event = {
      item: { data: { ...mockTarea, id_columna: 1 } }
    } as CdkDragDrop<Tarea[]>;

    tareasServiceMock.moverTarea.and.returnValue(throwError(() => ({ error: { mensaje: 'Error' } })));

    component.moverTarea(event, 2);
    tick();

    expect(Swal.fire).toHaveBeenCalled();
  }));

  it('refrescarTarea debería actualizar tarea en la lista', () => {
    const tareaActualizada = { ...mockTarea, titulo: 'Actualizado' };
    component.refrescarTarea(tareaActualizada);
    expect(component.tareas[0].titulo).toBe('Actualizado');
  });

  it('eliminarTarea debería eliminar tarea si se confirma', fakeAsync(() => {
    tareasServiceMock.deleteTareaById.and.returnValue(of(undefined));

    component.eliminarTarea(mockTarea);
    tick();

    expect(tareasServiceMock.deleteTareaById).toHaveBeenCalledWith(mockTarea.id_tarea);
    expect(component.tareas.length).toBe(0);
    expect(Swal.fire).toHaveBeenCalled();
  }));

  
});
