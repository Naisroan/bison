export class Api {

  // host
  static readonly host = "localhost:44334/api";

  // funcion que construye una url en base a los parametros para consumir n endpoint de la api
  static getBaseUrl(controller : string, endpoint : string, useHttps : boolean = true) {
    return `${useHttps ? "https" : "http"}://${this.host}/${controller}/${endpoint}`;
  }

  /*
    variables constantes para los controladores y sus respectivos endpoints

    c -> controller
    e -> endpoint

    ejemplos:
    c_usuario -> controlador usuario
    c_e_login -> controlador usuario endpoint login
  */

  // --------------------------------------------------
  // usuarioController
  // --------------------------------------------------
  static readonly c_usuario = "Usuario";
  static readonly c_usuario_e_login = "Login";
  static readonly c_usuario_e_signup = "SignUp";
  static readonly c_usuario_e_read = "Read";
  static readonly c_usuario_e_readAll = "ReadAll";
}