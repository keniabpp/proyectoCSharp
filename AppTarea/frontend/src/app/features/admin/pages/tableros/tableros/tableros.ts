import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Tablero } from '../../../../../core/models/tablero.model';
import { TablerosService } from '../../../../../core/services/tableros.service';

@Component({
  selector: 'app-tableros',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './tableros.html',
  styleUrl: './tableros.css'
})
export class Tableros implements OnInit {

  tableros: Tablero[] = [];

  errorMessage: string[] = [];
  
  constructor(private tablerosService: TablerosService) { }

  ngOnInit(): void {
    this.listTableros();
  }

  listTableros() {
    this.tablerosService.getAllTableros().subscribe({
      next: (data) => (this.tableros = data),

      error: (err) => {
        console.error('Error al cargar tableros:', err);
      }
    })
  }




}
