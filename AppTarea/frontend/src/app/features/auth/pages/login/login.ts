import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})

export class Login {
  credentials ={
    email:'',
    contrasena:''
  };

  errorMessage ='';

  constructor(private authService: AuthService, private router: Router) {}

  login(){
    this.authService.login(this.credentials).subscribe({
      next: (response) => {
        localStorage.setItem('token', response.token);
        
        if (response.rol === 'admin') {
          this.router.navigate(['/admin']);
        } else {
          this.router.navigate(['/usuario'])
        }
      },

      error: (err) => {
        console.error(err);
        this.errorMessage = 'Credaenciales invalidas';
      }
    });
  }




}
