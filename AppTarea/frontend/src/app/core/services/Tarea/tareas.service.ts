import { inject, Injectable, signal, WritableSignal } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { createTareaDTO, moverTarea, Tarea, TareaUpdate } from "../../models/tarea.model";
import { nuevaAsignacion, nuevaAsignacionTitulo } from "../../../state/tarea.state";


@Injectable({ providedIn: 'root' })
export class TareasService {
    private apiUrl = `${environment.apiUrl}/tareas`;
    private readonly _http = inject(HttpClient);




    notificarNuevaAsignacion(titulo: string) {
        nuevaAsignacion.set(true);
        nuevaAsignacionTitulo.set(titulo);
    }


    getAllTareas(): Observable<Tarea[]> {
        return this._http.get<Tarea[]>(this.apiUrl);
    }

    createTarea(tarea: createTareaDTO): Observable<Tarea> {
        return this._http.post<Tarea>(this.apiUrl, tarea);

    }

    deleteTareaById(id_tarea: number): Observable<void> {
        return this._http.delete<void>(`${this.apiUrl}/${id_tarea}`);

    }

    updateTarea(id_tarea: number, tareaActualizada: TareaUpdate): Observable<Tarea> {
        return this._http.put<Tarea>(`${this.apiUrl}/${id_tarea}`, tareaActualizada);
    }

    moverTarea(payload: moverTarea): Observable<any> {
        return this._http.put(`${this.apiUrl}/${payload.id_tarea}/moverTarea`, payload);
    }

    getTareasByUsuario(): Observable<Tarea[]> {
        const id_usuario = Number(localStorage.getItem('id'));
        console.log(`${this.apiUrl}tareasAsignadas?id_usuario=${id_usuario}`);

        return this._http.get<Tarea[]>(`${this.apiUrl}/tareasAsignadas?id_usuario=${id_usuario}`);
    }






}