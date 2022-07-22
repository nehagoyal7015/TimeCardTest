import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import { Router,NavigationStart} from '@angular/router';
import { AuthService } from './../utils/login/auth-service.service';
import { LoaderServiceService } from './../utils/loader/loader-service.service'
import { GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  users: SocialUser | null; 
  // model = new User()
  
  public user = {domainName: '', userEmail: '', password: ''}
  // public gUser: SocialUser = new SocialUser;
  public gUser = {userEmail:''};

  
  constructor(private router: Router,private http:HttpClient, private authService: AuthService, private loaderService: LoaderServiceService, private socialAuthService: SocialAuthService) 
  { }

  ngOnInit(): void {
  
  }

  login() {
    
  }
  onSubmit(heroForm:any) {
    this.loaderService.emitLoadingChangeEvent(true);
    this.authService.login(this.user).subscribe((data) => {
      console.log(data);
       if (this.authService.isLoggedIn()) {
          this.router.navigate(['/', 'home']);
          this.loaderService.emitLoadingChangeEvent(false);
        } else {
          console.log('test')
        }
      },
      (error) => {  
        this.loaderService.emitLoadingChangeEvent(false);
        console.log(error.error.message, error.error.messageType);
      }
    );
  }
  
  
  loginWithGoogle():void {
    
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID)
    this.socialAuthService.authState.subscribe(u => {
      this.gUser.userEmail = u.email;
      console.log(this.gUser);
      this.loaderService.emitLoadingChangeEvent(true);

      this.authService.gLogin((JSON.parse(JSON.stringify(this.gUser)))).subscribe((data) => {
        
        console.log(data);
         if (this.authService.isLoggedIn()) {
            this.router.navigate(['/', 'home']);
            this.loaderService.emitLoadingChangeEvent(false);
          } else {
            console.log('test')
            localStorage.removeItem('user');
            this.router.navigate(['/',]);
          }
        },
        (error) => {  
          this.loaderService.emitLoadingChangeEvent(false);
          localStorage.removeItem('user');
          console.log(error.error.message, error.error.messageType);
          this.router.navigate(['/']);
        }
      );
    });

    
    
  }
 
}