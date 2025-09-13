import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class AdminGuard implements CanActivate {

  constructor(private router: Router) {}

  canActivate(): boolean {
    const token = localStorage.getItem('token');
    
    if (token) {
      
      const payload = JSON.parse(atob(token.split('.')[1]));

      // Verificar si el rol es admin
      if (payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'admin') {
        return true;
      }
    }

    // Si no es admin o no hay token, redirigir al login
    this.router.navigate(['/login']);
    return false;
  }
}
