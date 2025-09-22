import { Component, EventEmitter, Output } from '@angular/core';
import Swal from 'sweetalert2';
import { Tarea, TareaUpdate } from '../../../../../core/models/tarea.model';
import { TareasService } from '../../../../../core/services/tareas.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-update-tareas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './updateTareas.html',
  styleUrl: './update-tareas.css'
})
export class UpdateTareas {

    constructor(private tareasService: TareasService) { }

    tareas: Tarea[] = [];
    errorMessage: string[] = [];


    @Output() tareaActualizadaEvent = new EventEmitter<Tarea>();



    tareaActualizada: TareaUpdate = {
        titulo: '',
        descripcion: '',

    }


    idTareaEditando: number = 0;


    cargarTareaParaEditar(tarea: Tarea) {
        this.tareaActualizada = {
            titulo: tarea.titulo,
            descripcion: tarea.descripcion,


        };
        this.idTareaEditando = tarea.id_tarea!;
    }




    updateTarea(): void {
        console.log('ID que se va a actualizar:', this.idTareaEditando);
        this.tareasService.updateTarea(this.idTareaEditando, this.tareaActualizada).subscribe({
            next: (respuesta) => {
                console.log(respuesta);
                this.tareaActualizadaEvent.emit(respuesta);
            },
            error: (err) => {
                console.error('Error al actualizar tarea:', err);

                let mensaje = 'No se pudo actualizar la tarea';

                if (err.error?.errores?.length) {
                    mensaje = err.error.errores.map((e: any) => e.mensaje).join('\n');
                } else if (err.error?.mensaje) {
                    mensaje = err.error.mensaje;
                }

                Swal.fire({
                    icon: 'error',
                    title: 'Error al actualizar',
                    text: mensaje,
                    confirmButtonText: 'Entendido',
                    confirmButtonColor: '#d33'
                });
            }

        });
    }


}
