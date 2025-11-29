import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-usuario-header',
  imports: [CommonModule,RouterModule],
  templateUrl: './usuario-header.html',
  styleUrl: './usuario-header.css'
})
export class UsuarioHeader {
  constructor(private router: Router) {}

   logout() {
    this.router.navigate(['/login']);
  }

}
