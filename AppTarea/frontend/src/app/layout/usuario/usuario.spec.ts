import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Usuario } from './usuario';
import { RouterTestingModule } from '@angular/router/testing';

describe('Usuario', () => {
  let component: Usuario;
  let fixture: ComponentFixture<Usuario>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        Usuario,
        RouterTestingModule // ✅ Necesario para soportar RouterModule y routerLink
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Usuario);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
