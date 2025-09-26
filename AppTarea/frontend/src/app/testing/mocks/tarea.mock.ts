
import { Tarea, createTareaDTO, TareaUpdate, moverTarea } from '../../core/models/tarea.model';

export const mockTarea: Tarea = {
  id_tablero: 1,
  id_columna: 1,
  id_tarea: 9,
  titulo: 'Diseñar ',
  descripcion: 'Crear diseño responsive ',
  detalle: 'httuyiyi',
  creado_en: new Date(),
  fecha_vencimiento: new Date(),
  creado_por: 1,
  nombre_creador: 'Laura ',
  asignado_a: 2,
  nombre_asignado: 'Carlos ',
  nombre_tablero: 'Proyecto ',
  nombre_columna: 'En progreso',
  estado_fechaVencimiento: new Date()
};

export const mockTareas: Tarea[] = [
  mockTarea,
  {
    id_tablero: 1,
    id_columna: 2,
    id_tarea: 10,
    titulo: 'Configurar ',
    descripcion: 'Instalar y configurar PostgreSQL para el proyecto',
    detalle: 'fgjgjkhkjk',
    creado_en: new Date(),
    fecha_vencimiento: new Date(),
    creado_por: 1,
    nombre_creador: 'Laura ',
    asignado_a: 3,
    nombre_asignado: 'Sofía ',
    nombre_tablero: 'Proyecto ',
    nombre_columna: 'Por hacer',
    estado_fechaVencimiento: new Date()
  }
];

export const mockCreateTareaDTO: createTareaDTO = {
  titulo: 'Implementar ',
  descripcion: 'Agregar ',
  creado_por: 1,
  fecha_vencimiento: new Date(),
  asignado_a: 2,
  id_tablero: 1,
  id_columna: 1
};

export const mockTareaUpdate: TareaUpdate = {
  titulo: 'Diseño de login ',
  descripcion: 'Se agregó gfgfhgfhg'
};

export const mockMoverTarea: moverTarea = {
  id_tarea: 10,
  id_columna: 3,
  detalle: 'lo logre'
};
