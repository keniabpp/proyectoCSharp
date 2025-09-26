import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import Swal from 'sweetalert2';
import { Tarea, TareaUpdate } from '../../../../../core/models/tarea.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TareasService } from '../../../../../core/services/Tarea/tareas.service';

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



    @Input() onTareaActualizada!: () => void;
    @Input() tarea: Tarea | null = null;


    ngOnChanges(changes: SimpleChanges): void {
        if (changes['tarea'] && this.tarea) {
            this.cargarTareaParaEditar(this.tarea);
        }
    }




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
        console.log('Tarea actualizada recibida en UpdateTareas:',);
        this.tareasService.updateTarea(this.idTareaEditando, this.tareaActualizada).subscribe({
            next: (respuesta) => {
                console.log(respuesta);
                if (this.onTareaActualizada) {
                    this.onTareaActualizada();
                }

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
