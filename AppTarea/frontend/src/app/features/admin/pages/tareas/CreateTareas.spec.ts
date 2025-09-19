import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateTareas } from './CreateTareas';
import { UsuariosService } from '../../../../core/services/usuarios.service';
import { TablerosService } from '../../../../core/services/tableros.service';
import { TareasService } from '../../../../core/services/tareas.service';
import { of, throwError } from 'rxjs';
import { mockUsuario, mockUsuarios } from '../../../../testing/mocks/usuario.mock';
import { mockTablero, mockTableros } from '../../../../testing/mocks/tablero.mock';
import { mockTarea, mockCreateTareaDTO } from '../../../../testing/mocks/tarea.mock';

describe('CreateTareas', () => {
  let component: CreateTareas;
  let fixture: ComponentFixture<CreateTareas>;
  let usuariosServiceMock: jasmine.SpyObj<UsuariosService>;
  let tablerosServiceMock: jasmine.SpyObj<TablerosService>;
  let tareasServiceMock: jasmine.SpyObj<TareasService>;

  beforeEach(async () => {
    usuariosServiceMock = jasmine.createSpyObj('UsuariosService', ['getAllUsuarios']);
    tablerosServiceMock = jasmine.createSpyObj('TablerosService', ['getAllTableros']);
    tareasServiceMock = jasmine.createSpyObj('TareasService', ['createTarea', 'notificarNuevaAsignacion']);

    await TestBed.configureTestingModule({
      imports: [CreateTareas],
      providers: [
        { provide: UsuariosService, useValue: usuariosServiceMock },
        { provide: TablerosService, useValue: tablerosServiceMock },
        { provide: TareasService, useValue: tareasServiceMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(CreateTareas);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('debería inicializar columnas ', () => {
    expect(component.columnas.length).toBeGreaterThan(0);
    expect(component.columnas[0].nombre).toBe('Por hacer');
    expect(component.nuevaTarea.id_columna).toBe(1);
  });

  it(' debería cargar usuarios y tableros', () => {
    usuariosServiceMock.getAllUsuarios.and.returnValue(of(mockUsuarios));
    tablerosServiceMock.getAllTableros.and.returnValue(of(mockTableros));

    component.ngOnInit();

    expect(usuariosServiceMock.getAllUsuarios).toHaveBeenCalled();
    expect(tablerosServiceMock.getAllTableros).toHaveBeenCalled();
    expect(component.usuarios).toEqual(mockUsuarios);
    expect(component.tableros).toEqual(mockTableros);
  });

  it('addTarea debería crear tarea y emitir evento', () => {
    component.nuevaTarea = mockCreateTareaDTO;
    spyOn(component.tareaCreadaEvent, 'emit');
    tareasServiceMock.createTarea.and.returnValue(of(mockTarea));

    component.addTarea();

    expect(tareasServiceMock.createTarea).toHaveBeenCalledWith(mockCreateTareaDTO);
    expect(component.tareas).toContain(mockTarea);
    expect(component.tareaCreadaEvent.emit).toHaveBeenCalledWith(mockTarea);
    expect(tareasServiceMock.notificarNuevaAsignacion).toHaveBeenCalledWith(mockTarea.titulo);
    expect(component.errorMessage.length).toBe(0);
  });

  it('addTarea debería manejar error con array de mensajes', () => {
    const errorResponse = {
      error: {
        errores: [{ mensaje: 'Título requerido' }]
      }
    };
    component.nuevaTarea = mockCreateTareaDTO;
    tareasServiceMock.createTarea.and.returnValue(throwError(() => errorResponse));

    component.addTarea();

    expect(component.errorMessage).toEqual(['Título requerido']);
  });

  

  
});
