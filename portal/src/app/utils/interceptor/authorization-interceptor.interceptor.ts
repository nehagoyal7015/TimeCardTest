import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthService } from './../../utils/login/auth-service.service'
import { Router } from '@angular/router';

@Injectable()
export class AuthorizationInterceptorInterceptor implements HttpInterceptor {

  constructor( private authService: AuthService, private router: Router ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    
    if (this.authService.isLoggedIn()) {
     
      const authToken = this.authService.getAuthorizationToken();
      request = request.clone({
          setHeaders:
              { Authorization: `Bearer ${authToken.token}` }
          }
      );
  } 

    return next.handle(request)
    .pipe(
			tap(null,
			error => {
				if(error.status === 401) {
					this.authService.logout();
					this.router.navigate(['/']);
				}
			})
		);
  }

  
}
