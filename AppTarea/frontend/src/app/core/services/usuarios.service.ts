import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { Usuario, UsuarioUpdate } from "../models/usuario.model";

@Injectable({providedIn: 'root'})
export class UsuariosService {
    private apiUrl = `${environment.apiUrl}/usuarios`;
    private readonly _http = inject(HttpClient);

    getAllUsuarios(): Observable<Usuario[]> {
        return this._http.get<Usuario[]>(this.apiUrl);
    
    }

    createUsuario(usuario: Usuario): Observable<Usuario> {
     return this._http.post<Usuario>(this.apiUrl, usuario);
     
    }

    deleteUsuarioById(id_usuario: number): Observable<void> {
        return this._http.delete<void>(`${this.apiUrl}/${id_usuario}`);

    }

    updateUsuario(id_usuario: number, usuarioActualizado: UsuarioUpdate): Observable<void> {
        return this._http.put<void>(`${this.apiUrl}/${id_usuario}`, usuarioActualizado);
    }


    

}
