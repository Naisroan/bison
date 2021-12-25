import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-master-menu',
  templateUrl: './master-menu.component.html',
  styleUrls: ['./master-menu.component.css']
})
export class MasterMenuComponent implements OnInit {

  menus = menuItems

  constructor() { }

  ngOnInit(): void {
  }

  hasChilds(id_menu : number) {
    return this.getChildMenus(id_menu).length > 0;
  }

  getChildMenus(id_menu : number) {
    let childs = menuItems.filter(n => n.id_menu != id_menu && n.id_padre == id_menu);
    return childs;
  }
}

export const menuItems = [
  {
    id_menu: 1,
    id_padre: 0,
    titulo: "bison ©",
    url: "/",
    icono: "home",
    descripcion: 'Inicio',
  },
  {
    id_menu: 2,
    id_padre: 2,
    titulo: "Cursos",
    url: ".",
    icono: "video_library",
    descripcion: 'Menu de cursos',
  },
  {
    id_menu: 3,
    id_padre: 0,
    titulo: "Reportes",
    url: ".",
    icono: "content_copy",
    descripcion: 'Reportes de los cursos',
  },
  {
    id_menu: 4,
    id_padre: 2,
    titulo: "Mis cursos comprados",
    url: ".",
    icono: "",
    descripcion: 'Visualiza los cursos que has comprado',
  },
  {
    id_menu: 5,
    id_padre: 2,
    titulo: "Mis cursos realizados",
    url: "cursos-realizados",
    icono: "",
    descripcion: 'Crea, visualiza, actualiza, o elimina tus cursos',
  }
];
