import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';

import { Usuario, UsuarioUpdate } from '../../../../core/models/usuario.model';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { UsuariosService } from '../../../../core/services/Usuario/usuarios.service';

@Component({
  selector: 'app-usuarios',
  standalone: true,
  imports: [CommonModule, FormsModule,],
  templateUrl: './usuarios.html',
  
})

export class Usuarios {

  constructor(private usuariosService: UsuariosService) { }

  usuarios: Usuario[] = [];

  errorMessage: string[] = [];


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
        this.listUsuarios();
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
        // console.error('Error al cargar usuarios:', err);

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
    this.usuariosService.updateUsuario(this.idUsuarioEditando, this.usuarioActualizado).subscribe({
      next: (respuesta) => {
        this.listUsuarios();


      },
      error: (err) => {
        

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
            
            this.listUsuarios();
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

