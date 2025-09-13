import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-admin-footer',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './admin-footer.html',
  styleUrl: './admin-footer.css'
})

export class AdminFooter {

}
