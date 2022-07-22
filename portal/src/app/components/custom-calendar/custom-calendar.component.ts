import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DashboardService } from 'src/app/Services/dashboard.service';

@Component({
  selector: 'app-custom-calendar',
  templateUrl: './custom-calendar.component.html',
  styleUrls: ['./custom-calendar.component.scss']
})
export class CustomCalendarComponent implements OnInit {
  @Output()
      getTimeSheet: EventEmitter<any> = new EventEmitter<any>();

  dtList: any[] = [];
  selectedDate = new Date();
  dateString: string = this.selectedDate.toString().substring(0,15);
  dtFrmt: string;
  userId: number;
  workdone: number[] = [0,0,0,0,0,0,0];
  hours: [];
  forapiDate: string = this.selectedDate.toString().substring(0,15);
  i = 0;

  constructor(private http: HttpClient, private dashboardService: DashboardService) { }

  ngOnInit(): void {
    let currentUser = JSON.parse(localStorage.getItem('user') as string);
    this.userId = currentUser.data.userId;
    this.GetWorkHr(0,this.formatDate(this.forapiDate));    
  }

  
  // creating array of visible dates
  visibleDates(offset: number) {
    var n = 0;
    this.dtList = [];
    while(n<7) {
      var date = new Date(this.selectedDate);
      var p: string = new Date(date.setDate(date.getDate() + offset - 3 + n)).toString().substring(0,15);
      p= p + this.workdone[n];
      this.dtList.push(p);
      n++;
    }
  }

  //Date which is clicked or selected on wich an event is occoured.
  select(date: string) {
    this.dateString = date.substring(0,15);
    this.dtFrmt = this.formatDate(this.dateString);
    this.getTimeSheet.emit(this.dateString); 
  }
  
  // selection of date from calender will create a new array of visible date here to sync with calander.
  syncCal(date: Date) {
    this.i=0;
    var n = 0;
    this.dtList = [];
    this.dateString = date.toString().substring(0,15);
    while(n<7) {
      var seldate = new Date(date);
      var p: string = new Date(seldate.setDate(seldate.getDate() +  - 3 + n)).toString().substring(0,15);
      p= p + this.workdone[n];
      this.dtList.push(p);
      n++;
    }
  }

  // returns a list of work hours of 7 days and send it to visible date to create new array of visible dates.
  GetWorkHr(offset: number,date: string) {
    this.dashboardService.GetWorkHr(date)
      .subscribe((result: any) => {
        if (result.success) {
           this.workdone = result.data;
           this.visibleDates(offset);
        }
        else {
          console.log("Unsuccessful");
        }
      });
  }

  // date wich is selected from picker work as a click event.
  selectFromPicker(){
    var date: string = this.dtList[3].toString().substring(0,15);
    console.log(date);
    this.getTimeSheet.emit(date); 
  }

  // on clicking right and left arrow on calendar will change the date offset, 
  // this will give correct starting date for geting list of working hours of visible dates. 
  apiDate(n: number) {
    if (n==0){
    this.forapiDate=this.dtList[2].toString().substring(0,15);
    }
    else
    this.forapiDate=this.dtList[4].toString().substring(0,15);
  }
  
  // formating date for api

  formatDate(dt: string) {
    var dtFormat = new Date(dt);
    var d = dtFormat.getDate();
    var dd = d.toString();
    if (d / 10 < 1)
      dd = '0' + d;
    var m = dtFormat.getMonth() + 1;
    var mm = m.toString();
    if (m / 10 < 1)
      mm = '0' + m;
    var y = dtFormat.getFullYear();
    // var someFormattedDate = mm + '-' + dd + '-' + y;
    var someFormattedDate = y + '-' + mm + '-' + dd;
    return someFormattedDate;
  }

}