import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TareasAsignadas } from './tareas-asignadas';
import { TareasService } from '../../../core/services/Tarea/tareas.service';
import { of, throwError } from 'rxjs';
import { mockTareas } from '../../../testing/mocks/tarea.mock';
import { nuevaAsignacion, nuevaAsignacionTitulo } from '../../../state/tarea.state';

describe('TareasAsignadas Component', () => {
  let component: TareasAsignadas;
  let fixture: ComponentFixture<TareasAsignadas>;
  let tareasService: jest.Mocked<TareasService>;

  beforeEach(() => {
    const tareasServiceMock = {
      getTareasByUsuario: jest.fn(),
    };

    TestBed.configureTestingModule({
      imports: [TareasAsignadas],
      providers: [
        { provide: TareasService, useValue: tareasServiceMock },
      ],
    });

    fixture = TestBed.createComponent(TareasAsignadas);
    component = fixture.componentInstance;
    tareasService = TestBed.inject(TareasService) as jest.Mocked<TareasService>;
  });

  it('debería cargar tareas asignadas al iniciar', () => {
    tareasService.getTareasByUsuario.mockReturnValue(of(mockTareas));
    component.ngOnInit();
    expect(component.tareas).toEqual(mockTareas);
  });

  it('debería manejar error al cargar tareas asignadas', () => {
    tareasService.getTareasByUsuario.mockReturnValue(throwError(() => new Error('Error')));
    component.ngOnInit();
    expect(component.errorMessage).toEqual(['No se pudieron cargar las tareas asignadas.']);
  });

  it('debería cerrar la notificación correctamente', () => {
    nuevaAsignacion.set(true);
    nuevaAsignacionTitulo.set('Tarea urgente');

    component.cerrarNotificacion();

    expect(nuevaAsignacion()).toBe(false);
    expect(nuevaAsignacionTitulo()).toBe(null);
  });
});
