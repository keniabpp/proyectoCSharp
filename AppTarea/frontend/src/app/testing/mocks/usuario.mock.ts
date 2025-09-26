
import { Usuario, UsuarioUpdate } from "../../core/models/usuario.model";

export const mockUsuario: Usuario = {
  id_usuario: 1,
  nombre: 'clara',
  apellido: 'etrt',
  email: 'htht@euuyuyuyu',
  telefono: '1234567890',
  contrasena: '123456',
  id_rol: 1 
};

export const mockUsuarios: Usuario[] = [
  mockUsuario,
  {
    id_usuario: 2,
    nombre: 'uyuyu',
    apellido: 'yuyuy',
    email: 'yiyui@yuyu',
    telefono: '576576',
    contrasena: 'abc123',
    id_rol: 2 
  }
];

export const mockUsuarioUpdate: UsuarioUpdate = {
  nombre: 'fdgfg',
  apellido: 'gfhgh',
  email: 'jgjhj@jgjgj',
  telefono: 'jhjhjh'
  
};
