import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UsuarioHeader } from '../../layout/usuario/usuario-header/usuario-header';
import { UsuarioFooter } from '../../layout/usuario/usuario-footer/usuario-footer';

@Component({
  selector: 'app-usuario',
  standalone: true,
  imports: [CommonModule,RouterModule, UsuarioHeader, UsuarioFooter],
  templateUrl: './usuario.html',
  styleUrl: './usuario.css'
})
export class Usuario {

}
