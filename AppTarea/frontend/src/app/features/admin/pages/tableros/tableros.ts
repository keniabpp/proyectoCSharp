import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ListTableros } from './ListTableros';
import { UpdateTableros } from './UpdateTableros';
import { CreateTableros } from './CreateTableros';
import { Tablero } from '../../../../core/models/tablero.model';

@Component({
  selector: 'app-tableros',
  standalone: true,
  imports: [CommonModule, FormsModule, ListTableros, UpdateTableros, CreateTableros],
  templateUrl: './tableros.html',
  styleUrl: './tableros.css'
})
export class Tableros {

  @ViewChild(UpdateTableros) updateComponent!: UpdateTableros;
  @ViewChild(ListTableros) listComponent!: ListTableros;

  seleccionarTablero(tablero: Tablero) {
    console.log('Usuario seleccionado para editar:', tablero);
    this.updateComponent.cargarTableroParaEditar(tablero);
  }

  refrescarLista() {

    this.listComponent.listTableros();
  }

  agregarTableroLista(tablero: Tablero): void {
    this.listComponent.tableros.push(tablero);

  }





}
