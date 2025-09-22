import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateTareas } from './update-tareas';

describe('UpdateTareas', () => {
  let component: UpdateTareas;
  let fixture: ComponentFixture<UpdateTareas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateTareas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateTareas);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
