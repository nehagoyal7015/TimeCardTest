import { Component, OnInit, HostListener } from '@angular/core';
import { AuthService } from './../utils/login/auth-service.service';
import { Router } from '@angular/router';
import { HomeService } from '../Services/home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  events: string[] = [];
  public bgPopup:boolean = false;
  public opened: boolean = true;
  showFiller = false;
  mobileMenu = true;
  public user: any = {};
  title = 'tickspot';
  constructor( private authService: AuthService, private service:HomeService, private router: Router ) {
    if (window.innerWidth < 1024) {
      this.bgPopup = true;
      this.opened = false;
    } else {
      this.opened = true;
      this.bgPopup = false;
    }
  
    
  }
  ngOnInit(): void {
    let currentuser = JSON.parse(localStorage.getItem('user') as string);
    this.getUserName(currentuser.data.userId);
    
  }
  signOut() {
    this.authService.logout();
    this.router.navigate(['/']);
}

getUserName(userId: number) {
  this.service.getCurrentUser(userId).subscribe((result: any) => {
    if (result.success) {
      this.user = result.data;
      console.log(this.user);
    } else {}
  },
    (error) => {
      console.log(error.error.message, error.error.messageType);
    });
}

}
