
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ListTareas } from './list-tareas';
import { TareasService } from '../../../../../core/services/Tarea/tareas.service';
import { of, throwError } from 'rxjs';
import { mockTarea, mockTareas } from '../../../../../testing/mocks/tarea.mock';
import Swal from 'sweetalert2';

jest.mock('sweetalert2', () => ({
  fire: jest.fn().mockResolvedValue({ isConfirmed: true }),
}));

describe('ListTareas Component', () => {
  let component: ListTareas;
  let fixture: ComponentFixture<ListTareas>;
  let tareasService: jest.Mocked<TareasService>;

  beforeEach(() => {
    const tareasServiceMock = {getAllTareas: jest.fn(),deleteTareaById: jest.fn(),};

    TestBed.configureTestingModule({
      imports: [ListTareas],
      providers: [
        { provide: TareasService, useValue: tareasServiceMock },
      ],
    });

    fixture = TestBed.createComponent(ListTareas);
    component = fixture.componentInstance;
    tareasService = TestBed.inject(TareasService) as jest.Mocked<TareasService>;
  });

  it('debería cargar tareas al iniciar', () => {
    tareasService.getAllTareas.mockReturnValue(of(mockTareas));
    component.ngOnInit();
    expect(component.tareas).toEqual(mockTareas);
  });

  it('debería emitir una tarea seleccionada', () => {
    const callbackMock = jest.fn();
    component.onTareaSeleccionada = callbackMock;
    component.emitirTarea(mockTarea);
    expect(callbackMock).toHaveBeenCalledWith(mockTarea);
  });

  it('debería eliminar una tarea si se confirma en Swal', async () => {
    tareasService.deleteTareaById.mockReturnValue(of(undefined));
    tareasService.getAllTareas.mockReturnValue(of(mockTareas));

    await component.deleteTarea(mockTarea.id_tarea!);

    expect(Swal.fire).toHaveBeenCalled();
    expect(tareasService.deleteTareaById).toHaveBeenCalledWith(mockTarea.id_tarea);
  });

  it('debería manejar error al eliminar tarea', async () => {
    const errorResponse = {
      error: { mensaje: 'No autorizado' },
    };
    tareasService.deleteTareaById.mockReturnValue(throwError(() => errorResponse));
    tareasService.getAllTareas.mockReturnValue(of(mockTareas));

    await component.deleteTarea(mockTarea.id_tarea!);

    expect(Swal.fire).toHaveBeenCalledWith(
      expect.objectContaining({
        icon: 'warning',
        title: 'Acción no permitida',
        text: 'No autorizado',
      })
    );
  });
});
