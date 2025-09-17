import { CommonModule } from "@angular/common";
import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { Tablero } from "../../../../core/models/tablero.model";
import { TablerosService } from "../../../../core/services/tableros.service";
import Swal from "sweetalert2";


@Component({
    selector: 'app-listTableros',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './ListTableros.html',
    styleUrl: './tableros.css'
})

export class ListTableros implements OnInit {
    
    constructor(private tablerosService: TablerosService) { }

    @Output() tableroSeleccionado = new EventEmitter<Tablero>();

    emitirTablero(tablero: Tablero): void {
        this.tableroSeleccionado.emit(tablero);
    }


    tableros: Tablero[] = [];

    errorMessage: string[] = [];


    ngOnInit(): void {
        this.listTableros();
    }

    listTableros() {
        this.tablerosService.getAllTableros().subscribe({
            next: (data) => (this.tableros = data),

            error: (err) => {
                console.error('Error al cargar tableros:', err);
            }
        })
    }
    

    deleteTablero(id_tablero: number) {
        Swal.fire({
            title: '¿Estás seguro?',
            text: 'Esta acción eliminará el tablero permanentemente.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                this.tablerosService.deleteTableroById(id_tablero).subscribe({
                    next: () => {
                        // Actualiza la lista de usuarios
                        this.listTableros();

                        // Muestra mensaje de éxito
                        Swal.fire({
                            title: '¡Eliminado!',
                            text: 'El tablero ha sido eliminado correctamente.',
                            icon: 'success',
                            timer: 2000,
                            showConfirmButton: false
                        });
                    },
                    error: (err) => {
                        // Muestra mensaje de error
                        Swal.fire({
                            title: 'Error',
                            text: 'No se pudo eliminar el tablero. Intenta de nuevo.',
                            icon: 'error'
                        });
                        console.error('Error al eliminar:', err);
                    }
                });
            }
        });
    }
}