import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { createTareaDTO, Tarea } from '../../../../core/models/tarea.model';
import { TareasService } from '../../../../core/services/tareas.service';
import { Usuario } from '../../../../core/models/usuario.model';
import { Columna } from '../../../../core/models/columna.model';
import { UsuariosService } from '../../../../core/services/usuarios.service';
import { TablerosService } from '../../../../core/services/tableros.service';
import { Tablero } from '../../../../core/models/tablero.model';
import { ListTareas } from './ListTareas';
import { CreateTareas } from './CreateTareas';
import { UpdateTareas } from './UpdateTareas';



@Component({
  selector: 'app-tareas',
  standalone: true,
  imports: [CommonModule, FormsModule, ListTareas, CreateTareas, UpdateTareas,],
  templateUrl: './tareas.html',
  styleUrl: './tareas.css'
})

export class Tareas {

  @ViewChild(ListTareas) listComponent!: ListTareas;
  @ViewChild(UpdateTareas) updateComponent!: UpdateTareas;

  agregarTareaALista(tarea: Tarea): void {
    this.listComponent.tareas.push(tarea);

  }

  seleccionarTarea(tarea: Tarea) {
    console.log('Tarea seleccionada para editar:', tarea);
    this.updateComponent.cargarTareaParaEditar(tarea);
  }


  refrescarLista() {

    this.listComponent.listTareas();

  }



}





