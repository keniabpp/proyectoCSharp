import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UsuariosService } from '../../../../core/services/usuarios.service';
import { Usuario, UsuarioUpdate } from '../../../../core/models/usuario.model';
import { FormsModule } from '@angular/forms';
import { ListUsuarios } from './ListUsuarios';
import { UpdateUsuarios } from './UpdateUsuarios';
import { CreateUsuarios } from './CreateUsuarios';

@Component({
  selector: 'app-usuarios',
  standalone: true,
  imports: [CommonModule, FormsModule, ListUsuarios, UpdateUsuarios, CreateUsuarios],
  templateUrl: './usuarios.html',
  styleUrl: './usuarios.css'
})

export class Usuarios {
  @ViewChild(UpdateUsuarios) updateComponent!: UpdateUsuarios;
  @ViewChild(ListUsuarios) listComponent!: ListUsuarios;


  seleccionarUsuario(usuario: Usuario) {
    console.log('Usuario seleccionado para editar:', usuario);
    this.updateComponent.cargarUsuarioParaEditar(usuario);
  }

  refrescarLista() {

    this.listComponent.listUsuarios();
  }

  agregarUsuarioALaLista(usuario: Usuario): void {
    this.listComponent.usuarios.push(usuario);

  }

}

