import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsuarioFooter } from './usuario-footer';

describe('UsuarioFooter', () => {
  let component: UsuarioFooter;
  let fixture: ComponentFixture<UsuarioFooter>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UsuarioFooter]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UsuarioFooter);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
