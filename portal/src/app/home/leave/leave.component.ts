import { ChangeDetectorRef, Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { from, Observable } from 'rxjs';
import { LeaveServiceService } from 'src/app/Shared/leave-service.service';
import { Validators, FormBuilder, FormGroup, FormControl, NgForm } from '@angular/forms';
import { } from '@angular/material/table';
import { Leave, userCountList } from 'src/app/Types/leave';

import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { DialogService } from 'src/app/Shared/dialog.service';
import { NotificationService } from 'src/app/Shared/notification.service';
import { result } from 'lodash';





@Component({
  selector: 'app-leave',
  templateUrl: './leave.component.html',
  styleUrls: ['./leave.component.scss']
})
export class LeaveComponent implements OnInit {

  buttonDisabled: boolean;
  minDate = new Date();
  UserId: number;
  Year: number;
  LeaveId : number; 
  durationInSeconds = 15;
  public userAccount: any;
  public selectedUser: any;
  public deletedRowClass: string;
  public isCancelled = true;
  leaveList: Leave[];
  leaveForm: any;

  public userCountList: any = { planned: '', unplanned: '', holiday: '', floating: '', totalCount: '' };
  displayedColumns: any = ['leaveType', 'fromDate', 'toDate', 'reason', 'total', 'isCancelled'];
  public otherEmpSource: any = [
    { planType: 'Unplanned' },
    { planType: 'planned' },
    { planType: 'Holiday' },
    { planType: 'Floating' }
  ];
  
  defaultYr: number = new Date().getFullYear();
  selectedRowIndex = -1;
  public otherEmpColumns = ['planned', 'Unplanned', 'Holiday', 'Floating', 'Total'];
  adminAccess = 0;
  selectedUserId = 0;
  selectedYear = 0;
  constructor(public dialog: MatDialog, private  formbulider: FormBuilder, private service: LeaveServiceService, private changeDetectorRefs: ChangeDetectorRef,
    private _snackBar: MatSnackBar, private dialogService: DialogService, private notificationservice: NotificationService) { }



  ngOnInit(): void {
    this.adminAccess = history.state.access;
    
    console.log(history.state.access);
    this.getLeaveList(this.defaultYr);
    this.leaveForm = this.formbulider.group({
      leaveType: ['', [Validators.required]],
      fromDate: ['', [Validators.required]],
      toDate: ['', [Validators.required]],
      reason: ['', [Validators.required]]
    });

    let currentUser = JSON.parse(localStorage.getItem('user') as string);

    this.selectedUserId = currentUser.data.userId;
    this.getLeaveList(this.curYear);
    this.getUserName(currentUser.data.userId);
    this.selectedUser = currentUser.data.userId;
    this.getUserLeave(currentUser.data.userId);
    this.UserId = currentUser.data.userId;

  }

  curYear: number = new Date().getFullYear();
  yearList: any = [
    { value: this.curYear - 1, name: this.curYear - 1 },
    { value: this.curYear, name: this.curYear },
    { value: this.curYear + 1, name: this.curYear + 1 },
    { value: this.curYear + 2, name: this.curYear + 2 }
  ];

  onFormSubmit() {
    const leave = this.leaveForm.value;
    this.createLeave(leave);
    this.leaveForm.reset();
  }

  onNewFormSubmit(){
    const leave = this.leaveForm.value;
    this.createNewLeave(leave,this.UserId);
    this.leaveForm.reset();
  }

  createLeave(leave: Leave) {
    this.service.addLeave(leave).subscribe((result: any) => {
      this.dialog.closeAll();
      if (result.data) {
        this.getLeaveList(this.Year);
        this.getLeaveList(this.curYear);
        this.getUserLeave(this.UserId);
        this.leaveList = result.data;
        this._snackBar.open('Record Saved Successfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      } else {
        this._snackBar.open('Unsuccessfull', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      }

    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
  }

  openDialog(templateRef: TemplateRef<any>) {
    this.dialog.open(templateRef, {
      width: '500px',
    });
  }

  createNewLeave(leave: Leave, userId: any) {
    this.UserId = userId
    this.service.addNewLeave(leave,userId).subscribe((result: any) => {
      this.dialog.closeAll();
      if (result.data) {
        this.getLeaveList(this.Year);
        this.getLeaveList(this.curYear);
        this.getUserLeave(this.UserId);
        this.leaveList = result.data;
        this._snackBar.open('Record Saved Successfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      } else {
        this._snackBar.open('Unsuccessfull', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      }
    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
  }
  getRecord(row: any) {
    this.openDialog(row);
  }

  getLeaveList(year: any) {
    this.selectedYear = year;
    
    this.service.getLeave(this.selectedUserId, year).subscribe((result: any) => {
      if (result.success) {
        this.leaveList = result.data;
        this.getUserLeave(this.UserId);
        this.changeDetectorRefs.detectChanges();
       
      }
    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
  }

  dateCheck(date: Date) {
    var elementDate = new Date(date);
    var curDate = new Date();
    if (elementDate <= curDate)
      return true;
    else
      return false;
  }

  cancel(LeaveId: number) {

    this.dialogService.openConfirmDialog().afterClosed().subscribe(res => {
      if (res) {
        this.service.cancelLeave(LeaveId)
          .subscribe((result: any) => {
            if (result.data) {
              this.getLeaveList(this.Year);
              this.getLeaveList(this.curYear);
              this.getUserLeave(this.UserId);
              this._snackBar.open('Leave Cancel successfully!', 'OK', {
                panelClass: 'my-custom-snackbar',
                duration: 2 * 1000,

              });

            } else {
              this._snackBar.open('Can not cancel record', 'OK', {
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

  getUserName(userId: number) {
    
    this.service.getUser(userId).subscribe((result: any) => {
      if (result.success) {
        this.userAccount = result.data;
        this.changeDetectorRefs.detectChanges();
      } else { }
    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
  }

  onEmployeeChange(event: any) {
    this.selectedUserId = event;
    this.service.getLeave(this.selectedUserId, this.selectedYear).subscribe((result: any) => {
      if (result.success) {
        
        this.leaveList = result.data;
        this.getUserLeave(this.UserId);
        console.log(this.userAccount);
      } else {
        console.log();
      }
    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
  }


  getUserLeave(userId: Number) {
    this.service.getUserLeaveInfo(userId).subscribe((result: any) => {
      if (result.success) {
        this.userCountList = result.data;

        console.log(this.userCountList);
      }
    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
  }

  dateChangeEvent(event: any) {
    this.minDate = event.value;
  }
}




