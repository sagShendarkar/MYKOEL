import { HeaderService } from './../containers/admin-layout/services/header.service';

import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { AuthService } from '../services/auth/auth.service';
@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, public headerService:HeaderService,private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let parentRoute = '';

let isMoodFilled=localStorage.getItem('IsMoodFilled');
let userId=localStorage.getItem('userId');
if(isMoodFilled==='YES'){

    console.log(route?.data);
this.headerService.headerTitle$.next(route?.data?.['title']);
this.headerService.isDisplayAddBtn$.next(route?.data?.['isDisplayAddBtn']);
this.headerService.backBtnUrl$.next(route?.data?.['backBtnUrl']);
if(route?.data?.['isDisplayAddBtn']){ 
this.headerService.addBtn$.next(route?.data?.['addBtn']);
}
    const currentUser = this.authService.isTokenAvailable();
    if (currentUser) {
      // logged in so return true
      return true;
    }
    // not logged in so redirect to login page with the return url
     this.authService.logout();
    return false;
}else{
  if (this.authService.isTokenAvailable()) {
     this.router.navigateByUrl("/mood-check/"+userId);
  }else{
    this.router.navigate(['login']);
  }
}
return false;
  }
}
