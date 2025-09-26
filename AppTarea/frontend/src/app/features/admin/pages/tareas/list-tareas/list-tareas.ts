import { Component, Input } from '@angular/core';
import Swal from 'sweetalert2';
import { TareasService } from '../../../../../core/services/tareas.service';
import { Tarea } from '../../../../../core/models/tarea.model';

@Component({
  selector: 'app-list-tareas',
  standalone: true,
  imports: [],
  templateUrl: './list-tareas.html',
  styleUrl: './list-tareas.css'
})
export class ListTareas {

  constructor(private tareasService: TareasService) { }


  errorMessage: string[] = [];


  @Input() tareas: Tarea[] = [];

  @Input() onTareaSeleccionada!: (tarea: Tarea) => void; // "Este componente hijo va a recibir del padre una función (callback) llamada 'onTareaSeleccionada'"
  emitirTarea(tarea: Tarea): void {
    if (this.onTareaSeleccionada) {
      this.onTareaSeleccionada(tarea);
    }
  }



  ngOnInit(): void {
    this.listTareas();




  }

  listTareas() {
    this.tareasService.getAllTareas().subscribe({
      next: (data) => (this.tareas = data),

      error: (err) => {
        console.error('Error al cargar tareas:', err);
      }
    })
  }







  deleteTarea(id_tarea: number) {
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
        this.tareasService.deleteTareaById(id_tarea).subscribe({
          next: () => {
            this.listTareas();

            Swal.fire({
              title: '¡Eliminado!',
              text: 'La Tarea ha sido eliminada correctamente.',
              icon: 'success',
              timer: 2000,
              showConfirmButton: false
            });
          },
          error: (err) => {
            const mensaje = err.error?.mensaje;

            Swal.fire({
              icon: 'warning',
              title: 'Acción no permitida',
              text: mensaje,
              confirmButtonText: 'Entendido',
              confirmButtonColor: '#3085d6'
            });
            console.error('Error al eliminar:', err);
          }
        });
      }
    });
  }

}
