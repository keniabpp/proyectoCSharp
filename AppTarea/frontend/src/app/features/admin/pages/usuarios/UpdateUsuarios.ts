import { CommonModule } from "@angular/common";
import { Component, EventEmitter, Output } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { Usuario, UsuarioUpdate } from "../../../../core/models/usuario.model";
import { UsuariosService } from "../../../../core/services/usuarios.service";
import Swal from "sweetalert2";







@Component({
  selector: 'app-updateUsuarios',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './UpdateUsuarios.html',
  styleUrl: './usuarios.css'
})

export class UpdateUsuarios {

  constructor(private usuariosService: UsuariosService) { }

  @Output() usuarioActualizadoEvent = new EventEmitter<void>();
  usuarios: Usuario[] = [];

  errorMessage: string[] = [];

  usuarioActualizado: UsuarioUpdate = {
    nombre: '',
    apellido: '',
    telefono: '',
    email: '',
    contrasena: '',


  }

  idUsuarioEditando: number = 0;


  cargarUsuarioParaEditar(usuario: Usuario) {
    this.usuarioActualizado = {
      nombre: usuario.nombre,
      apellido: usuario.apellido,
      telefono: usuario.telefono,
      email: usuario.email,
      contrasena: '',
    };
    this.idUsuarioEditando = usuario.id_usuario!;
  }

  updateUsuario(): void {
    console.log('ID que se va a actualizar:', this.idUsuarioEditando);
    this.usuariosService.updateUsuario(this.idUsuarioEditando, this.usuarioActualizado).subscribe({
      next: (respuesta) => {
        console.log(respuesta);
        this.usuarioActualizadoEvent.emit(); // refresca la lista
      },
      error: (err) => {
        console.error('Error al actualizar usuario:', err);

        if (err.error?.errores?.length) {
          this.errorMessage = err.error.errores.map((e: any) => e.mensaje);
        }
        else if (err.error?.mensaje) {
          this.errorMessage = [err.error.mensaje];
        }
        else {
          this.errorMessage = ['No se pudo actualizar el usuario'];
        }
      }


    });
  }










}