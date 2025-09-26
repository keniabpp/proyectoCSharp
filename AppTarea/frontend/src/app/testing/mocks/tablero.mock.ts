
import { Tablero, createTableroDTO, TableroUpdate } from "../../core/models/tablero.model";


// Objeto mock que simula un tablero existente con todos sus datos
export const mockTablero: Tablero = {
  id_tablero: 1,
  nombre: 'Proyecto ',
  creado_en: new Date(),
  creado_por: 1,
  nombre_usuario: 'Ana ',
  id_rol: 1,
  nombre_rol: 'Admin'
};

// Array de tableros simulados para pruebas, incluye el tablero mock y otro adicional
export const mockTableros: Tablero[] = [
  mockTablero,
  {
    id_tablero: 2,
    nombre: 'Desarrollo',
    creado_en: new Date(),
    creado_por: 1,
    nombre_usuario: 'Juan',
    id_rol: 1,
    nombre_rol: 'Admin'
  }
];

export const mockCreateTableroDTO: createTableroDTO = {
  nombre: 'Nuevo Proyecto',
  creado_por: 1,
  id_rol: 1
};

export const mockTableroUpdate: TableroUpdate = {
  nombre: 'Actualizar'
};
