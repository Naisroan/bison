// default
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

// utiles
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// material
import { MaterialModule } from './material-module';

// componentes
import { HelloBisontecaComponent } from './c/hello-bisonteca/hello-bisonteca.component';
import { LoginComponent } from './c/login/login.component';

@NgModule({
  declarations: [

    // default
    AppComponent,

    // componentes
    HelloBisontecaComponent,
     LoginComponent
  ],
  imports: [

    // default
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,

    // material
    MaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
