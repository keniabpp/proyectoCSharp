import { CdkDrag } from '@angular/cdk/drag-drop';
import { CommonModule } from '@angular/common';
import { Component, } from '@angular/core';
import { Tablerokanban } from '../tablerokanban/tablerokanban';
import { FormsModule } from '@angular/forms';
import { TareasService } from '../../../core/services/tareas.service';
import { TablerosService } from '../../../core/services/tableros.service';
import { Tablero } from '../../../core/models/tablero.model';
import { Tarea } from '../../../core/models/tarea.model';
import { Tareas } from '../../../features/admin/pages/tareas/tareas';
import { CreateTareas } from '../../../features/admin/pages/tareas/create-tareas/create-tareas';



@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, Tablerokanban, CreateTareas],
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


  cargarTareasDelTablero() {
    this.tareasService.getAllTareas().subscribe(data => {
      this.tareas = data;
      this.tareas = data.filter(t => t.id_tablero === Number(this.tableroSeleccionado));

    });
  }

  agregarTareaDesdeFormulario(tarea: Tarea): void {
    this.tareas.push(tarea);
    this.tareas = [...this.tareas]; 
  }





} 
