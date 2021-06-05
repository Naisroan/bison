import { Component, OnInit } from '@angular/core';
import { FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  hide = true
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required]);

  constructor() { }

  ngOnInit(): void {
  }

  getErrorMessage(inputName : string) {
  
    switch (inputName) {

      case "email": {

        if (this.email.hasError('required')) {
          return 'Ingrese tu correo';
        }
    
        return this.email.hasError('email') ? 'No es un correo valido' : '';
      }

      case "password": {

        if (this.password.hasError('required')) {
          return 'Ingresa tu contraseña';
        }
      }
    }

    return "";
  }
}
