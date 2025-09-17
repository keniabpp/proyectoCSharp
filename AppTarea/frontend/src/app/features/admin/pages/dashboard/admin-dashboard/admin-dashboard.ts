import { CdkDrag } from '@angular/cdk/drag-drop';
import { CommonModule } from '@angular/common';
import { Component, } from '@angular/core';
import { Columna } from '../../../../../core/models/columna.model';
import { TareasService } from '../../../../../core/services/tareas.service';
import { Tarea } from '../../../../../core/models/tarea.model';
import { Tablerokanban } from '../tablerokanban/tablerokanban';
import { TablerosService } from '../../../../../core/services/tableros.service';
import { Tablero } from '../../../../../core/models/tablero.model';
import { FormsModule } from '@angular/forms';
import { UpdateTareas } from '../../tareas/UpdateTareas';
import { CreateTareas } from '../../tareas/CreateTareas';


@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, Tablerokanban, FormsModule, CreateTareas],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css'
})

export class AdminDashboard {

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
      console.log('Todas las tareas:', data); // 👈 Verifica que llegan
      console.log('Tablero seleccionado:', this.tableroSeleccionado);
      this.tareas = data.filter(t => t.id_tablero === Number(this.tableroSeleccionado));
      console.log('Tareas filtradas:', this.tareas);
    });
  }

  agregarTareaDesdeFormulario(tarea: Tarea): void {
    this.tareas.push(tarea);
    this.tareas = [...this.tareas]; // 🔁 fuerza el refresco visual
  }





} 
