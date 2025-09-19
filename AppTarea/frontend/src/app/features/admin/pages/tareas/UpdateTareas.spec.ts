
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UpdateTareas } from './UpdateTareas';
import { TareasService } from '../../../../core/services/tareas.service';
import { of, throwError } from 'rxjs';
import { mockTarea, mockTareaUpdate } from '../../../../testing/mocks/tarea.mock';

describe('UpdateTareas', () => {
  let component: UpdateTareas;
  let fixture: ComponentFixture<UpdateTareas>;
  let tareasServiceMock: jasmine.SpyObj<TareasService>;

  beforeEach(async () => {
    tareasServiceMock = jasmine.createSpyObj('TareasService', ['updateTarea']);

    await TestBed.configureTestingModule({
      imports: [UpdateTareas],
      providers: [
        { provide: TareasService, useValue: tareasServiceMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(UpdateTareas);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('cargarTareaParaEditar debe asignar correctamente los datos', () => {
    component.cargarTareaParaEditar(mockTarea);

    expect(component.tareaActualizada.titulo).toBe(mockTarea.titulo);
    expect(component.tareaActualizada.descripcion).toBe(mockTarea.descripcion);
    expect(component.idTareaEditando).toBe(mockTarea.id_tarea);
  });

  it('updateTarea debe llamar al servicio y emitir evento', () => {
    component.idTareaEditando = mockTarea.id_tarea;
    component.tareaActualizada = mockTareaUpdate;
    spyOn(component.tareaActualizadaEvent, 'emit');

    tareasServiceMock.updateTarea.and.returnValue(of(mockTarea));

    component.updateTarea();

    expect(tareasServiceMock.updateTarea).toHaveBeenCalledWith(mockTarea.id_tarea, mockTareaUpdate);
    expect(component.tareaActualizadaEvent.emit).toHaveBeenCalledWith(mockTarea);
    expect(component.errorMessage.length).toBe(0);
  });

  

  it('updateTarea debe manejar error con mensaje único', () => {
    const errorResponse = {
      error: {
        mensaje: 'Error al actualizar'
      }
    };

    component.idTareaEditando = mockTarea.id_tarea;
    component.tareaActualizada = mockTareaUpdate;

    tareasServiceMock.updateTarea.and.returnValue(throwError(() => errorResponse));

    component.updateTarea();

    expect(component.errorMessage).toEqual(['Error al actualizar']);
  });

  
});
