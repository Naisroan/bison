// default
import { NgModule } from '@angular/core';

// utiles
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// modulos de angular
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';

// modulos material
import { MaterialModule } from './material-module';

// componentes
import { HelloBisontecaComponent } from './c/hello-bisonteca/hello-bisonteca.component';
import { LoginComponent } from './c/login/login.component';
import { ThemesComponent } from './c/tests/themes/themes.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [

    // default
    AppComponent,

    // componentes
    HelloBisontecaComponent,
     LoginComponent,
     ThemesComponent
  ],
  imports: [

    // default
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,

    // modulos de angular
    ReactiveFormsModule,
    FormsModule,

    // material
    MaterialModule,

    // http
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
