import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { MediaMatcher } from '@angular/cdk/layout';
import { StorageService } from './services/usuario/storage.service';
import { OverlayContainer } from '@angular/cdk/overlay';

export const DARK_CLASS_NAME = 'dark-theme';
export const LIGHT_CLASS_NAME = 'light-theme';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnDestroy, OnInit {
  
  title = 'bison ©';
  theme = DARK_CLASS_NAME;
  mobileQuery: MediaQueryList;
  opened: boolean;
  storage : StorageService;
  menuItems = [
    {
      id: 1,
      id_padre: 0,
      titulo: "Cursos",
      url: ".",
      icono: "video_library",
    },
    {
      id: 2,
      id_padre: 0,
      titulo: "Reportes",
      url: ".",
      icono: "content_copy",
    },
    {
      id: 3,
      id_padre: 1,
      titulo: "Mis cursos comprados",
      url: ".",
      icono: "content_copy",
    },
    {
      id: 4,
      id_padre: 1,
      titulo: "Mis cursos realizados",
      url: ".",
      icono: "content_copy",
    }
  ];

  private _mobileQueryListener: () => void;

  constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher, storage : StorageService, private overlay: OverlayContainer) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
    this.opened = false;
    this.storage = storage;
  }

  ngOnInit(): void {
    this.fixOverlayTheme();
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  toggleTheme(): void {
    this.theme = this.theme == DARK_CLASS_NAME ? LIGHT_CLASS_NAME : DARK_CLASS_NAME;
    this.fixOverlayTheme();
  }

  fixOverlayTheme() : void {

    this.overlay.getContainerElement().classList.remove(DARK_CLASS_NAME);
    this.overlay.getContainerElement().classList.remove(LIGHT_CLASS_NAME);

    if (this.theme == DARK_CLASS_NAME) {
      this.overlay.getContainerElement().classList.add(DARK_CLASS_NAME);
    } else {
      this.overlay.getContainerElement().classList.add(LIGHT_CLASS_NAME);
    }
  }
}
