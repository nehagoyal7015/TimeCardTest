import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { AlertService } from './../../utils/alert-service.service';
import { AuthService } from '../login/auth-service.service';
import { Router } from '@angular/router';


@Injectable()
export class HttpErrorInterceptorServiceInterceptor implements HttpInterceptor {
  // public errorMsg:any = '';
  constructor(private alertService: AlertService, private authService: AuthService, private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // console.log('http error interceptor');
   
    return next.handle(request)
      .pipe(
        catchError((error: HttpErrorResponse) => { 
          const errorMessage = this.setError(error);
          this.alertService.errorMsg(errorMessage);
          console.log(errorMessage);
          return throwError(errorMessage);
        })
      )
  }
  setError(error: HttpErrorResponse) {
    let errorMessage:any;
    if(error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else if(error.status === 401) {
      this.authService.logout();
      this.router.navigate(['/']);
    } else {
       if(error.status != 0 ) {
        errorMessage = error.error.message;
       } 
    }
    // if(error.status === 401) {
    //   this.authService.logout();
    //   this.router.navigate(['/']);
    // }
    return errorMessage;
  }
  
}
