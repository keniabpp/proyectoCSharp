import { Component } from '@angular/core';
import { Usuario, UsuarioUpdate } from '../../../core/models/usuario.model';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UsuariosService } from '../../../core/services/Usuario/usuarios.service';

@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [CommonModule, FormsModule,],
  templateUrl: './perfil.html',
  styleUrl: './perfil.css'
})
export class Perfil {

  constructor(private usuariosService: UsuariosService) { }

  usuarios: Usuario[] = [];

  errorMessage: string[] = [];

  ngOnInit(): void {
    const id = Number(localStorage.getItem('id'));
    if (id) {
      this.usuariosService.getUsuarioById(id).subscribe({
        next: (usuario) => {
          this.cargarUsuarioParaEditar(usuario);

        },
        error: (err) => {
          console.error('Error al cargar tu perfil:', err);
          this.errorMessage = ['No se pudo cargar tu perfil'];
        }
      });
    }
  }



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
        Swal.fire({
          icon: 'success',
          title: 'Perfil actualizado',
          text: 'Tus datos se han guardado correctamente'
        });

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
