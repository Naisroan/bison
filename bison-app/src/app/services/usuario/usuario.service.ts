import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Api } from 'src/app/services/api';
import { Usuario } from 'src/app/services/usuario/usuario.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  controller : string = Api.c_usuario;

  constructor(private http: HttpClient) { 
  }
  
  login(usuario : Usuario) : Observable<any> {
    return this.http.post<any>(
       // url
      Api.getBaseUrl(this.controller, Api.c_usuario_e_login),
      // data
      usuario
    );
  }
  
  signup(usuario : Usuario) : Observable<any> {
    return this.http.post<any>(
       // url
      Api.getBaseUrl(this.controller, Api.c_usuario_e_signup),
      // data
      usuario
    );
  }
}
