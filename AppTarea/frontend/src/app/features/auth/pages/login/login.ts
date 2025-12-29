import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../../core/services/Auth/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html',
 
})

export class Login {
  credentials = {
    Email: '',
    Contrasena: ''
  };

  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  login() {
    this.errorMessage = '';
    // console.log('Enviando credenciales:', this.credentials);
    this.authService.login(this.credentials).subscribe({
      next: (response) => {
        // console.log('Respuesta del backend:', response);
        // Adaptar a minúsculas o mayúsculas según la respuesta
        const rol = response.rol || response.Rol;
        localStorage.setItem('id', response.id_usuario?.toString() || '');
        localStorage.setItem('rol', rol);
        if (rol?.toLowerCase() === 'admin') {
          this.router.navigate(['/admin']);
        } else {
          this.router.navigate(['/usuario']);
        }
      },
      error: (err) => {
        console.error('Error en login:', err);
        if (err.status === 401 && err.error?.message) {
          this.errorMessage = err.error.message;
        } else if (err.error?.mensaje) {
          this.errorMessage = err.error.mensaje;
        } else if (err.error?.errores?.length) {
          this.errorMessage = err.error.errores.map((e: any) => e.mensaje).join(' ');
        } else if (err.status === 403) {
          this.errorMessage = 'Cuenta bloqueada. Contacte al administrador.';
        } else {
          this.errorMessage = 'Credenciales inválidas o error de red.';
        }
      }
    });
  }




}
