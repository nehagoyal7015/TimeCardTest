import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/Services/employee.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DialogService } from 'src/app/Shared/dialog.service';
import { NotificationService } from 'src/app/Shared/notification.service';
import { Router } from '@angular/router';
import { LoaderServiceService } from 'src/app/utils/loader/loader-service.service';
import { MailService } from 'src/app/Services/mail.service';



export interface Fruit {
  name: string;
}
@Component({
  selector: 'app-employee-manage',
  templateUrl: './employee-manage.component.html',
  styleUrls: ['./employee-manage.component.scss']
})

export class EmployeeManageComponent implements OnInit {

  public pkID: any = '';
  public EmployeeForm : any= {
    firstName: '',
    lastName: '',
    phoneNumber: '',
    dateOfBirth: '',
    desgination: '',
    joiningDate: '',
    empId: '',
    isActive: true,
    fatherName: '',
    motherName: '',
    alternateNumber: '',
    personalEmail: '',
    skypeId: '',
    address1: '',
    address2: '',
    isMaritalStatus: false,
    spouseName: '',
    spousePhone: '',
    spouseEmail: '',
    userName: '',
    password: '',
    userEmail: '',
    ssoId: 0,
    domainId: '',
    skillList: ['Design', 'Build', 'Testing', 'Deploy', 'Support'],
  }
  public domainId: any;
  profile: File;
  public showPassword: boolean = false;
  addOnBlur = true;
  durationInSeconds = 15;
  // public addEmpSkilList = {
  //   skills: ['Design', 'Build', 'Testting', 'Deploy', 'Support']
  // }

  @Output() public OnUpload = new EventEmitter();
  constructor(private router: Router, public dialog: MatDialog, private formbulider: FormBuilder, public service: EmployeeService, public mailService: MailService,
    private _snackBar: MatSnackBar, private dialogService: DialogService, private notificationservice: NotificationService, private loaderService: LoaderServiceService) {



  }

  ngOnInit(): void {
    
    let currentuser = JSON.parse(localStorage.getItem('user') as string);
    this.domainId = (currentuser.data.domainId);
    this.pkID = this.service.getPkID();
    if (this.pkID) {
      this.getEmpDetails();
    }

  }

  public togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  onFileChange(event: any) {
    this.profile = event.target.files.item(0);
  }

  eventMetter() {
    this.pkID = this.service.getPkID();

  }
  AddEmp() {
    if (!this.pkID) {
      this.newAddEmp();
    } else {
      this.editEmployee();
    }
  }
  cancelEmp() {
    this.router.navigate(['/home/Admin/empList']);
  }
  newAddEmp() {
    // var userEmail = { userEmail: this.EmployeeForm.userEmail };
    this.EmployeeForm.domainId = this.domainId;
    if(!this.EmployeeForm.firstName){
      this.EmployeeForm.firstName = null;
    }
    if(!this.EmployeeForm.dateOfBirth){
      this.EmployeeForm.dateOfBirth = null;
    }
    if(!this.EmployeeForm.firstName){
      this.EmployeeForm.firstName = null;
    }
    if(!this.EmployeeForm.joiningDate){
      this.EmployeeForm.joiningDate = null;
    }
    if(!this.EmployeeForm.fatherName){
      this.EmployeeForm.fatherName = null;
    }
    if(!this.EmployeeForm.closeDate){
      this.EmployeeForm.closeDate = null;
    }
    this.service.addEmployee(this.profile, this.EmployeeForm).subscribe((result: any) => {
      if (result.data) {
        // this.mailService.mailSender(JSON.parse(JSON.stringify(userEmail))).subscribe();
        this._snackBar.open('Record Saved Successfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
        setTimeout(() => {
          setTimeout(() => {
            this.router.navigate(['/home/Admin/proj-setp']);
          });
        }, 1000);
      
        
      } else {
        this._snackBar.open('UnsucessFull', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      }
    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
  }
  /**
   * @name: editEmployee
   * @description: edit Employee
   */
  editEmployee() {
    Object.assign(this.EmployeeForm, { pK_Id: this.pkID });
    this.loaderService.emitLoadingChangeEvent(true);
    this.service.editHoliday(this.profile, this.EmployeeForm).subscribe((result: any) => {
      if (result.success) {
        this.pkID = '';
        this._snackBar.open('Record Saved Successfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      } else {
        this._snackBar.open('Cant not be Edit Success', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }

  getEmpDetails() {
    this.loaderService.emitLoadingChangeEvent(true);
    this.service.getEmpDetails(this.pkID).subscribe((result: any) => {
      if (result.success) {
        this.EmployeeForm = result.data;
        console.log(this.EmployeeForm);
      } else { }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }

  add(event: MatChipInputEvent): void {
    const value: any = (event.value || '').trim();
    if (value) {
      this.EmployeeForm.skillList.push(value);
    }
    event.chipInput!.clear();
  }
  remove(fruit: any): void {
    const index = this.EmployeeForm.skillList.indexOf(fruit);
    if (index >= 0) {
      this.EmployeeForm.skillList.splice(index, 1);
    }
  }

}
