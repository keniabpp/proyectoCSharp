import { Component, Input } from '@angular/core';
import { Tablero } from '../../../../../core/models/tablero.model';
import { Columna } from '../../../../../core/models/columna.model';
import { createTareaDTO, Tarea } from '../../../../../core/models/tarea.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Usuario } from '../../../../../core/models/usuario.model';
import { UsuariosService } from '../../../../../core/services/Usuario/usuarios.service';
import { TablerosService } from '../../../../../core/services/Tableros/tableros.service';
import { TareasService } from '../../../../../core/services/Tarea/tareas.service';

@Component({
  selector: 'app-create-tareas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './create-tareas.html',
  styleUrl: './create-tareas.css'
})
export class CreateTareas {


  constructor(
    private usuariosService: UsuariosService,
    private tablerosService: TablerosService,
    private tareasService: TareasService
  ) { }

  

  @Input() onTareaCreada!: (tarea: Tarea) => void;

  errorMessage: string[] = [];


  usuarios: Usuario[] = [];
  // tareas: Tarea[] = [];
  tableros: Tablero[] = [];

  columnas: Columna[] = [
    { id_columna: 1, nombre: 'Por hacer', posicion: 1 },
    // { id_columna: 2, nombre: 'En Progreso', posicion: 2 },
    // { id_columna: 3, nombre: 'hecho', posicion: 3 }
  ];

  ngOnInit(): void {
    this.listUsuarios();
    this.listTableros();
  }

  listUsuarios() {
    this.usuariosService.getAllUsuarios().subscribe({
      next: (data) => (this.usuarios = data),

      error: (err) => {
        // console.error('Error al cargar usuarios:', err);

      }
    })
  }


  listTableros() {
    this.tablerosService.getAllTableros().subscribe({
      next: (data) => (this.tableros = data),

      error: (err) => {
        // console.error('Error al cargar tableros:', err);
      }
    })
  }

  nuevaTarea: createTareaDTO = {
    titulo: '',
    descripcion: '',
    creado_por: 0,
    fecha_vencimiento: new Date(),
    asignado_a: 0,
    id_tablero: 0,
    id_columna: 1,


  };

  addTarea(): void {
    this.tareasService.createTarea(this.nuevaTarea).subscribe({
      next: (tareaCreada) => {
        
        if (this.onTareaCreada){
          this.onTareaCreada(tareaCreada);
        }
        this.tareasService.notificarNuevaAsignacion(tareaCreada.titulo);
        this.nuevaTarea = {

          titulo: '',
          descripcion: '',
          creado_por: 0,
          fecha_vencimiento: new Date,
          asignado_a: 0,
          id_tablero: 0,
          id_columna: 1,

        };

      },

      error: (err) => {
        // console.error('Error al cargar tareas:', err);

        if (err.error?.errores?.length) {

          this.errorMessage = err.error.errores.map((e: any) => e.mensaje);
        }
        else if (err.error?.mensaje) {

          this.errorMessage = [err.error.mensaje];
        }
        else {
          this.errorMessage = ['No se pudo registrar la tarea'];
        }
      }


    })
  }

}
