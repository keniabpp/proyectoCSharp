import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Usuarios } from './usuarios';
import { Usuario } from '../../../../core/models/usuario.model';
import { mockUsuario, mockUsuarios } from '../../../../testing/mocks/usuario.mock';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('Usuarios', () => {
  let component: Usuarios;
  let fixture: ComponentFixture<Usuarios>;
  beforeEach(async () => {
    

    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule] 
    }).compileComponents();

    fixture = TestBed.createComponent(Usuarios);
    component = fixture.componentInstance;

    
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  
});
