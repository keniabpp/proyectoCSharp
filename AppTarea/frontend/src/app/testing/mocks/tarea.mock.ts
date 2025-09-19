
import { Tarea, createTareaDTO, TareaUpdate, moverTarea } from '../../core/models/tarea.model';

export const mockTarea: Tarea = {
  id_tablero: 1,
  id_columna: 1,
  id_tarea: 101,
  titulo: 'Diseñar interfaz de login',
  descripcion: 'Crear diseño responsive para la pantalla de inicio de sesión',
  detalle: 'Debe incluir validación de campos y compatibilidad móvil',
  creado_en: new Date('2025-09-10T09:00:00'),
  fecha_vencimiento: new Date('2025-09-20T18:00:00'),
  creado_por: 1,
  nombre_creador: 'Laura Martínez',
  asignado_a: 2,
  nombre_asignado: 'Carlos Ruiz',
  nombre_tablero: 'Proyecto Kanban',
  nombre_columna: 'En progreso',
  estado_fechaVencimiento: new Date('2025-09-20T18:00:00')
};

export const mockTareas: Tarea[] = [
  mockTarea,
  {
    id_tablero: 1,
    id_columna: 2,
    id_tarea: 102,
    titulo: 'Configurar base de datos',
    descripcion: 'Instalar y configurar PostgreSQL para el proyecto',
    detalle: 'Debe incluir usuarios, roles y backups automáticos',
    creado_en: new Date('2025-09-11T10:30:00'),
    fecha_vencimiento: new Date('2025-09-22T17:00:00'),
    creado_por: 1,
    nombre_creador: 'Laura Martínez',
    asignado_a: 3,
    nombre_asignado: 'Sofía Gómez',
    nombre_tablero: 'Proyecto Kanban',
    nombre_columna: 'Por hacer',
    estado_fechaVencimiento: new Date('2025-09-22T17:00:00')
  }
];

export const mockCreateTareaDTO: createTareaDTO = {
  titulo: 'Implementar autenticación',
  descripcion: 'Agregar login con JWT y refresh token',
  creado_por: 1,
  fecha_vencimiento: new Date('2025-09-25T23:59:00'),
  asignado_a: 2,
  id_tablero: 1,
  id_columna: 1
};

export const mockTareaUpdate: TareaUpdate = {
  titulo: 'Diseño de login actualizado',
  descripcion: 'Se agregó validación con expresiones regulares'
};

export const mockMoverTarea: moverTarea = {
  id_tarea: 101,
  id_columna: 3,
  detalle: 'Se movió a la columna "Finalizado" tras revisión'
};
