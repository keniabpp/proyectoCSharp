import { Routes } from "@angular/router";

import { UsuarioGuard } from "../../core/guards/usuario-guard";
import { Dashboard } from "../../shared/components/dashboard/dashboard";
import { Perfil } from "../../shared/components/perfil/perfil";
import { TareasAsignadas } from "../../shared/components/tareas-asignadas/tareas-asignadas";
import { Usuarios } from "../admin/pages/usuarios/usuarios";
import { Usuario } from "../../layout/usuario/usuario";




export const UsuarioRoutes: Routes = [
  {
    path: 'usuario',
    component: Usuario,
    canActivate: [UsuarioGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: Dashboard },
      { path: 'perfil', component: Perfil },
      { path: 'tareas-asignadas', component: TareasAsignadas },
      
      
     
    ]
  }

]