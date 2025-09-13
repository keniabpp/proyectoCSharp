import { Routes } from '@angular/router';
import { AdminLayout } from './layout/admin-layout/admin-layout';
import { AdminGuard } from '../../core/guards/admin-guard';
import { AdminDashboard } from './pages/dashboard/admin-dashboard/admin-dashboard';
import { Usuarios } from './pages/usuarios/usuarios';
import { Tableros } from './pages/tableros/tableros/tableros';

export const adminRoutes: Routes = [
  {
    path: 'admin',
    component: AdminLayout,
    canActivate: [AdminGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: AdminDashboard },
      { path: 'usuarios', component: Usuarios },
      { path: 'tableros', component: Tableros },
      // { path: 'tareas', component: Tareas },
      
      
    ]
  }
];
