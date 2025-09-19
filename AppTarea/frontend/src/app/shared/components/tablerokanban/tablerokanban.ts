import { Component, Input, OnInit, ViewChild } from '@angular/core';

import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { DragDropModule, CdkDragDrop } from '@angular/cdk/drag-drop';

import Swal from 'sweetalert2';
import { UpdateTareas } from '../../../features/admin/pages/tareas/UpdateTareas';
import { TablerosService } from '../../../core/services/tableros.service';
import { TareasService } from '../../../core/services/tareas.service';
import { Tablero } from '../../../core/models/tablero.model';
import { moverTarea, Tarea } from '../../../core/models/tarea.model';
import { Columna } from '../../../core/models/columna.model';
import { ColumnaColorPipe } from '../../pipes/columnaColor.pipe';



@Component({
  selector: 'app-tablerokanban',
  standalone: true,
  imports: [CommonModule, FormsModule, DragDropModule, UpdateTareas, ColumnaColorPipe ],
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
        
        tareaMovida.id_columna = nuevaColumnaId;
        this.tareas = this.tareas.filter(t => t.id_tarea !== tareaMovida.id_tarea);
        this.tareas.push(tareaMovida);
        this.tareas = [...this.tareas];

        
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

        
      }
    });
  }

  @ViewChild('modalTarea') modalTarea!: UpdateTareas;


  editarTarea(tarea: Tarea): void {
    this.modalTarea.cargarTareaParaEditar(tarea); 

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
      this.tareas = [...this.tareas]; 
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
            this.tareas = [...this.tareas]; 

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

            
          }
        });
      }
    });
  }

}
