import { CommonModule } from "@angular/common";
import { Component, EventEmitter, Output } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TareasService } from "../../../../core/services/tareas.service";
import { Tarea, TareaUpdate } from "../../../../core/models/tarea.model";



@Component({
    selector: 'app-updateTareas',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './UpdateTareas.html',
    styleUrl: './tareas.css'
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
                console.error('Error al actualizar usuario:', err);

                if (err.error?.errores?.length) {
                    this.errorMessage = err.error.errores.map((e: any) => e.mensaje);
                }
                else if (err.error?.mensaje) {
                    this.errorMessage = [err.error.mensaje];
                }
                else {
                    this.errorMessage = ['No se pudo actualizar el usuario'];
                }
            }
        });
    }




    
}