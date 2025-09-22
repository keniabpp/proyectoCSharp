import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { createTareaDTO, Tarea, TareaUpdate } from '../../../../core/models/tarea.model';
import { TareasService } from '../../../../core/services/tareas.service';
import Swal from 'sweetalert2';
import { ListTareas } from './list-tareas/list-tareas';
import { UpdateTareas } from './update-tareas/update-tareas';
import { CreateTareas } from './create-tareas/create-tareas';

@Component({
  selector: 'app-tareas',
  standalone: true,
  imports: [CommonModule, FormsModule, ListTareas, CreateTareas, UpdateTareas],
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

  cargarTareaParaEditar(tarea: Tarea) {
    this.updateComponent.cargarTareaParaEditar(tarea);
  }

  abrirModalEdicion(): void {
  const modalElement = document.getElementById('editarTareaModal');
  if (modalElement) {
    const modal = new (window as any).bootstrap.Modal(modalElement);
    modal.show();
  }
}







}





