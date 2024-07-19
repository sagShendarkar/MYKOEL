import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth/auth.service'; 
import { take } from 'rxjs/operators'; 

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
     
  let  currentUser=this.accountService.isTokenAvailable();
    if (currentUser) {
      let token = localStorage.getItem('token');
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      })
    }

    return next.handle(request);
  }
}