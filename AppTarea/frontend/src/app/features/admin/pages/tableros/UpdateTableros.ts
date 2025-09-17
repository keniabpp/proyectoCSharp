import { CommonModule } from "@angular/common";
import { Component, EventEmitter, Output } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TablerosService } from "../../../../core/services/tableros.service";
import { Tablero, TableroUpdate } from "../../../../core/models/tablero.model";


@Component({
  selector: 'app-updateTableros',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './UpdateTableros.html',
  styleUrl: './tableros.css'
})

export class UpdateTableros {

    constructor(private tablerosService: TablerosService) { }

    @Output() tableroActualizadoEvent = new EventEmitter<void>();

    tableros: Tablero[] = [];
    
    errorMessage: string[] = [];


    tableroActualizado: TableroUpdate = {
    nombre: '',

  }

  idTableroEditando: number = 0;

  updateTablero(): void {
    this.tablerosService.updateTablero(this.idTableroEditando, this.tableroActualizado).subscribe({
      next: (respuesta) => {
        console.log(respuesta);
        this.tableroActualizadoEvent.emit(); // refresca la lista
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
          this.errorMessage = ['No se pudo actualizar el tablero'];
        }
      }
    });
  }

  cargarTableroParaEditar(tablero: Tablero) {
    this.tableroActualizado = {
      nombre: tablero.nombre,
      
    };
    this.idTableroEditando = tablero.id_tablero!;
  }
    
}