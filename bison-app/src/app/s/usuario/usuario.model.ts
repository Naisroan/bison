export class Usuario {
  public idUsuario : number = 0;
  public idRol : number = 0;
  public nick : string = "";
  public pass : string = "";
  public email : string = "";
  public nombre : string = "";
  public apPaterno : string = "";
  public apMaterno : string = "";
  public activo? : boolean;
  public rutaImagen : string = "";
  public fechaAlta? : Date;
  public fechaMod? : Date;
}