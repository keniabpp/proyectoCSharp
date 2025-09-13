
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { inject } from "@angular/core/primitives/di";
import { Tablero } from "../models/tablero.model";
import { Observable } from "rxjs/internal/Observable";



@Injectable({providedIn: 'root'})
export class TablerosService{
    private apiUrl = `${environment.apiUrl}/tableros`;
    private readonly _http = inject(HttpClient);


    getAllTableros(): Observable<Tablero[]> {
     return this._http.get<Tablero[]>(this.apiUrl);
        
    }
}