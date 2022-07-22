import { Component, OnInit } from '@angular/core';
import { EmployeeService } from './../../../../Services/employee.service';
import { LoaderServiceService } from './../../../../utils/loader/loader-service.service';
import { UserGroupService } from 'src/app/Services/user-group.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  public empDetails:any = {};
  public domainId: number;
  public availableGroupsColumns: string[] = ['groupName', 'shortDescription', 'assignBtn'];
  public availableGroupsRow:any = [];

  public assignedGroupsColumns: string[] = ['groupName', 'shortDescription', 'deleteBtn'];
  public assignedGroupsRow:any = [];
  constructor(public empService: EmployeeService, private loaderService: LoaderServiceService, private userGroupService: UserGroupService, private _snackBar: MatSnackBar) { 
    let currentUser = JSON.parse(localStorage.getItem('user') as string);
    this.domainId = currentUser.data.domainId;
    this.empDetails = this.userGroupService.empListDetails;
    this.getUserGroupAvailable();
    this.getUserGroupAssigned();
  }

  ngOnInit(): void {
    let currentUser = JSON.parse(localStorage.getItem('user') as string);
    this.domainId = currentUser.data.domainId;
  }


  addRow(row:any, rowI:any ) {
    const newRow = {domainId: row.domainId, groupId: row.groupId, groupName : row.groupName, shortDescription: row.shortDescription}
    this.assignedGroupsRow = [...this.assignedGroupsRow, newRow];
    if (rowI > -1) {
      this.availableGroupsRow.splice(rowI, 1);
      this.availableGroupsRow = [...this.availableGroupsRow]; // new ref!
    }
  }

  removeData(rowid:any, row:any) {
    if (rowid > -1) {
      this.assignedGroupsRow.splice(rowid, 1);
      this.assignedGroupsRow = [...this.assignedGroupsRow]; // new ref!
    }
    const newRow = {domainId: row.domainId, groupId: row.groupId, groupName : row.groupName, shortDescription: row.shortDescription}
    this.availableGroupsRow = [...this.availableGroupsRow, newRow];
  }

   /**
   * @name: getUserGroupAvailable
   * @description: get User Group Available
   */
    getUserGroupAvailable() {
      this.loaderService.emitLoadingChangeEvent(true);
      this.userGroupService.getUserGroupAvailable(this.empDetails.userId, this.domainId).subscribe((result: any) => {
        if (result.success) {
          this.availableGroupsRow = result.data;
        } else { }
        this.loaderService.emitLoadingChangeEvent(false);
      },
        (error: any) => {
          this.loaderService.emitLoadingChangeEvent(false);
          console.log(error.error.message);
        });
    }

     /**
   * @name: getUserGroupAssigned
   * @description: get User Group Assigned
   */
      getUserGroupAssigned() {
        this.loaderService.emitLoadingChangeEvent(true);
        this.userGroupService.getUserGroupAssigned(this.empDetails.userId).subscribe((result: any) => {
          if (result.success) {
            this.assignedGroupsRow = result.data;
          } else { }
          this.loaderService.emitLoadingChangeEvent(false);
        },
          (error: any) => {
            this.loaderService.emitLoadingChangeEvent(false);
            console.log(error.error.message);
          });
      }
       /**
   * @name: getUserGroupSave
   * @description: get User Group Save
   */
        getUserGroupSave() {
          
          let assignedList:any = [];
          for (let i = 0; i < this.assignedGroupsRow.length; i++) {
            assignedList.push({userId: this.empDetails.userId, groupId: this.assignedGroupsRow[i].groupId})
          }
          this.loaderService.emitLoadingChangeEvent(true);
          this.userGroupService.postUserGroupSave(assignedList).subscribe((result: any) => {
            if (result.success) {
              this._snackBar.open('Record Saved Successfully!', 'Close', {
                panelClass: 'my-custom-snackbar',
                duration: 2 * 1000,
              });
              this.getUserGroupAssigned();
              this.getUserGroupAvailable();
            } else { }
            this.loaderService.emitLoadingChangeEvent(false);
          },
            (error: any) => {
              this.loaderService.emitLoadingChangeEvent(false);
              console.log(error.error.message);
            });
        }

}
