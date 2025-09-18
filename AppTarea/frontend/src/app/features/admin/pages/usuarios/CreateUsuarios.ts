import { CommonModule } from "@angular/common";
import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { Usuario } from "../../../../core/models/usuario.model";
import { UsuariosService } from "../../../../core/services/usuarios.service";


@Component({
  selector: 'app-createUsuarios',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './CreateUsuarios.html',
  styleUrl: './usuarios.css'
})

export class CreateUsuarios {

  constructor(private usuariosService: UsuariosService) { }

  @Output() usuarioCreadoEvent = new EventEmitter<Usuario>();

  errorMessage: string[] = [];

  usuarios: Usuario[] = [];

  nuevoUsuario: Usuario = {
    nombre: '',
    apellido: '',
    telefono: '',
    email: '',
    contrasena: '',
    id_rol: 0,

  };

  addUsuario(): void {
    this.usuariosService.createUsuario(this.nuevoUsuario).subscribe({
      next: (usuarioCreado) => {
        this.usuarios.push(usuarioCreado);
        this.usuarioCreadoEvent.emit(usuarioCreado);
        this.nuevoUsuario = {

          nombre: '',
          apellido: '',
          telefono: '',
          email: '',
          contrasena: '',
          id_rol: 0
        };

      },

      error: (err) => {
        console.error('Error al cargar usuarios:', err);

        if (err.error?.errores?.length) {

          this.errorMessage = err.error.errores.map((e: any) => e.mensaje);
        }
        else if (err.error?.mensaje) {

          this.errorMessage = [err.error.mensaje];
        }
        else {
          this.errorMessage = ['No se pudo registrar el usuario'];
        }
      }


    })
  }



}