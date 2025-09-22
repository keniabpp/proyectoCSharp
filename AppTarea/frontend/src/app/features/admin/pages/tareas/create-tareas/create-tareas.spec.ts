import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTareas } from './create-tareas';

describe('CreateTareas', () => {
  let component: CreateTareas;
  let fixture: ComponentFixture<CreateTareas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateTareas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateTareas);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
