
export interface Usuario {

  id_usuario?: number;
  nombre: string;
  apellido: string;
  telefono: string;
  email: string;
  contrasena: string;
  id_rol: number;
    
}

export interface UsuarioUpdate {
  
  nombre: string;
  apellido: string;
  telefono: string;
  email: string;
  contrasena?: string;
}
