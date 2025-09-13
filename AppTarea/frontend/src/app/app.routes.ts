import { Routes } from '@angular/router';
import { Login } from './features/auth/pages/login/login';
import { Register } from './features/auth/pages/register/register';
import { adminRoutes } from './features/admin/admin.routes';



//aqui definimos las rutas de nuestra app cuando hacemos click en algun lado y nos mande a lo que nesecitamos

export const routes: Routes = [
    {path:'login',component: Login },
    {path:'register',component: Register },
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    ...adminRoutes,
    { path: '**', redirectTo: 'login' }
    
    
];
