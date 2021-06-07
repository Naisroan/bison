import { MatSnackBar } from '@angular/material/snack-bar';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidatorFn, Validators} from '@angular/forms';
import { Usuario } from 'src/app/s/usuario/usuario.model';
import { UsuarioService } from 'src/app/s/usuario/usuario.service';
import { MustMatch } from 'src/app/h/must-match.validator';
import { StorageService } from 'src/app/s/usuario/storage.service';
import { Session } from 'src/app/s/usuario/session.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  sectionIndex = 0;
  hide = true;
  errorMessage = "";
  loading = false;
  usuario : Usuario;
  frmLogin : FormGroup;
  frmRegistro : FormGroup;

  constructor(public service : UsuarioService, public storage : StorageService, public snack : MatSnackBar) {

    this.usuario = new Usuario();

    this.frmLogin = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
    });
    
    this.frmRegistro = new FormGroup({
      new_nick: new FormControl('', [Validators.required]),
      new_email: new FormControl('', [Validators.required, Validators.email]),
      new_confirm_email: new FormControl('', [Validators.required, Validators.email]),
      new_password: new FormControl('', [Validators.required]),
      new_confirm_password: new FormControl('', [Validators.required])
    }, {
      validators: [
        this.passwordMatchValidator,
        this.emailMatchValidator
      ]
    });
  }

  ngOnInit(): void {
    this.loading = false;
  }

  onFrmLoginSubmit() {

    this.loading = true;
    
    this.usuario.email = this.frmLogin.controls["email"].value;
    this.usuario.pass = this.frmLogin.controls["password"].value;
    
    this.service.login(this.usuario).subscribe({
      next: result => {
        let session = result as Session;
        this.frmLogin.reset('');
        this.usuario.email = '';
        this.usuario.pass = '';
        this.storage.setCurrentSession(session);
        this.loading = false;
      },
      error: result => {
        if (result.status == 400) {
          this.openSnackBar(result.error, "Entendido");
        } else {
          this.openSnackBar("La información no esta disponible por el momento, contacte con el administrador", "Entendido");
          console.log(result);
        }

        this.loading = false;
      }
    });
  }

  onFrmRegistroSubmit() {

    this.loading = true;
    
    this.usuario.nick = this.frmRegistro.controls["new_nick"].value;
    this.usuario.email = this.frmRegistro.controls["new_email"].value;
    this.usuario.pass = this.frmRegistro.controls["new_password"].value;

    this.service.signup(this.usuario).subscribe({
      next: result => {
        this.sectionIndex = 0;
        this.frmRegistro.reset('');
        this.usuario.nick = '';
        this.usuario.email = '';
        this.usuario.pass = '';
        this.loading = false;
        this.openSnackBar(`El usuario ${this.usuario.nick} ha sido creado con éxito`, 'Entengido');
      },
      error: result => {
        if (result.status == 400) {
          this.openSnackBar(result.error, "Entendido");
        } else {
          this.openSnackBar("La información no esta disponible por el momento, contacte con el administrador", "Entendido");
          console.log(result);
        }

        this.loading = false;
      }
    });
  }

  openSnackBar(message: string, action: string) {
    this.snack.open(message, action, {
      duration: 3000
    });
  }

  getErrorMessageForLogin(inputName : string) {
  
    switch (inputName) {

      default: {
        return "";
      }

      case "email": {

        if (this.frmLogin.controls["email"].hasError('required')) {
          return 'Ingrese tu correo';
        }
    
        return this.frmLogin.controls["email"].hasError('email') ? 'No es un correo valido' : '';
      }

      case "password": {

        if (this.frmLogin.controls["password"].hasError('required')) {
          return 'Ingresa tu contraseña';
        }

        return "";
      }

      case "nick": {

        if (this.frmLogin.controls["nick"].hasError('required')) {
          return 'Ingresa tu alias';
        }

        return "";
      }
    }

    return "";
  }

  getErrorMessageForRegister(inputName : string) {
  
    switch (inputName) {

      default: {
        return "";
      }

      case "new_email": {

        if (this.frmRegistro.controls["new_email"].hasError('required')) {
          return 'Ingrese el correo que usarás';
        }
    
        return this.frmRegistro.controls["new_email"].hasError('email') ? 'No es un correo valido' : '';
      }

      case "new_confirm_email": {

        if (this.frmRegistro.controls["new_confirm_email"].hasError('required')) {
          return 'Ingrese el correo que usarás';
        }
    
        return this.frmRegistro.controls["new_confirm_email"].hasError('email') ? 'No es un correo valido' : '';
      }

      case "new_password": {

        if (this.frmRegistro.controls["new_password"].hasError('required')) {
          return 'Ingresa la contraseña que usarás';
        }

        return "";
      }

      case "new_nick": {

        if (this.frmRegistro.controls["new_nick"].hasError('required')) {
          return 'Ingresa el alias que usarás';
        }

        return "";
      }

      case "new_name": {

        if (this.frmRegistro.controls["new_name"].hasError('required')) {
          return 'Ingresa tu(s) nombre(s)';
        }

        return "";
      }

      case "confirm_password": {

        if (this.frmRegistro.controls["new_confirm_password"].hasError('mismatch')) {
          return 'Las contraseñan no coinciden';
        }

        return "";
      }

      case "confirm_email": {

        if (this.frmRegistro.controls["new_confirm_email"].hasError('mismatch')) {
          return 'Los correos no coinciden';
        }

        return "";
      }
    }

    return "";
  }

  passwordMatchValidator(g: FormGroup) {

    let new_password = g.get('new_password')!;
    let new_confirm_password = g.get('new_confirm_password')!;

    if (new_password.value !== new_confirm_password.value) {

      new_confirm_password.setErrors({'mismatch': true})
      return {'mismatch': true};

    } else {

      new_confirm_password.setErrors(null)
      return null; // esto hay que checarlo, al parecer retornar un null, es valido
    }

    return g.get('new_password')!.value === g.get('new_confirm_password')!.value
       ? null : {'mismatch': true};
  }

  emailMatchValidator(g: FormGroup) {

    let new_email = g.get('new_email')!;
    let new_confirm_email = g.get('new_confirm_email')!;

    if (new_email.value !== new_confirm_email.value) {

      new_confirm_email.setErrors({'mismatch': true})
      return {'mismatch': true};

    } else {

      new_confirm_email.setErrors(null)
      return null; // esto hay que checarlo, al parecer retornar un null, es valido
    }
  }
}
