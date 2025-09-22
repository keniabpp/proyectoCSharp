import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListTareas } from './list-tareas';

describe('ListTareas', () => {
  let component: ListTareas;
  let fixture: ComponentFixture<ListTareas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListTareas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListTareas);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
