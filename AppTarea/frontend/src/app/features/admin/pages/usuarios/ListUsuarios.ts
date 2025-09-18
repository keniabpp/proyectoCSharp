import { CommonModule } from "@angular/common";
import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { Usuario } from "../../../../core/models/usuario.model";
import { UsuariosService } from "../../../../core/services/usuarios.service";
import Swal from "sweetalert2";


@Component({
    selector: 'app-listUsuarios',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './ListUsuarios.html',
    styleUrl: './usuarios.css'
})

export class ListUsuarios implements OnInit {

    @Output() usuarioSeleccionado = new EventEmitter<Usuario>();

    usuarios: Usuario[] = [];

    errorMessage: string[] = [];

    constructor(private usuariosService: UsuariosService) { }

    emitirUsuario(usuario: Usuario): void {
        this.usuarioSeleccionado.emit(usuario);
    }


    ngOnInit(): void {
        this.listUsuarios();
    }


    listUsuarios() {
        this.usuariosService.getAllUsuarios().subscribe({
            next: (data) => (this.usuarios = data),

            error: (err) => {
                console.error('Error al cargar usuarios:', err);

            }
        })
    }


    deleteUsuario(id_usuario: number) {
        Swal.fire({
            title: '¿Estás seguro?',
            text: 'Esta acción eliminará al usuario permanentemente.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                this.usuariosService.deleteUsuarioById(id_usuario).subscribe({
                    next: () => {
                        // Actualiza la lista de usuarios
                        this.listUsuarios();

                        // Muestra mensaje de éxito
                        Swal.fire({
                            title: '¡Eliminado!',
                            text: 'El usuario ha sido eliminado correctamente.',
                            icon: 'success',
                            timer: 2000,
                            showConfirmButton: false
                        });
                    },



                    error: (err) => {
                        const mensaje = err.error?.mensaje || 'No se pudo eliminar el usuario';

                        Swal.fire({
                            icon: 'warning',
                            title: 'Acción no permitida',
                            text: mensaje,
                            confirmButtonText: 'Entendido',
                            confirmButtonColor: '#3085d6'
                        });


                    }
                });
            }
        });
    }









}