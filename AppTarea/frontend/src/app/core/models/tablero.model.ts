
export interface Tablero {

  id_tablero: number;
  nombre: string;
  creado_en: Date;
  creado_por: number;
  nombre_usuario: string;
  id_rol: number;
  nombre_rol: string;
}

export interface createTableroDTO {
  nombre: string;
  creado_por: number;
  id_rol: number;
}

export interface TableroUpdate {


  nombre: string;

}