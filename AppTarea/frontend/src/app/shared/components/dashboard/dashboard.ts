import { CdkDrag } from '@angular/cdk/drag-drop';
import { CommonModule } from '@angular/common';
import { Component, } from '@angular/core';
import { Tablerokanban } from '../tablerokanban/tablerokanban';
import { FormsModule } from '@angular/forms';
import { CreateTareas } from '../../../features/admin/pages/tareas/CreateTareas';
import { TareasService } from '../../../core/services/tareas.service';
import { TablerosService } from '../../../core/services/tableros.service';
import { Tablero } from '../../../core/models/tablero.model';
import { Tarea } from '../../../core/models/tarea.model';



@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, CreateTareas, Tablerokanban],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})

export class Dashboard {

  constructor(
    private tareasService: TareasService,
    private tablerosService: TablerosService
  ) { }

  tableros: Tablero[] = [];

  tableroSeleccionado: number = 0;


  tareas: Tarea[] = [];

  ngOnInit() {
    this.tablerosService.getAllTableros().subscribe(data => {
      this.tableros = data;
      if (data.length > 0) {
        this.tableroSeleccionado = data[0].id_tablero;
        this.cargarTareasDelTablero();
      }
    });
  }

  cargarTareasDelTablero() {
    this.tareasService.getAllTareas().subscribe(data => {
      this.tareas = data;
      console.log('Todas las tareas:', data); 
      console.log('Tablero seleccionado:', this.tableroSeleccionado);
      this.tareas = data.filter(t => t.id_tablero === Number(this.tableroSeleccionado));
      console.log('Tareas filtradas:', this.tareas);
    });
  }

  agregarTareaDesdeFormulario(tarea: Tarea): void {
    this.tareas.push(tarea);
    this.tareas = [...this.tareas]; 
  }





} 
