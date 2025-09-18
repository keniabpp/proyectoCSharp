import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'columnaColor',
  standalone: true
})
export class ColumnaColorPipe implements PipeTransform {
  transform(id_columna: number): string {
    switch (id_columna) {
      case 1: return '#ff9d00ff'; // Por hacer → naranja
      case 2: return '#3498db'; // En progreso → azul
      case 3: return '#2ecc71'; // Hecho → verde
      default: return '#7f8c8d'; // Gris por defecto
    }
  }
}
