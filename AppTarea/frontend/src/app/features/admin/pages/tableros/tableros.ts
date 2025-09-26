import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { createTableroDTO, Tablero, TableroUpdate } from '../../../../core/models/tablero.model';

import Swal from 'sweetalert2';
import { TablerosService } from '../../../../core/services/Tableros/tableros.service';

@Component({
  selector: 'app-tableros',
  standalone: true,
  imports: [CommonModule, FormsModule,],
  templateUrl: './tableros.html',
  
})
export class Tableros {


  constructor(private tablerosService: TablerosService) { }



  tableros: Tablero[] = [];

  errorMessage: string[] = [];


  ngOnInit(): void {
    this.listTableros();
  }

  listTableros() {
    this.tablerosService.getAllTableros().subscribe({
      next: (data) => (this.tableros = data),

      error: (err) => {
        // console.error('Error al cargar tableros:', err);
      }
    })
  }


  nuevoTablero: createTableroDTO = {
    nombre: '',
    creado_por: 1,
    id_rol: 1,

  };

  addTablero(): void {
    this.tablerosService.createTablero(this.nuevoTablero).subscribe({
      next: (tableroCreado) => {
        this.listTableros();
        this.nuevoTablero = {

          nombre: '',
          creado_por: 0,
          id_rol: 1
        };

      },

      error: (err) => {
        // console.error('Error al cargar tableros:', err);

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


  tableroActualizado: TableroUpdate = {
    nombre: '',

  }

  idTableroEditando: number = 0;

  cargarTableroParaEditar(tablero: Tablero) {
    this.tableroActualizado = {
      nombre: tablero.nombre,

    };
    this.idTableroEditando = tablero.id_tablero!;
  }

  updateTablero(): void {
    this.tablerosService.updateTablero(this.idTableroEditando, this.tableroActualizado).subscribe({
      next: (respuesta) => {
        this.listTableros();

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
            this.listTableros();

            Swal.fire({
              title: '¡Eliminado!',
              text: 'El tablero ha sido eliminado correctamente.',
              icon: 'success',
              timer: 2000,
              showConfirmButton: false
            });
          },
          error: (err) => {

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
