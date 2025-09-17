
export interface Tarea {
    id_tablero: number;
    id_columna: number;
    id_tarea: number;
    titulo: string;
    descripcion: string;
    detalle: string;
    creado_en: Date;
    fecha_vencimiento: Date;
    creado_por: number;
    nombre_creador: string;
    asignado_a: number;
    nombre_asignado: string;
    nombre_tablero: string;
    nombre_columna: string;
    estado_fechaVencimiento: Date;

}

export interface createTareaDTO {
    titulo: string;
    descripcion: string;
    creado_por: number;
    fecha_vencimiento: Date;
    asignado_a: number;
    id_tablero: number;
    id_columna: number;
}

export interface TareaUpdate {
    titulo: string;
    descripcion: string;
}

export interface moverTarea {
    id_tarea: number;
    id_columna: number;
    detalle: string;

}