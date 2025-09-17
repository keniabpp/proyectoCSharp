// import { CommonModule } from "@angular/common";
// import { Component, EventEmitter, Output } from "@angular/core";
// import { FormsModule } from "@angular/forms";
// import { TareasService } from "../../../../core/services/tareas.service";
// import { moverTarea, Tarea } from "../../../../core/models/tarea.model";


// @Component({
//     selector: "app-moverTareas",
//     standalone: true,
//     imports: [CommonModule, FormsModule],
//     templateUrl: "./Tareareas.html",
//     styleUrl: "./tareas.css",
// })
// export class MoverTareas {

//     errorMessage: string[] = [];
//     @Output() tareaMovidaEvent = new EventEmitter<void>();

//     constructor(private tareasService: TareasService) { }

    
//     moverPayload: moverTarea = {
//         id_tarea: 0,
//         id_columna: 0,
//         detalle: "",
//     };

//     moverTarea(): void {
//         console.log("Moviendo tarea:", this.moverPayload);

//         this.tareasService.moverTarea(this.moverPayload).subscribe({
//             next: (respuesta) => {
//                 console.log(respuesta);
//                 this.tareaMovidaEvent.emit(); 
//             },
//             error: (err) => {
//                 console.error("Error al mover tarea:", err);

//                 if (err.error?.errores?.length) {
//                     this.errorMessage = err.error.errores.map((e: any) => e.mensaje);
//                 } else if (err.error?.mensaje) {
//                     this.errorMessage = [err.error.mensaje];
//                 } else {
//                     this.errorMessage = ["No se pudo mover la tarea"];
//                 }
//             },
//         });
//     }

//     // método para precargar datos de la tarea a mover
//     cargarTareaParaMover(tarea: Tarea, id_columnaDestino: number) {
//         this.moverPayload = {
//             id_tarea: tarea.id_tarea!,
//             id_columna: id_columnaDestino,
//             detalle: tarea.detalle ?? "",
//         };
//     }
// }
