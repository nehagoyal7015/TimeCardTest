import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';
import { SelectionModel } from '@angular/cdk/collections';
import { LoaderServiceService } from 'src/app/utils/loader/loader-service.service';
import { UserGroupService } from 'src/app/Services/user-group.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-group-access-control',
  templateUrl: './group-access-control.component.html',
  styleUrls: ['./group-access-control.component.scss']
})
export class GroupAccessControlComponent implements OnInit {

  @ViewChild(MatTable) table: MatTable<any>;
  public addGroup = { groupName: '', shortDescription: '' }
  public domainId = '';
  public groupId = '';
  public groupList: any = {};
  public displayedColumns: string[] = ['accessName', 'assignButton'];
  public availablePermissions: any = [];
  public assignedPermissionsColumns: string[] = ['accessName', 'isCreateAllowed', 'isReadAllowed', 'isUpdateAllowed', 'isDeleteAllowed', 'remove'];
  public assignedPermissionsRow: any = [];
  selection = new SelectionModel<any>(true, []);

  constructor(private router: Router, private loaderService: LoaderServiceService, private userGroupService: UserGroupService, private _snackBar: MatSnackBar) {
    
    this.groupList = this.userGroupService.getGroupData()
    this.domainId = this.groupList.domainId;
    this.groupId = this.groupList.groupId;
    this.addGroup = { groupName: this.groupList.groupName, shortDescription: this.groupList.shortDescription }
  }

  ngOnInit(): void {
    let currentUser = JSON.parse(localStorage.getItem('user') as string);
    this.domainId = currentUser.data.domainId;
    this.getGroupAccessControlAssigned();
    this.getGroupAccessControlAvialable();
  }

  /**
     * @name: selectChek
     * @description: Assigned Permissions checkbox
     */
  selectChek(event: any, i: any, colName: any) {
    
    this.assignedPermissionsRow.forEach((element: any, index: any, val: any) => {
      if (index === i) {
        if (colName === "isCreateAllowed") {
          element.isCreateAllowed = event.checked;
        } else if (colName === "isReadAllowed") {
          element.isReadAllowed = event.checked;
        } else if (colName === "isUpdateAllowed") {
          element.isUpdateAllowed = event.checked;
        } else if (colName === "isDeleteAllowed") {
          element.isDeleteAllowed = event.checked;
        }
      }
    });
    if (event.checked === true) {
      // alert('Hi');
    }
  }
  /**
    * @name: addRow
    * @description: Available Permissions Add
    */
  addRow(row: any, rowI: any) {
    
    const newRow = { groupId: this.groupId, accessControlId: row.accessControlId, accessName: row.accessName, isCreateAllowed: false, isReadAllowed: false, isUpdateAllowed: false, isDeleteAllowed: false }
    this.assignedPermissionsRow = [...this.assignedPermissionsRow, newRow];
    if (rowI > -1) {
      this.availablePermissions.splice(rowI, 1);
      this.availablePermissions = [...this.availablePermissions]; // new ref!
    }
  }
  /**
   * @name: removeData
   * @description: Assigned Permissions Remove
   */
  removeData(rowid: any, row: any) {
    if (rowid > -1) {
      this.assignedPermissionsRow.splice(rowid, 1);
      this.assignedPermissionsRow = [...this.assignedPermissionsRow]; // new ref!
    }
    const newRow = { accessControlId: row.accessControlId, accessName: row.accessName, description: '', possibleActions: '' }
    this.availablePermissions = [...this.availablePermissions, newRow];
  }
  /**
   * @name: cancelBtn
   * @description: back router nav
   */
  cancelBtn() {
    this.router.navigate(['/home/Admin/security/group']);
  }

  /**
   * @name: updateGroupAcces
   * @description: update Group Acces
   */
  updateGroupAcces() {
    this.updateGroupDescription();
    this.addGroupAccessControl();
  }

  /**
   * @name: updateGroupDescription
   * @description: update Group Description
   */
  updateGroupDescription() {
    const updateGroupName = {
      domainId: this.domainId,
      groupId: this.groupId,
      groupName: this.addGroup.groupName,
      shortDescription: this.addGroup.shortDescription
    }
    this.loaderService.emitLoadingChangeEvent(true);
    this.userGroupService.EditGroup(updateGroupName).subscribe((result: any) => {
      if (result.success) {
      } else { }
      this.loaderService.emitLoadingChangeEvent(false);
    },
      (error: any) => {
        this.loaderService.emitLoadingChangeEvent(false);
        console.log(error.error.message);

      });
  }
  /**
    * @name: addGroupAccessControl
    * @description: add Group Access Control
    */
  addGroupAccessControl() {
  
    this.loaderService.emitLoadingChangeEvent(true);
    console.log(this.assignedPermissionsRow);
    this.userGroupService.AddGroupAccessControl(this.groupId, this.assignedPermissionsRow).subscribe((result: any) => {
      if (result.success) {
        this._snackBar.open('Record Saved Successfully!', 'Close', {
          panelClass: 'my-custom-snackbar',
          duration: 2 * 1000,
        });
        this.getGroupAccessControlAssigned();
        this.getGroupAccessControlAvialable();
      } else { }
      this.loaderService.emitLoadingChangeEvent(false);
    },
      (error: any) => {
        this.loaderService.emitLoadingChangeEvent(false);
        console.log(error.error.message);

      });
  }
  /**
   * @name: getGroupAccessControlAssigned
   * @description: get Group Access Control Assigned
   */
  getGroupAccessControlAssigned() {
    this.loaderService.emitLoadingChangeEvent(true);
    console.log(this.groupId);
    this.userGroupService.getGroupAccessControAssigned(this.groupId).subscribe((result: any) => {
      if (result.success) {
        this.assignedPermissionsRow = result.data;
        console.log(result.data);
      } else { }
      this.loaderService.emitLoadingChangeEvent(false);
    },
      (error: any) => {
        this.loaderService.emitLoadingChangeEvent(false);
        console.log(error.error.message);

      });
  }
  /**
   * @name: getGroupAccessControlAvialable
   * @description: get Group Access Control Avialable
   */
  getGroupAccessControlAvialable() {
    this.loaderService.emitLoadingChangeEvent(true);
    console.log(this.groupId);
    this.userGroupService.getGroupAccessControlAvialable(this.groupId).subscribe((result: any) => {
      if (result.success) {
        this.availablePermissions = result.data;
      } else { }
      this.loaderService.emitLoadingChangeEvent(false);
    },
      (error: any) => {
        this.loaderService.emitLoadingChangeEvent(false);
        console.log(error.error.message);
      });
  }
}
