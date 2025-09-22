import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Tareas } from './tareas';
import { setupTestingModule } from '../../../../testing/helpers/test-bed.helper';

// Mocks
import { mockTarea } from '../../../../testing/mocks/tarea.mock';

describe('Tareas', () => {
  let component: Tareas;
  let fixture: ComponentFixture<Tareas>;

  beforeEach(async () => {
    
    setupTestingModule();

    await TestBed.configureTestingModule({
      imports: [Tareas]
    }).compileComponents();

    fixture = TestBed.createComponent(Tareas);
    component = fixture.componentInstance;

    
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  
});
