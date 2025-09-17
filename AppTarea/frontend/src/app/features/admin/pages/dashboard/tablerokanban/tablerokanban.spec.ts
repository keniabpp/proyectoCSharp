import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Tablerokanban } from './tablerokanban';

describe('Tablerokanban', () => {
  let component: Tablerokanban;
  let fixture: ComponentFixture<Tablerokanban>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Tablerokanban]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Tablerokanban);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
