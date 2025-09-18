import { Component, OnInit, Signal } from '@angular/core';
import { TareasService } from '../../../core/services/tareas.service';
import { Tarea } from '../../../core/models/tarea.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { VencimientoColorPipe } from '../../pipes/fechaVencimiento.pipe';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { nuevaAsignacion, nuevaAsignacionTitulo } from '../../../state/tarea.state';


@Component({
  selector: 'app-tareas-asignadas',
  standalone: true,
  imports: [CommonModule, FormsModule, VencimientoColorPipe, DragDropModule],
  templateUrl: './tareas-asignadas.html',
  styleUrl: './tareas-asignadas.css'
})
export class TareasAsignadas implements OnInit {

  nuevaAsignacion = nuevaAsignacion;
  nuevaAsignacionTitulo = nuevaAsignacionTitulo;




  constructor(private tareasService: TareasService) {}


  cerrarNotificacion() {
    nuevaAsignacion.set(false);
    nuevaAsignacionTitulo.set(null);
  }


  tareas: Tarea[] = [];
  errorMessage: string[] = [];


  ngOnInit(): void {
    this.getTareasByUsuario();

  }


  getTareasByUsuario() {
    this.tareasService.getTareasByUsuario().subscribe({
      next: (tareas) => {
        console.log('Tareas recibidas:', tareas);
        this.tareas = tareas;
      },
      error: (err) => {
        console.error('Error al cargar tareas:', err);
        this.errorMessage = ['No se pudieron cargar las tareas asignadas.'];

      }

    })
  }




}
