import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TareasAsignadas } from './tareas-asignadas';

describe('TareasAsignadas', () => {
  let component: TareasAsignadas;
  let fixture: ComponentFixture<TareasAsignadas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TareasAsignadas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TareasAsignadas);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
