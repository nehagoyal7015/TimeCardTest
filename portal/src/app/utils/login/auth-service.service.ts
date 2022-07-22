import { HttpClient, HttpErrorResponse, HttpBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SocialAuthService } from 'angularx-social-login';
import { throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { environment } from './../../../../src/environments/environment';
import { AlertService } from './../../utils/alert-service.service'
@Injectable({
  providedIn: 'root'
})
export class 
AuthService {

  public errorData: {};

  private http : HttpClient;
  constructor(private handler: HttpBackend, private alertService: AlertService,private socialAuthService: SocialAuthService) {
    this.http = new HttpClient(handler);
   }

  baseUrl = environment.apiUrl;

  login(data: any) {
    return this.http.post<any>(this.baseUrl + 'Authenticate/authentication', data)
    .pipe(map(user => {
        if (user && user.data) {
          localStorage.setItem('user', JSON.stringify(user));
        }
      }),
      catchError((error: HttpErrorResponse) => { 
        const errorMessage = this.handleError(error);
        this.alertService.errorMsg(errorMessage);
        console.log(errorMessage);
        return throwError(errorMessage);
      })
    );
  }

  gLogin(data: any) {
    
    return this.http.post<any>(this.baseUrl + 'Authenticate/gAuth' , data)
    .pipe(map(user => { 
        if (user && user.data) {
          localStorage.setItem('user', JSON.stringify(user));
        }
      }),
      catchError((error: HttpErrorResponse) => { 
        const errorMessage = this.handleError(error);
        this.alertService.errorMsg(errorMessage);
        console.log(errorMessage);
        return throwError(errorMessage);
      })
    );
  }




  isLoggedIn(){
    if(localStorage.getItem('user')) {
      return true
    }
    return false;
  }

  logout() {
    localStorage.removeItem('user');
    sessionStorage.removeItem('user');
    window.location.reload();
  }

  getAuthorizationToken() {
    let currentUser = JSON.parse(localStorage.getItem('user') as string);
    return currentUser.data;
  }
  

  private handleError(error: HttpErrorResponse) {
    let errorMessage:any;
    if(error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
       if(error.status != 0 ) {
        errorMessage = error.error.message;
       }
    }
    return errorMessage;
  }

  

}