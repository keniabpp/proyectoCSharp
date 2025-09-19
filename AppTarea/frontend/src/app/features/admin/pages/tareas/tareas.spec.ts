import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Tareas } from './tareas';
import { setupTestingModule } from '../../../../testing/helpers/test-bed.helper';

// Mocks
import { mockTarea } from '../../../../testing/mocks/tarea.mock';

describe('Tareas', () => {
  let component: Tareas;
  let fixture: ComponentFixture<Tareas>;

  beforeEach(async () => {
    // Usa tu helper para incluir HttpClientTestingModule
    setupTestingModule();

    await TestBed.configureTestingModule({
      imports: [Tareas]
    }).compileComponents();

    fixture = TestBed.createComponent(Tareas);
    component = fixture.componentInstance;

    // Simular componentes hijos
    component.listComponent = jasmine.createSpyObj('ListTareas', ['listTareas'], { tareas: [] });
    component.updateComponent = jasmine.createSpyObj('UpdateTareas', ['cargarTareaParaEditar']);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('agregarTareaALista debería agregar tarea a la lista', () => {
    component.agregarTareaALista(mockTarea);
    expect(component.listComponent.tareas).toContain(mockTarea);
  });

  it('seleccionarTarea debería cargar tarea en componente de edición', () => {
    component.seleccionarTarea(mockTarea);
    expect(component.updateComponent.cargarTareaParaEditar).toHaveBeenCalledWith(mockTarea);
  });

  it('refrescarLista debería llamar listTareas en componente de lista', () => {
    component.refrescarLista();
    expect(component.listComponent.listTareas).toHaveBeenCalled();
  });
});
