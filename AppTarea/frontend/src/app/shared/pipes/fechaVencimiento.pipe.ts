import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'vencimientoColor',
  standalone: true
})
export class VencimientoColorPipe implements PipeTransform {
  transform(estado_fechaVencimiento: string): string {
    switch (estado_fechaVencimiento.toLowerCase()) {
      case 'tarea vencida':
        return 'red';
      case 'dentro del plazo':
        return 'orange';
      case 'completada':
        return 'green';
      default:
        return 'inherit';
    }
  }
}
