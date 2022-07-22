import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HomeService } from 'src/app/Services/home.service';
import { LoaderServiceService } from 'src/app/utils/loader/loader-service.service';

@Component({
  selector: 'app-myprofile',
  templateUrl: './myprofile.component.html',
  styleUrls: ['./myprofile.component.scss']
})
export class MyprofileComponent implements OnInit {

public EmployeeInformation ={
  phoneNumber:  '',
  isActive: true,
  fatherName:  '',
  motherName:  '',
  alternateNumber: '',
  personalEmail:  '',
  skypeId :  '',
  address1: '',
  address2:  '',
  isMaritalstatus: false,
  spouseName:  '',
  spousePhone:  '',
  spouseEmail:  '',
  userName:'',
  userEmail:'',
  skills: ['Design', 'Build', 'Testing', 'Deploy', 'Support'],
  skillList: ['Design', 'Build', 'Testing', 'Deploy', 'Support'],
};
public userId:any;
durationInSeconds = 15;
  @ViewChild('editProfile') editProfile: TemplateRef<any>;
  constructor(private dialog: MatDialog,private loaderService: LoaderServiceService ,private service:HomeService, private _snackBar: MatSnackBar,) { }

  ngOnInit(): void {
    let currentuser = JSON.parse(localStorage.getItem('user') as string);
    this.getProfileInfo(currentuser.data.userId);
    this.userId = (currentuser.data.userId);
    
  }

    /**
   * @name: closePopUP
   * @description: closePopUP
   */
     closePopUP(): void {
      this.dialog.closeAll();
    }
  /**
   * @name: editProfilePopUp
   * @description: add Project PopUp
   */
   editProfilePopUp(): void {
    this.dialog.open(this.editProfile, {
       width: '800px'
      
    });
  }
  getProfileInfo(userId:number){
    this.service.getEmployeeInfo(userId).subscribe( (result:any) => {
      if(result.success) {
        this.EmployeeInformation = result.data;
        console.log(this.EmployeeInformation); 
        
      } else {}        
    }, (error)  => {
      console.log(error.error.message, error.error.messageType);
    }
    )
  }
  editProfileInfo(){      
    this.service.editProfile(this.EmployeeInformation).subscribe( (result:any) => {
      if(result.success) {
        this.closePopUP(); 
        this._snackBar.open('Record Updated Successfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      } else {
        this._snackBar.open('Cant not be Edit Success', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      } 
    }, (error)  => {
      console.log(error.error.message, error.error.messageType);
    }
    )
  }


}
