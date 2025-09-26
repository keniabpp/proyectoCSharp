

import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { inject } from "@angular/core/primitives/di";

import { Observable } from "rxjs/internal/Observable";
import { environment } from "../../../../environments/environment";
import { createTableroDTO, Tablero, TableroUpdate } from "../../models/tablero.model";



@Injectable({ providedIn: 'root' })
export class TablerosService {
    private apiUrl = `${environment.apiUrl}/tableros`;
    private readonly _http = inject(HttpClient);


    getAllTableros(): Observable<Tablero[]> {
        return this._http.get<Tablero[]>(this.apiUrl);

    }


    createTablero(tablero: createTableroDTO): Observable<Tablero> {
        return this._http.post<Tablero>(this.apiUrl, tablero);

    }

    deleteTableroById(id_tablero: number): Observable<void> {
        return this._http.delete<void>(`${this.apiUrl}/${id_tablero}`);

    }

    updateTablero(id_tablero: number, tableroActualizado: TableroUpdate): Observable<void> {
        return this._http.put<void>(`${this.apiUrl}/${id_tablero}`, tableroActualizado);
    }
}