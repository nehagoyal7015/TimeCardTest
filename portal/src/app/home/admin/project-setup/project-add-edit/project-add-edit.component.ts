import { Component, OnInit } from '@angular/core';
import { ProjectSetupService } from './../../../../Services/project-setup.service';
import { LoaderServiceService } from './../../../../utils/loader/loader-service.service';
import { MatChipInputEvent } from '@angular/material/chips';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';

import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-project-add-edit',
  templateUrl: './project-add-edit.component.html',
  styleUrls: ['./project-add-edit.component.scss']
})
export class ProjectAddEditComponent implements OnInit {

  public clientList: any = [{ key: 1, value: null, description: '' }];
  public EmployeeList: any = [{ key: 1, value: null, description: '' }];
  addEmp = false;
  saveBTN = '';
  durationInSeconds = 15;

  public taskDetailList: any[] = [{
    task: '',
    taskBudgetCost: 0,
    taskBudgetHours: 0,
    isArchive:false,
  }];
  isStatusArchieved: boolean = false;

  public addProjectForm: any = {
    clientId: 0,
    userId: 0,
    managerId: 0,
    projectId: 0,
    name: '',
    description: '',
    budgetCost: 0,
    budgetHours: 0,
    startDate: '',
    estimateCompletion: '',
    closeDate: '',
    isActive: true,
    ownerShips: '',
    actualBudget: 0,
    isBillable: false,
    domainId: '',
    taskBudgetCost: 0,
    taskBudgetHours: 0,
    taskDataList: [] as any,
    userInfo:[] as any
  }
  public getProjectId: any;
  public getClientId: any;

  public domainId: any;

  empList: any = [{key:1}]


  constructor(
    private projectSetupService: ProjectSetupService,
    private router: Router,
    private _snackBar: MatSnackBar,
    private loaderService: LoaderServiceService) {

    this.getProjectId = this.projectSetupService.getProjIdID();
    this.getClientId = this.projectSetupService.getClientID();

    if (this.getProjectId === 0) {
      this.saveBTN = 'Add';
      this.getEmployee();

    } else {
      this.saveBTN = 'Update';
      this.getEmployee();
      this.getProjectUpdateList();
    }
  }

  ngOnInit(): void {
    let currentuser = JSON.parse(localStorage.getItem('user') as string);
    this.domainId = (currentuser.data.domainId);
    this.getClientList();
    this.getEmployeeList();
  }

  addTask() {
    this.taskDetailList.push({
      taskId: 0,
      task: '',
      taskBudgetCost: 0,
      taskBudgetHours: 0,
      isArchive: false
    });
  }
  
  removeAddress(i: number) {
    this.taskDetailList.splice(i, 1);
  }

  /**
   * @name: addSkilList
   * @description: add Skil List
   */
  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    // Add our fruit
    if (value) {
      this.addProjectForm.taskList.push(value);
    }
    // Clear the input value
    event.chipInput!.clear();
  }


  eventCheck(event: any, key: any): void {
    
    this.addProjectForm.userInfo = [];
    if (event.checked === true) {
      for (let i = 0; i < this.empList.length; i++) {
        if (this.empList[i].key === key) {
          this.empList[i].isActive = true;
        }
        if (this.empList[i].isActive === true) {
          this.addProjectForm.userInfo.push({'userId': this.empList[i].key});
        }
      }
    } else {
      for (let i = 0; i < this.empList.length; i++) {
        if (this.empList[i].key === key) {
          this.empList[i].isActive = false;
        }
        if (this.empList[i].isActive === true) {
          this.addProjectForm.userInfo.push({'userId': this.empList[i].key});
        }
      }
    }
  }

  selectCheck(event: any) {
    
    this.addProjectForm.userInfo = [];
    if (event.checked === true) {
      for (let i = 0; i < this.empList.length; i++) {
        this.empList[i].isActive = true;
        if (this.empList[i].isActive === true) {
          this.addProjectForm.userInfo.push(this.empList[i].key);
        }
      }       
    } else {
      for (let i = 0; i < this.empList.length; i++) {
        this.empList[i].isActive = false;
        this.addProjectForm.userInfo = [];
      }
    }
  }
  /**
     * @name: removeSkilList
     * @description: remove Skil List
     */


  remove(fruit: any): void {
    const index = this.addProjectForm.taskList.indexOf(fruit);
    if (index >= 0) {
      this.addProjectForm.taskList.splice(index, 1);
    }
  }
  /**
     * @name: AddEditProject
     * @description: Add Edit Project
     */
  AddEditProject() {
    if (this.getProjectId === 0) {
      this.newAddProject();
    } else {
      this.updateProject();
      // this.isStatusArchieved == true;
    }
  }

  backPage() {
    this.router.navigate(['/home/Admin/proj-setp']);
  }

  /**
   * @name: newAddProject
   * @description: new Add Project
   */
  newAddProject() {
    
    this.addProjectForm.taskList = this.taskDetailList;
    this.addProjectForm.domainId = this.domainId;
    if (!this.addProjectForm.startDate) {
      this.addProjectForm.startDate = null;
    }
    if (!this.addProjectForm.estimateCompletion) {
      this.addProjectForm.estimateCompletion = null;
    }
    if (!this.addProjectForm.closeDate) {
      this.addProjectForm.closeDate = null;
    }
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.postAddNewproject(this.addProjectForm).subscribe((result: any) => {
      if (result.success) {
        this._snackBar.open('Project Added Successfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
        this.selectCheck(false);

      }
      else {
        this._snackBar.open('Project Unsuccessfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      }
      this.loaderService.emitLoadingChangeEvent(false);

    },
      (error: any) => {
        this.loaderService.emitLoadingChangeEvent(false);
        console.log(error.error.message);
      });
  }
  /**
   * @name: updateProject
   * @description: update Project
   */
  updateProject() {
    
    this.addProjectForm.domainId = this.domainId;
    if (!this.addProjectForm.startDate) {
      this.addProjectForm.startDate = null;
    }
    if (!this.addProjectForm.estimateCompletion) {
      this.addProjectForm.estimateCompletion = null;
    }
    if (!this.addProjectForm.closeDate) {
      this.addProjectForm.closeDate = null;
    }
   
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.putUpdateproject(this.addProjectForm).subscribe((result: any) => {
      
      if (result.success) {
        this._snackBar.open('Project Update Successfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
        this.selectCheck(false);
      } else {
        this._snackBar.open('Update Unsuccessfully!', 'ok', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      }

      this.loaderService.emitLoadingChangeEvent(false);

    },

      (error: any) => {
        this.loaderService.emitLoadingChangeEvent(false);
        console.log(error.error.message);
      });
  }

  /**
   * @name: getClientList
   * @description: get Client List
   */

  getClientList() {
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.getClientList().subscribe((result: any) => {
      if (result.success) {
        this.clientList = result.data;
      } else { }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    });
  }


  getEmployeeList() {
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.getEmployeeListAll().subscribe((result: any) => {
      if (result.success) {
        this.EmployeeList = result.data;
      } else { }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    });
  }
  /**
  * @name: getEmployee
  * @description: get Employee
  */
  getEmployee() {

    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.getEmployee().subscribe((result: any) => {
      if (result.success) {
        this.empList = result.data;
        for (let i = 0; i < result.data.length; i++) {
          this.empList[i].isActive = false
        }
      } else { }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    });
  }

  /**
* @name: getProjectUpdateList
* @description: get Project Update List
*/
  getProjectUpdateList() {

    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.getProjectList(this.getProjectId).subscribe((result: any) => {
      if (result.success) {

        this.addProjectForm = result.data;
        this.taskDetailList = result.data.taskList;
        this.addProjectForm.projectId = this.getProjectId;
        this.addProjectForm.clientId = this.getClientId;
        this.isStatusArchieved = result.data.taskList;


        let emplList = result.data.userInfo;
        for (let i = 0; i < emplList.length; i++) {
          // this.addProjectForm.allEmp.push(result.data.allEmp[i].userId); 

          for (let j = 0; j < this.empList.length; j++) {
            if (this.empList[j].key === emplList[i].userId) {
              this.empList[j].isActive = true;
            }

          }
        }

      } else { }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    });
  }

  archeive(projectId: any,isArcheive:boolean) {
    
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.CancelArcheive(projectId,isArcheive).subscribe((result: any) => {
      if (result.success) {

      } else { }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    });
  }

}
