import { CommonModule } from '@angular/common';
import { Component, ViewChild, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Tarea, } from '../../../../core/models/tarea.model';
import { ListTareas } from './list-tareas/list-tareas';
import { UpdateTareas } from './update-tareas/update-tareas';
import { CreateTareas } from './create-tareas/create-tareas';
import { TareasService } from '../../../../core/services/tareas.service';

@Component({
  selector: 'app-tareas',
  standalone: true,
  imports: [CommonModule, FormsModule, ListTareas, CreateTareas, UpdateTareas],
  templateUrl: './tareas.html',

})

export class Tareas {

  constructor(private tareasService: TareasService) { }

  tareasSignal = signal<Tarea[]>([]);
  tareaSeleccionadaSignal = signal<Tarea | null>(null);


  ngOnInit(): void {
    this.refrescarLista();
  }

  // Cargar todas las tareas y actualizar el signal
  refrescarLista(): void {
    this.tareasService.getAllTareas().subscribe({
      next: (tareas) => this.tareasSignal.set(tareas),
      error: (err) => console.error('Error al cargar tareas:', err),
    });
  }


  agregarTareaALista(tarea: Tarea): void {
    const tareasActuales = this.tareasSignal();
    this.tareasSignal.set([...tareasActuales, tarea]);
  }

  seleccionarTarea(tarea: Tarea): void {
    console.log('Tarea seleccionada para editar:', tarea);
    this.tareaSeleccionadaSignal.set(tarea);
  }


















}





