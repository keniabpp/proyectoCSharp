import { Routes } from '@angular/router';
// import { AdminLayout } from './layout/admin-layout/admin-layout';
import { AdminGuard } from '../../core/guards/admin-guard';

import { Usuarios } from './pages/usuarios/usuarios';
import { Tableros } from './pages/tableros/tableros';
import { Admin, } from './admin';
import { Tareas } from './pages/tareas/tareas';
import { Perfil } from '../../shared/components/perfil/perfil';
import { TareasAsignadas } from '../../shared/components/tareas-asignadas/tareas-asignadas';
import { Dashboard } from '../../shared/components/dashboard/dashboard';

export const adminRoutes: Routes = [
  {
    path: 'admin',
    component: Admin,
    canActivate: [AdminGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: Dashboard },
      { path: 'usuarios', component: Usuarios },
      { path: 'tableros', component: Tableros },
      { path: 'tareas', component: Tareas },
      { path: 'perfil', component: Perfil },
      { path: 'tareas-asignadas', component: TareasAsignadas },
      
      
    ]
  }
];
