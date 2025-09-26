import { inject, Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Columna } from "../../models/columna.model";



@Injectable({ providedIn: 'root' })
export class ColumnasService {

    private apiUrl = `${environment.apiUrl}/columnas`;
    private readonly _http = inject(HttpClient);


    getAllColumnas(): Observable<Columna[]> {
        return this._http.get<Columna[]>(this.apiUrl);

    }

}