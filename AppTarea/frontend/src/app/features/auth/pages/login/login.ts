import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})

export class Login {
  credentials = {
    email: '',
    contrasena: ''
  };

  errorMessage: string[] = [];

  constructor(private authService: AuthService, private router: Router) { }

  login() {
    this.authService.login(this.credentials).subscribe({
      next: (response) => {
        console.log('Respuesta del login:', response);
        localStorage.setItem('id', response.id_usuario.toString());

        localStorage.setItem('rol', response.rol);
        

        if (response.rol === 'admin') {
          this.router.navigate(['/admin']);
        } else {
          this.router.navigate(['/usuario'])
        }
      },

      error: (err) => {
        if (err.error?.errores?.length) {

          this.errorMessage = err.error.errores.map((e: any) => e.mensaje);
        }
        else if (err.error?.mensaje) {

          this.errorMessage = [err.error.mensaje];
        }
        else {
          this.errorMessage = ['Credenciales Invalidas'];
        }


      }
    });
  }




}
