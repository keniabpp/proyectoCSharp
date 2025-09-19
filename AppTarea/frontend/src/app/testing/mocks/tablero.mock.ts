
// src/testing/mocks/tablero.mock.ts

import { Tablero, createTableroDTO, TableroUpdate } from "../../core/models/tablero.model";

export const mockTablero: Tablero = {
  id_tablero: 1,
  nombre: 'Proyecto Kanban',
  creado_en: new Date('2025-09-01T10:00:00'),
  creado_por: 1,
  nombre_usuario: 'Ana Pérez',
  id_rol: 1,
  nombre_rol: 'Admin'
};

export const mockTableros: Tablero[] = [
  mockTablero,
  {
    id_tablero: 2,
    nombre: 'Desarrollo Web',
    creado_en: new Date('2025-09-02T15:30:00'),
    creado_por: 1,
    nombre_usuario: 'Juan Gómez',
    id_rol: 1,
    nombre_rol: 'Usuario'
  }
];

export const mockCreateTableroDTO: createTableroDTO = {
  nombre: 'Nuevo Proyecto',
  creado_por: 1,
  id_rol: 1
};

export const mockTableroUpdate: TableroUpdate = {
  nombre: 'Proyecto Actualizado'
};
