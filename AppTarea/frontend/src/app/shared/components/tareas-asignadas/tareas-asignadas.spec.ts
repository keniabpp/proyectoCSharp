import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TareasAsignadas } from './tareas-asignadas';
import { setupTestingModule } from '../../../testing/helpers/test-bed.helper';
import { TareasService } from '../../../core/services/tareas.service';
import { of, throwError } from 'rxjs';
import { mockTareas } from '../../../testing/mocks/tarea.mock';
import { nuevaAsignacion, nuevaAsignacionTitulo } from '../../../state/tarea.state';

describe('TareasAsignadas', () => {
  let component: TareasAsignadas;
  let fixture: ComponentFixture<TareasAsignadas>;
  let tareasServiceMock: jasmine.SpyObj<TareasService>;

  beforeEach(async () => {
    tareasServiceMock = jasmine.createSpyObj('TareasService', ['getTareasByUsuario']);

    setupTestingModule([
      { provide: TareasService, useValue: tareasServiceMock }
    ]);

    await TestBed.configureTestingModule({
      imports: [TareasAsignadas]
    }).compileComponents();

    fixture = TestBed.createComponent(TareasAsignadas);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('llamar getTareasByUsuario', () => {
    tareasServiceMock.getTareasByUsuario.and.returnValue(of(mockTareas));

    component.ngOnInit();

    expect(tareasServiceMock.getTareasByUsuario).toHaveBeenCalled();
    expect(component.tareas).toEqual(mockTareas);
  });

  it('getTareasByUsuario debería manejar error', () => {
    const consoleSpy = spyOn(console, 'error');
    tareasServiceMock.getTareasByUsuario.and.returnValue(throwError(() => ({ error: 'fallo' })));

    component.getTareasByUsuario();

    expect(consoleSpy).toHaveBeenCalledWith('Error al cargar tareas:', { error: 'fallo' });
    expect(component.errorMessage).toEqual(['No se pudieron cargar las tareas asignadas.']);
  });

  it('cerrarNotificacion ', () => {
    const asignacionSpy = spyOn(nuevaAsignacion, 'set');
    const tituloSpy = spyOn(nuevaAsignacionTitulo, 'set');

    component.cerrarNotificacion();

    expect(asignacionSpy).toHaveBeenCalledWith(false);
    expect(tituloSpy).toHaveBeenCalledWith(null);
  });
});
