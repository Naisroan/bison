import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Session } from  'src/app/s/usuario/session.model';


@Injectable({
  providedIn: 'root'
})
export class StorageService {

  private readonly sessionKey = "currentUser";
  private localStorageService;
  private currentUser? : Session = null!;

  constructor(private router : Router) {
    this.localStorageService = localStorage;
    this.currentUser = this.loadSessionData();
  }

  setCurrentSession(session: Session): void {
    this.currentUser = session;
    this.localStorageService.setItem(this.sessionKey, JSON.stringify(session));
  }

  loadSessionData(): Session {
    var sessionStr = this.localStorageService.getItem(this.sessionKey);
    return (sessionStr) ? <Session>JSON.parse(sessionStr) : null!;
  }

  getCurrentSession() : Session {
    return this.currentUser!;
  }

  removeCurrentSession(): void {
    this.localStorageService.removeItem('currentUser');
    this.currentUser = null!;
  }

  getCurrentUser(): Session {
    var session: Session = this.getCurrentSession();
    return (session) ? <Session>session : null!;
  };

  isAuthenticated(): boolean {
    return (this.getCurrentToken() != null) ? true : false;
  };

  getCurrentToken(): string {
    var session = this.getCurrentSession();
    return (session && session.token) ? session.token : null!;
  };

  logout(): void{
    this.removeCurrentSession();
    // this.router.navigate(['/login']);
  }
}
