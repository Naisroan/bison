import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { MediaMatcher } from '@angular/cdk/layout';
import { StorageService } from './s/usuario/storage.service';
import { Session } from './s/usuario/session.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnDestroy, OnInit {
  
  title = 'bison ©';
  theme = 'dark-theme';
  mobileQuery: MediaQueryList;
  opened: boolean;
  storage : StorageService;
  menuItems = [
    {
      titulo: "Cursos",
      url: "#!",
      icono: "video_library",
    },
    {
      titulo: "Reportes",
      url: "#!",
      icono: "content_copy",
    }
  ];

  private _mobileQueryListener: () => void;

  constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher, storage : StorageService) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
    this.opened = false;
    this.storage = storage;
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  toggleTheme(): void {
    this.theme = this.theme == 'dark-theme' ? 'light-theme' : 'dark-theme';
  }
}
