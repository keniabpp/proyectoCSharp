import { CommonModule } from "@angular/common";
import { Component, EventEmitter, Output } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TablerosService } from "../../../../core/services/tableros.service";
import { createTableroDTO, Tablero } from "../../../../core/models/tablero.model";


@Component({
    selector: 'app-createTableros',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './CreateTableros.html',
    styleUrl: './tableros.css'
})

export class CreateTableros {

     @Output() tableroCreadoEvent = new EventEmitter<Tablero>();
    tableros: Tablero[] = [];

    errorMessage: string[] = [];
    
    constructor(private tablerosService: TablerosService) { }


    nuevoTablero: createTableroDTO = {
        nombre: '',
        creado_por: 1,
        id_rol: 1,
    
      };
    
      addTablero(): void {
        this.tablerosService.createTablero(this.nuevoTablero).subscribe({
          next: (tableroCreado) => {
            this.tableros.push(tableroCreado);
            this.tableroCreadoEvent.emit(tableroCreado);
            this.nuevoTablero = {
    
              nombre: '',
              creado_por: 0,
              id_rol: 1
            };
    
          },
    
          error: (err) => {
            console.error('Error al cargar tableros:', err);
    
            if (err.error?.errores?.length) {
    
              this.errorMessage = err.error.errores.map((e: any) => e.mensaje);
            }
            else if (err.error?.mensaje) {
    
              this.errorMessage = [err.error.mensaje];
            }
            else {
              this.errorMessage = ['No se pudo registrar el tablero'];
            }
          }
    
    
        })
      }
}