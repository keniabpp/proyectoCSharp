
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateTareas } from './create-tareas';
import { UsuariosService } from '../../../../../core/services/Usuario/usuarios.service';
import { TablerosService } from '../../../../../core/services/Tableros/tableros.service';
import { TareasService } from '../../../../../core/services/Tarea/tareas.service';
import { of, throwError } from 'rxjs';
import {
    mockUsuario,
    mockUsuarios,
} from '../../../../../testing/mocks/usuario.mock';
import {
    mockTablero,
    mockTableros,
} from '../../../../../testing/mocks/tablero.mock';
import {
    mockCreateTareaDTO,
    mockTarea,
} from '../../../../../testing/mocks/tarea.mock';

describe('CreateTareas Component', () => {
    let component: CreateTareas;
    let fixture: ComponentFixture<CreateTareas>;
    let usuariosService: jest.Mocked<UsuariosService>;
    let tablerosService: jest.Mocked<TablerosService>;
    let tareasService: jest.Mocked<TareasService>;

    beforeEach(() => {
        const usuariosServiceMock = {getAllUsuarios: jest.fn(),};
        const tablerosServiceMock = {getAllTableros: jest.fn(),};
        const tareasServiceMock = {createTarea: jest.fn(),notificarNuevaAsignacion: jest.fn(),};

        TestBed.configureTestingModule({
            imports: [CreateTareas],
            providers: [
                { provide: UsuariosService, useValue: usuariosServiceMock },
                { provide: TablerosService, useValue: tablerosServiceMock },
                { provide: TareasService, useValue: tareasServiceMock },
            ],
        });

        fixture = TestBed.createComponent(CreateTareas);
        component = fixture.componentInstance;
        usuariosService = TestBed.inject(UsuariosService) as jest.Mocked<UsuariosService>;
        tablerosService = TestBed.inject(TablerosService) as jest.Mocked<TablerosService>;
        tareasService = TestBed.inject(TareasService) as jest.Mocked<TareasService>;
    });

    it('debería cargar usuarios y tableros al iniciar', () => {
        usuariosService.getAllUsuarios.mockReturnValue(of(mockUsuarios));
        tablerosService.getAllTableros.mockReturnValue(of(mockTableros));

        component.ngOnInit();

        expect(component.usuarios).toEqual(mockUsuarios);
        expect(component.tableros).toEqual(mockTableros);
    });

    it('debería agregar una tarea y limpiar el formulario', () => {
        tareasService.createTarea.mockReturnValue(of(mockTarea));
        tareasService.notificarNuevaAsignacion.mockImplementation(() => { });
        const onTareaCreadaMock = jest.fn();
        component.onTareaCreada = onTareaCreadaMock;

        component.nuevaTarea = { ...mockCreateTareaDTO };
        component.addTarea();

        expect(tareasService.createTarea).toHaveBeenCalledWith(mockCreateTareaDTO);
        expect(tareasService.notificarNuevaAsignacion).toHaveBeenCalledWith(mockTarea.titulo);
        expect(onTareaCreadaMock).toHaveBeenCalledWith(mockTarea);
        expect(component.nuevaTarea).toEqual(
            expect.objectContaining({
                titulo: '',
                descripcion: '',
                creado_por: 0,
                asignado_a: 0,
                id_tablero: 0,
                id_columna: 1,
            })
        );
        expect(component.nuevaTarea.fecha_vencimiento).toBeInstanceOf(Date);
    });

    it('debería manejar errores al crear tarea', () => {
        const errorResponse = {
            error: { errores: [{ mensaje: 'Título requerido' }] },
        };
        tareasService.createTarea.mockReturnValue(throwError(() => errorResponse));

        component.nuevaTarea = { ...mockCreateTareaDTO };
        component.addTarea();

        expect(component.errorMessage).toEqual(['Título requerido']);
    });
});
