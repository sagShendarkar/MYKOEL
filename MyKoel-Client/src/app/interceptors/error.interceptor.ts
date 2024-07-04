import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Router, NavigationExtras } from '@angular/router';
// import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { AuthService } from '../services/auth/auth.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  unauthAlert: boolean = false;

  constructor(private router: Router, private auth: AuthService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error:any) => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modalStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key])
                  }
                }
                // throw modalStateErrors.flat();
              } else if (typeof (error.error) === 'object') {
                Swal.fire(error.statusText, error.status);
              } else {
                Swal.fire(error.error, error.status);
              }
              break;
            case 401:
              var that = this;
              // var authValues = this.auth.getAuthFromLocalStorage();
              // if(new Date(authValues.refreshExpiryInMins).getTime() - Date.now() <= 0){
              //   //only show one alert
              //   if(this.unauthAlert){
              //     return;
              //   }
              //   this.unauthAlert = true;
              //   Swal.fire({
              //     html:"<div class=\"container fs-3\">Session Expired! <br>Please login again.</div>",
              //     allowOutsideClick: false
              //   }).then(()=>{
              //     this.unauthAlert = false;
              //     that.auth.logout();
              //     document.location.reload();
              //     });
              // }
              // else{
              //   let defaultRouterLink = localStorage.getItem('defaultRouterlink');
              //   this.router.navigateByUrl('/');
              // }
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              // const navigationExtras: NavigationExtras = { state: { error: error.error } }
              // this.router.navigateByUrl('/server-error', navigationExtras);
              console.log("Error 500::"+error);
              break;
            default:
              //Swal.fire('Something unexpected went wrong');
              console.log(error);
              break;
          }
        }
        return throwError(error);
      })
    )
  }
}
