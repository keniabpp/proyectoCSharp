import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Usuarios } from './usuarios';
import { UpdateUsuarios } from './UpdateUsuarios';
import { ListUsuarios } from './ListUsuarios';
import { Usuario } from '../../../../core/models/usuario.model';
import { mockUsuario, mockUsuarios } from '../../../../testing/mocks/usuario.mock';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('Usuarios', () => {
  let component: Usuarios;
  let fixture: ComponentFixture<Usuarios>;
  let mockUpdateComponent: jasmine.SpyObj<UpdateUsuarios>;
  let mockListComponent: jasmine.SpyObj<ListUsuarios>;

  beforeEach(async () => {
    mockUpdateComponent = jasmine.createSpyObj('UpdateUsuarios', ['cargarUsuarioParaEditar']);
    mockListComponent = jasmine.createSpyObj('ListUsuarios', ['listUsuarios'], { usuarios: [...mockUsuarios] });

    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule] // <-- aquí
    }).compileComponents();

    fixture = TestBed.createComponent(Usuarios);
    component = fixture.componentInstance;

    component.updateComponent = mockUpdateComponent;
    component.listComponent = mockListComponent;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('seleccionarUsuario para actualizarlo', () => {
    const usuario: Usuario = mockUsuario;
    component.seleccionarUsuario(usuario);
    expect(mockUpdateComponent.cargarUsuarioParaEditar).toHaveBeenCalledWith(usuario);
  });

  it('refrescarLista ', () => {
    component.refrescarLista();
    expect(mockListComponent.listUsuarios).toHaveBeenCalled();
  });

  it('agregarUsuarioALaLista ', () => {
    const nuevoUsuario: Usuario = {
      id_usuario: 3,
      nombre: 'Pedro',
      apellido: 'Ramírez',
      email: 'pedro@example.com',
      telefono: '5551234',
      contrasena: 'pass123',
      id_rol: 1
    };
    component.agregarUsuarioALaLista(nuevoUsuario);
    expect(mockListComponent.usuarios).toContain(nuevoUsuario);
  });
});
