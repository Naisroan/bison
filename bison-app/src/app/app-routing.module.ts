import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CursosListComponent } from './c/cursos-list/cursos-list.component';
import { HelloBisontecaComponent } from './c/hello-bisonteca/hello-bisonteca.component';

const routes: Routes = [
  { path: '', component: HelloBisontecaComponent },
  { path: 'cursos-realizados', component: CursosListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
