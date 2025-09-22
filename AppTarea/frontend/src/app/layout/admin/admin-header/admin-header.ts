import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AdminFooter } from '../admin-footer/admin-footer';

@Component({
  selector: 'app-admin-header',
  standalone: true,
  imports: [CommonModule,RouterModule,],
  templateUrl: './admin-header.html',
  styleUrl: './admin-header.css'
})
export class AdminHeader {
  constructor(private router: Router) {}

   logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

}
