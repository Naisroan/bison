import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CursosListComponent } from './components/cursos/cursos-list/cursos-list.component';
import { HelloBisontecaComponent } from './components/hello-bisonteca/hello-bisonteca.component';

const routes: Routes = [
  { path: '', component: HelloBisontecaComponent },
  { path: 'cursos-realizados', component: CursosListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
