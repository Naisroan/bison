// default
import { NgModule } from '@angular/core';

// utiles
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';

// modulos de angular
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';

// modulos material
import { MaterialModule } from './material-module';

// componentes
import { HelloBisontecaComponent } from './components/hello-bisonteca/hello-bisonteca.component';
import { MasterMenuComponent } from './components/master-menu/master-menu.component';
import { CursosListComponent } from './components/cursos/cursos-list/cursos-list.component';
import { LoginComponent } from './components/login/login.component';

@NgModule({
  declarations: [

    // default
    AppComponent,

    // componentes
    HelloBisontecaComponent,
    LoginComponent,
    MasterMenuComponent,
    CursosListComponent
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
