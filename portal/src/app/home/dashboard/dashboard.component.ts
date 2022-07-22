import { Component, OnInit, TemplateRef } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { HolidayService } from 'src/app/Services/holiday.service';
import { HolidayModel, listEmpOptHoliday, userOptHolidays } from 'src/app/Model/holiday.model';
import { LeaveServiceService } from 'src/app/Shared/leave-service.service';
import { Leave, UpcomingLeaves } from 'src/app/Types/leave';
import { DashboardService } from 'src/app/Services/dashboard.service';
import { Clients, Projects, TimeSheetEntity, TimeSheetInpt } from './project.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DialogService } from 'src/app/Shared/dialog.service';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';



@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})



export class DashboardComponent implements OnInit {
  public time = '';
  errorHengling = new FormControl('', [
    Validators.required
  ]);
  buttonForTime = 'ENTER TIME';
  tasks: any;
  timesheetId = -1;
  leaveList: UpcomingLeaves[];
  holidayList: HolidayModel[];
  optFlotEmpList: userOptHolidays[];
  userProj: Clients[];
  recentProj: Projects[];
  requestDate = new Date;
  timeSheet: TimeSheetEntity[];
  workHr: number = 0;
  weekData = { workedHrs: 0, totalWorkHr: 0 };
  monthData = { workedHrs: 0, totalWorkHr: 0 };

  // weekWorkHr: number = 0;
  // MonthWorkHr: number = 0;
  timeSheetInput = new TimeSheetInpt;
  userId: number;
  workdone: number[] = [0, 0, 0, 0, 0, 0, 0];
  i = 0;

  public OptEmpTable = ['EmpId', 'Name'];
  empSelectUserId = -1;
  isArchievedStatus:boolean=false;
  constructor(private http: HttpClient, public dialog: MatDialog, private holidayService: HolidayService, private leaveService: LeaveServiceService, private dashboardService: DashboardService, private _snackBar: MatSnackBar, private dialogService: DialogService, private router: Router) {
  }

  ngOnInit(): void {
    this.GetUpcomingHoliday();
    let currentUser = JSON.parse(localStorage.getItem('user') as string);
    this.getAllUpcomingLeave();
    this.GetUserProjects();
    this.userId = currentUser.data.userId;
    this.GetTimeSheet(this.requestDate.toLocaleDateString('en-US'));
    this.GetWeekHr();
    this.GetMonthHr();
    this.GetRecentProj();

  }

  openDialog(templateRef: TemplateRef<any>, isFloating: boolean) {

    if (isFloating) {
      this.dialog.open(templateRef, {
        width: '500px',
      });
    }
  }

  addProjectIdInfo(id: any, event:any){
    debugger;
      
    if(event.checked){
      this.timeSheetInput.projectId = id;
  }
    else{
      // val is found, removing from array
      this.timeSheetInput.projectId = 0;
    }
   }



  // Adding & Updating user TimeSheet data

  AddUserTime() {
    debugger;
    this.timeSheetInput.timeSheetId = this.timesheetId;
    this.timeSheetInput.requestDate = this.formatDate(this.requestDate.toDateString());
    this.timeSheetInput.isBilled = true;
    this.dashboardService.AddTimeSheet((JSON.parse(JSON.stringify(this.timeSheetInput)))).subscribe((result: any) => {
      if (result.data) {
        this._snackBar.open('Record Saved Successfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: 2 * 1000,
        });


      } else {
        this._snackBar.open('Can not add record', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: 2 * 1000,
        });

      }
      this.GetTimeSheet(this.requestDate.toDateString());
      this.buttonForTime = 'ENTER TIME';
      this.timesheetId = -1;
      this.GetMonthHr();
      this.GetWeekHr();
    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
  }


  // when selecting client-project all the timesheet data first reset.
  selection() {
    this.tasks = null;
    this.timeSheetInput.projectId = -1;
    this.timeSheetInput.taskId = -1;
    this.timeSheetInput.billableHours = 0.0;
    this.timeSheetInput.billableHoursNote = '';
    this.timeSheetInput.nonBillableHours = 0.0;
    this.timeSheetInput.nonBillableHoursNote = '';

  }

  // Getting projects and tasks associated to user
  GetUserProjects() {
    this.dashboardService.GetUserProjects()
      .subscribe((result: any) => {
        if (result.success) {
          this.userProj = result.data;
          if(result.data[0].projectId){
            this.timeSheetInput.projectId = result.data[0].projectId;
            this.timeSheetInput.taskId = result.data[0].taskId;
          }
          console.log(result.data);
          console.log("lskdfjhgkjasdfkj");
          console.log(this.userProj);
        }
        else {
          console.log("Unsuccessful");
        }
      },
        (error) => {
          console.log(error.error.message, error.error.messageType);
        });
  }


  // Getting TimeSheet data of user from api
  GetTimeSheet(date: string) {
    this.requestDate = new Date(date);
    this.workHr = 0;
    this.timeSheet = [];
    date = this.formatDate(date);
    this.dashboardService.GetTimeSheet(date)
      .subscribe((result: any) => {
        this.workHours(result.data);
        if (result.success) {
          this.timeSheet = result.data;
          console.log(result.data);
          console.log(this.timeSheet);

        }
        else {
          console.log("Unsuccessful");
        }
      },
        (error) => {
          console.log(error.error.message, error.error.messageType);
        });
  }


  // Deletion of TimeSheet data
  DeleteTimeSheet(id: number) {
    this.dialogService.deleteConfirmDialog().afterClosed().subscribe(res => {
      if (res) {

        this.dashboardService.DeleteTimeSheet(id)
          .subscribe((result: any) => {
            this.GetTimeSheet(this.requestDate.toDateString());
            if (result.data) {
              this._snackBar.open('Record deleted successfully!', 'OK', {
                panelClass: 'my-custom-snackbar',
                duration: 2 * 1000,
              });
            } else {
              this._snackBar.open('Can not delete record', 'OK', {
                panelClass: 'my-custom-snackbar',
                duration: 2 * 1000,
              });
            }
          },
            (error) => {
              console.log(error.error.message, error.error.messageType);
            });
      }
    })
  }


  // Edit TimeSheet data of user
  editTimeSheet(data: any) {
    this.timeSheetInput.taskId = data.taskId;
    this.timeSheetInput.projectId = data.projectId;
    this.timeSheetInput.billableHours = data.billableHours;
    this.timeSheetInput.billableHoursNote = data.billableHoursNote;
    this.timeSheetInput.nonBillableHours = data.nonBillableHours;
    this.timeSheetInput.nonBillableHoursNote = data.nonBillableHoursNote;
    this.timesheetId = data.timeSheetId;
    this.buttonForTime = 'UPDATE ENTRIES';

  }

  // Getting upcoming holiday list.
  GetUpcomingHoliday() {
    this.holidayService.GetUpcomingHoliday()
      .subscribe((result: any) => {
        if (result.success) {
          this.holidayList = result.data;
        }
        else {
          console.log("Unsuccessful");
        }
      },
        (error) => {
          console.log(error.error.message, error.error.messageType);
        });
  }



  // Getting upcoming leave list
  getAllUpcomingLeave() {
    this.leaveService.getAllUpcomingLeave().subscribe((result: any) => {
      if (result.success) {
        this.leaveList = result.data;
        console.log(this.leaveList);
      }
    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
  }


  // Getting recent projects and tasks associated to user.
  GetRecentProj() {
    this.dashboardService.GetRecentProj().subscribe((result: any) => {
      if (result.success) {
        this.recentProj = result.data;
        this.isArchievedStatus = result.data.projectTask;
        console.log(this.recentProj);
        console.log(this.isArchievedStatus);
      }
    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
  }

  // Click event for recent projects and tasks
  assigningRecent(taskId: any, projId: any) {
    this.timeSheetInput.taskId = taskId;
    this.timeSheetInput.projectId = projId;
  }


  // Calculating work hours of the day from TimeSheet data list
  workHours(data: TimeSheetEntity[]) {
    data.forEach(element => {
      this.workHr = element.billableHours + this.workHr;

    });
  }


  // Formating date
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

  // Formating Hours like 08:00Hr
  hoursFormating(data: any) {
    if (data % 1 > 0) {
      var formatedHours = '0' + data.toString().substring(0, 1) + ':' + (data % 1) * 60;
      return formatedHours;
    }
    else {
      var formatedHours = '0' + data + ':00';
      return formatedHours;
    }
  }

  // Getting current week working hours.
  GetWeekHr() {
    this.dashboardService.GetWeekHr()
      .subscribe((result: any) => {
        if (result.success) {
          this.weekData = result.data;
        }
        else {
          console.log("Unsuccessful");
        }
      });
  }

  // Getting current month working hours.
  GetMonthHr() {
    this.dashboardService.GetMonthHr()
      .subscribe((result: any) => {
        if (result.success) {
          this.monthData = result.data;
        }
        else {
          console.log("Unsuccessful");
        }
      });
  }

  //Comparing date
  isLater(a: string, b: string) {
    return Date.parse(a) > Date.parse(b);
  }
}
