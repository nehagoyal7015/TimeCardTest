import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Router} from '@angular/router';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { LoaderServiceService } from 'src/app/utils/loader/loader-service.service';
import { UserGroupService } from 'src/app/Services/user-group.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-usergroup',
  templateUrl: './usergroup.component.html',
  styleUrls: ['./usergroup.component.scss']
})
export class UsergroupComponent implements OnInit {

  public addGroup = {groupName: '', shortDescription: ''}
  displayedColumns = ['groupName','shortDescription', 'domainId', 'groupId'];
  public groupId = '';
  domainId: number;
  
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  public groupTableList: MatTableDataSource<any>;


  constructor(private router: Router, private loaderService: LoaderServiceService, private userGroupService: UserGroupService,  private _snackBar: MatSnackBar) {
    
    
   }

  ngOnInit(): void {
    let currentUser = JSON.parse(localStorage.getItem('user') as string);
    this.domainId = currentUser.data.domainId;
    this.getGroupList();
    
  }

  /**
   * @name: AssignEmp
   * @description: Edit Emp 
   */
  AssignEmp(event:any, shortDescription:any) {
    this.userGroupService.setGroupData(shortDescription);
    this.router.navigate(['/home/Admin/security/group-access-control']);
  }

  /**
   * @name: delete Group Btn
   * @description: Delete Group Btn
   */
  deleteGroupBtn(groupId:any) {
    this.groupId = groupId;
    this.deleteGroup();
  }
  
  /**
   * @name: addGroupList
   * @description: add Group List
   */
   addGroupList(){
    this.loaderService.emitLoadingChangeEvent(true);
    this.userGroupService.AddGroup(this.addGroup, this.domainId).subscribe((result: any) => {
      if (result.success){
        this._snackBar.open('Record Saved Successfully!', 'Close', {
          panelClass: 'my-custom-snackbar',
          duration: 2 * 1000,
        });
        this.getGroupList();
        this.addGroup = {groupName: '', shortDescription: ''}
      } else {}
      this.loaderService.emitLoadingChangeEvent(false);
    },
    (error:any) =>{
      this.loaderService.emitLoadingChangeEvent(false);
       console.log(error.error.message); 
       
    });
  }
  /**
   * @name: getGroupList
   * @description: get Group List
   */
   getGroupList(){
    this.loaderService.emitLoadingChangeEvent(true);
    this.userGroupService.GetGroup(this.domainId).subscribe((result: any) => {
      if (result.success){
        // this.groupList = result.data;
        this.groupTableList = new MatTableDataSource(result.data);
        this.groupTableList.paginator = this.paginator;
        this.groupTableList.sort = this.sort;
      } else {}
      this.loaderService.emitLoadingChangeEvent(false);
    },
    (error:any) =>{
      this.loaderService.emitLoadingChangeEvent(false);
       console.log(error.error.message); 
       
    });
  }

  /**
   * @name: deleteGroup
   * @description: delete Group
   */
   deleteGroup(){ 
    this.loaderService.emitLoadingChangeEvent(true);
    this.userGroupService.DeleteGroup(this.groupId).subscribe((result: any) => {
      if (result.success){
        this.getGroupList()
      } else {}
      this.loaderService.emitLoadingChangeEvent(false);
    },
    (error:any) =>{
      this.loaderService.emitLoadingChangeEvent(false);
       console.log(error);  
    });
  }
}
