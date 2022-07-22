import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth-service.service';

@Injectable({
  providedIn: 'root'
})
export class UserActiveGuard implements CanActivate {
  constructor( private authService: AuthService, private router: Router){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
     return this.checkLogin();
  }
  CanActivateChild( router: ActivatedRouteSnapshot, state: RouterStateSnapshot ): boolean {
    return this.canActivate(router, state);
  }
  checkLogin() {
    if(this.authService.isLoggedIn()) {
      return true;
    } 
    this.router.navigate(['/']);
    return false;
  }
}
