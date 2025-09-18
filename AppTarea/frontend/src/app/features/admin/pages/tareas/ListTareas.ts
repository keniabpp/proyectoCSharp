import { CommonModule } from "@angular/common";
import { Component, EventEmitter, Output } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TareasService } from "../../../../core/services/tareas.service";
import { Tarea } from "../../../../core/models/tarea.model";
import Swal from "sweetalert2";



@Component({
    selector: 'app-listTareas',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './ListTareas.html',
    styleUrl: './tareas.css'
})

export class ListTareas {

    constructor(private tareasService: TareasService) { }

    tareas: Tarea[] = [];
    errorMessage: string[] = [];

    

    @Output() tareaSeleccionada = new EventEmitter<Tarea>();

    emitirTarea(tarea: Tarea): void {
        this.tareaSeleccionada.emit(tarea);
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
                        
                        Swal.fire({
                            title: 'Error',
                            text: 'No se pudo eliminar la tarea. Intenta de nuevo.',
                            icon: 'error'
                        });
                        console.error('Error al eliminar:', err);
                    }
                });
            }
        });
    }

}