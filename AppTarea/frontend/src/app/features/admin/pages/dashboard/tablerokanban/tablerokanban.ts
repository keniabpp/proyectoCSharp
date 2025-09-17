import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { moverTarea, Tarea } from '../../../../../core/models/tarea.model';
import { Columna } from '../../../../../core/models/columna.model';
import { CommonModule } from '@angular/common';
import { TablerosService } from '../../../../../core/services/tableros.service';
import { Tablero } from '../../../../../core/models/tablero.model';
import { FormsModule } from '@angular/forms';
import { DragDropModule, CdkDragDrop } from '@angular/cdk/drag-drop';
import { TareasService } from '../../../../../core/services/tareas.service';
import Swal from 'sweetalert2';
import { UpdateTareas } from '../../tareas/UpdateTareas';

@Component({
  selector: 'app-tablerokanban',
  standalone: true,
  imports: [CommonModule, FormsModule, DragDropModule, UpdateTareas],
  templateUrl: './tablerokanban.html',
  styleUrl: './tablerokanban.css'
})
export class Tablerokanban implements OnInit {
  constructor(
    private tablerosService: TablerosService,
    private tareasService: TareasService
  ) { }

  tableros: Tablero[] = [];

  @Input() tareas: Tarea[] = [];
  @Input() id_tablero: number = 0;

  columnas: Columna[] = [
    { id_columna: 1, nombre: 'Por hacer', posicion: 1 },
    { id_columna: 2, nombre: 'En Progreso', posicion: 2 },
    { id_columna: 3, nombre: 'Hecho', posicion: 3 }
  ];

  connectedDropLists: string[] = [];

  ngOnInit(): void {
    this.connectedDropLists = this.columnas.map(c => `columna-${c.id_columna}`);
  }

  obtenerTareas(id_columna: number): Tarea[] {
    return this.tareas.filter(t => t.id_columna === id_columna);
  }

  moverTarea(event: CdkDragDrop<Tarea[]>, nuevaColumnaId: number): void {
    const tareaMovida = event.item.data as Tarea;

    if (tareaMovida.id_columna === nuevaColumnaId) return;

    const payload: moverTarea = {
      id_tarea: tareaMovida.id_tarea!,
      id_columna: nuevaColumnaId,
      detalle: tareaMovida.detalle ?? ''
    };

    this.tareasService.moverTarea(payload).subscribe({
      next: () => {
        // Actualiza el campo localmente
        tareaMovida.id_columna = nuevaColumnaId;

        // Elimina la tarea de su columna anterior
        this.tareas = this.tareas.filter(t => t.id_tarea !== tareaMovida.id_tarea);

        // Reinsertar la tarea actualizada
        this.tareas.push(tareaMovida);

        // Fuerza el refresco visual
        this.tareas = [...this.tareas];

        console.log('✅ Tarea movida con éxito');
      },
      error: (err) => {
        const mensaje = err.error?.mensaje || 'No se pudo mover la tarea';

        Swal.fire({
          icon: 'warning',
          title: 'Acción no permitida',
          text: mensaje,
          confirmButtonText: 'Entendido',
          confirmButtonColor: '#3085d6'
        });

        console.error('❌ Error al mover tarea:', err);
      }
    });
  }

  @ViewChild('modalTarea') modalTarea!: UpdateTareas;


  editarTarea(tarea: Tarea): void {
    this.modalTarea.cargarTareaParaEditar(tarea); // ✅ carga el ID correctamente

    const modalElement = document.getElementById('editarTareaModal');
    if (modalElement) {
      const modal = new (window as any).bootstrap.Modal(modalElement);
      modal.show();
    }
  }


  refrescarTarea(tareaActualizada: Tarea): void {
    const index = this.tareas.findIndex(t => t.id_tarea === tareaActualizada.id_tarea);
    if (index !== -1) {
      this.tareas[index] = tareaActualizada;
      this.tareas = [...this.tareas]; // 🔁 fuerza el refresco visual
    }
  }



  eliminarTarea(tarea: Tarea): void {
    Swal.fire({
      title: '¿Estás seguro?',
      text: 'Esta acción eliminará la tarea permanentemente.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.tareasService.deleteTareaById(tarea.id_tarea!).subscribe({
          next: () => {
            this.tareas = this.tareas.filter(t => t.id_tarea !== tarea.id_tarea);
            this.tareas = [...this.tareas]; // fuerza el refresco

            Swal.fire({
              title: '¡Eliminado!',
              text: 'La tarea ha sido eliminada correctamente.',
              icon: 'success',
              timer: 2000,
              showConfirmButton: false
            });
          },
          error: (err) => {
        const mensaje = err.error?.mensaje || 'No se pudo mover la tarea';

        Swal.fire({
          icon: 'warning',
          title: 'Acción no permitida',
          text: mensaje,
          confirmButtonText: 'Entendido',
          confirmButtonColor: '#3085d6'
        });

        console.error('❌ Error al mover tarea:', err);
      }
        });
      }
    });
  }

}
