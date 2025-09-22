
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AdminHeader } from './admin-header/admin-header';
import { AdminFooter } from './admin-footer/admin-footer';



@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule,RouterModule,AdminHeader,AdminFooter],
  templateUrl: 'admin.html',
  styleUrl: 'admin.css'
})

export class Admin {

}
