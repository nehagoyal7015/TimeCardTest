import { HttpClient } from '@angular/common/http';
import { Component, HostListener, OnInit, Input, Output } from '@angular/core';
import { LoaderServiceService } from './utils/loader/loader-service.service';
import { AlertService } from './utils/alert-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  public spinnerIcon:  false;
  public message:any = '';
  

  constructor(private loaderServiceService: LoaderServiceService, private alertService: AlertService  ){
    
  }
  ngOnInit(): void {    
    this.loaderServiceService.spinner().subscribe(
      (data) => {
        this.setSpinner(data)
      }
    )
    this.alertService.errorAlt().subscribe(
      (data:any) => {
        this.gerErrorAlt(data);
        console.log(data);
      }
    )
  }
  setSpinner(blon:any) {
    this.spinnerIcon = blon;
  }
  gerErrorAlt(alt:any) {
      this.message = alt;
      setTimeout(() => {
        this.message = '';
      }, 10000);
    }
    closeAlert() {
      this.message = '';
    }

}
