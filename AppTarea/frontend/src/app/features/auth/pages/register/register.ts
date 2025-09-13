import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})

export class Register {
  usuario = {
    nombre:'',
    apellido:'',
    telefono:'',
    email:'',
    contrasena:''
  }
  errorMessage: string[] = [];
  constructor(private authService: AuthService, private router: Router){}

  register(){
    this.authService.register(this.usuario).subscribe({
      next: () => {
        this.router.navigate(['/login']);
      },

      error: (err) =>{
        // console.error(err);
        console.log('Error recibido en Register:', err);

        if (err.error?.errores?.length) {

          this.errorMessage = err.error.errores.map((e: any) => e.mensaje);
        } 
        else if (err.error?.mensaje) {

          this.errorMessage = [err.error.mensaje];
        } 
        else {
          this.errorMessage = ['No se pudo registrar el usuario'];
        }
        
       
      }
    });
  }

}
