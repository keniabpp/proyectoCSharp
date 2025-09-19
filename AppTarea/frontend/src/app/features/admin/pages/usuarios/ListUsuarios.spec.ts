
import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { UsuariosService } from '../../../../core/services/usuarios.service';
import { of, throwError } from 'rxjs';
import Swal from 'sweetalert2';
import { ListUsuarios } from './ListUsuarios';
import { mockUsuario, mockUsuarios } from '../../../../testing/mocks/usuario.mock';

describe('ListUsuarios', () => {
    let component: ListUsuarios;
    let fixture: ComponentFixture<ListUsuarios>;
    let usuariosServiceMock: jasmine.SpyObj<UsuariosService>;

    beforeEach(async () => {
        usuariosServiceMock = jasmine.createSpyObj('UsuariosService', ['getAllUsuarios', 'deleteUsuarioById']);

        await TestBed.configureTestingModule({
            imports: [ListUsuarios],
            providers: [
                { provide: UsuariosService, useValue: usuariosServiceMock }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(ListUsuarios);
        component = fixture.componentInstance;

        // Evitar que Swal muestre popups reales
        spyOn(Swal, 'fire').and.returnValue(Promise.resolve({ isConfirmed: true } as any));
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('debería cargar usuarios al iniciar', () => {
        usuariosServiceMock.getAllUsuarios.and.returnValue(of(mockUsuarios));

        component.ngOnInit();

        expect(usuariosServiceMock.getAllUsuarios).toHaveBeenCalled();
        expect(component.usuarios).toEqual(mockUsuarios);
    });

    it('emitirUsuario debe emitir usuario seleccionado', () => {
        spyOn(component.usuarioSeleccionado, 'emit');

        component.emitirUsuario(mockUsuario);

        expect(component.usuarioSeleccionado.emit).toHaveBeenCalledWith(mockUsuario);
    });

    it('deleteUsuario debe llamar al servicio y actualizar lista', async () => {
        usuariosServiceMock.getAllUsuarios.and.returnValue(of(mockUsuarios));
        usuariosServiceMock.deleteUsuarioById.and.returnValue(of(undefined));

        await component.deleteUsuario(mockUsuario.id_usuario!); // espera la promesa

        expect(usuariosServiceMock.deleteUsuarioById).toHaveBeenCalledWith(mockUsuario.id_usuario!);
        expect(usuariosServiceMock.getAllUsuarios).toHaveBeenCalled(); // ✅ ahora sí se llama
    });





    it('listUsuarios debe manejar error', () => {
        const consoleSpy = spyOn(console, 'error');
        usuariosServiceMock.getAllUsuarios.and.returnValue(throwError(() => ({ message: 'Error' })));

        component.listUsuarios();

        expect(consoleSpy).toHaveBeenCalledWith('Error al cargar usuarios:', { message: 'Error' });
    });
});
