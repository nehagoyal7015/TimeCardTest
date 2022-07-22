import {AfterViewInit, Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import {MatSort} from '@angular/material/sort';
import {MatChipInputEvent} from '@angular/material/chips';
import { ProjectSetupService} from './../../../Services/project-setup.service';
import { LoaderServiceService } from './../../../utils/loader/loader-service.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-project-setup',
  templateUrl: './project-setup.component.html',
  styleUrls: ['./project-setup.component.scss']
})
export class ProjectSetupComponent implements OnInit {

  @ViewChild('addProject') addProject: TemplateRef<any>;
  @ViewChild('addClient') addClient: TemplateRef<any>;
  
  durationInSeconds = 15;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  public clientList:any = [{key: 1, value: null, description: ''}]; 

  public PopupFormName = '';

  public addProjectForm = {
    clientId: 0,
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
    task: ['Design', 'Build', 'Testting', 'Deploy', 'Support']
  }
  public newAddClient = {
    clientId: 0,
    clientName: '',
    isActive: true,
    country: '',
    contactNo: '',
    emailAddress: '',
    website: '',
    registrationNumber: 0,
    address: ''
  }
  public clientAndProductDetails:any = [];
  

  displayedColumns = ['name', 'ownerShips', 'budgetHours', 'edit'];
  
  projectTableList = new MatTableDataSource(this.clientAndProductDetails);
   
    
    
  constructor(
      private dialog: MatDialog, 
      private projectSetupService: ProjectSetupService, 
      private router: Router,
      private _snackBar: MatSnackBar,
      private loaderService: LoaderServiceService) {
      }

  ngOnInit(): void {
    this.getClientList();
    this.getClientListDetails();
  }

  projectDetails(event:any, projectId:any, clientId:any){
    this.router.navigate(['/home/Admin/proj-setp/project-details']);
    this.projectSetupService.setProjIdID(projectId, clientId);
  }
  /**
   * @name: newAddProjectpopup
   * @description: newAddProjectpopup
   */
  
   newAddProjectpopup() {
     this.router.navigate(['/home/Admin/proj-setp/project-add-edit']);
     this.projectSetupService.setProjIdID(0, 0);
  }
  /**
   * @name: editProject
   * @description: edit Project PopUp
   */

  editProject(event:any, projectId:any, clientId:any ) {
    this.router.navigate(['/home/Admin/proj-setp/project-add-edit']);
    this.projectSetupService.setProjIdID(projectId, clientId);
  }
  /**
   * @name: getRowData
   * @description: get Row Data
   */
  getRowData(row:any, event:any){
    this.addProjectForm.name = row.name;
    this.addProjectForm.budgetCost = row.budgetHours;
    this.addProjectForm.ownerShips = row.ownerShips;
  }
  /**
   * @name: newAddClientpopup
   * @description: new Add Client popup
   */
  
   newAddClientpopup() {
    this.PopupFormName = 'New Client';
    this.newAddClient = { clientId: 0, clientName: '', isActive: true, country: '', contactNo: '', emailAddress: '', website: '', registrationNumber: 0, address: ''}
    this.addClientPopUp();
  }
  /**
   * @name: editProject
   * @name: editProject
   * @description: edit Project PopUp
   */

  updateClientPopup(clientDetails:any) {
    this.PopupFormName = 'Edit Client';
    this.newAddClient.clientName = clientDetails.clientName;
    this.newAddClient.clientId = clientDetails.clientId;
    this.newAddClient.contactNo = '';
    this.newAddClient.emailAddress = '';
    this.newAddClient.website = '';
    this.newAddClient.address = '';
    this.newAddClient.country = '';
    this.addClientPopUp();
  }

/**
   * @name: addClientPopUp
   * @description: add Client PopUp
   */
  addClientPopUp(): void {
    this.dialog.open(this.addClient, {
       width: '600px'
    });
  }
  /**
   * @name: closePopUP
   * @description: closePopUP
   */
   closePopUP(): void {
    this.dialog.closeAll();
  }
   /**
   * @name: tableDataSort
   * @description: table Data Sort
   */
  ngAfterViewInit() {
    this.projectTableList.sort = this.sort;
  }
 /**
   * @name: tableApplyFilter
   * @description: table Apply Filter
   */
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.projectTableList.filter = filterValue.trim().toLowerCase();
  }

  /**
   * @name: addSkilList
   * @description: add Skil List
   */
  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    // Add our fruit
    if (value) {
      this.addProjectForm.task.push(value);
    }
    // Clear the input value
    event.chipInput!.clear();
  }
/**
   * @name: removeSkilList
   * @description: remove Skil List
   */
  remove(fruit: any): void {
    const index = this.addProjectForm.task.indexOf(fruit);
    if (index >= 0) {
      this.addProjectForm.task.splice(index, 1);
    }
  }
/**
   * @name: AddEditProject
   * @description: Add Edit Project
   */
 AddEditProject(){
    if(this.addProjectForm.projectId === 0){
      this.newAddProject();
    } else {
      this.updateProject();
    }
  }
  /**
   * @name: saveUpdateClient
   * @description: add Update Client
   */
  saveUpdateClient(){
    if(this.newAddClient.clientId === 0){
      this.addNewClient();
    } else {
      this.clientUpdate();
    }
  }
  /**
   * @name: newAddProject
   * @description: new Add Project
   */
  newAddProject(){
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.postAddNewproject(this.addProjectForm).subscribe((result: any) => {
      if (result.success){
        this.closePopUP();
        this.getClientListDetails();
      } else {}
      this.loaderService.emitLoadingChangeEvent(false);
      this.closePopUP();
    },
    (error:any) =>{
      this.loaderService.emitLoadingChangeEvent(false);
       console.log(error.error.message); 
       this.closePopUP();
    });
  }
  /**
   * @name: updateProject
   * @description: update Project
   */
   updateProject(){
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.putUpdateproject(this.addProjectForm).subscribe((result: any) => {
      if (result.success){
        this.closePopUP();
        this.getClientListDetails();
      } else {}
      this.loaderService.emitLoadingChangeEvent(false);
      this.closePopUP();
    },
    (error:any) =>{
      this.loaderService.emitLoadingChangeEvent(false);
       console.log(error.error.message); 
       this.closePopUP();
    });
  }
  
 /**
   * @name: addNewClient
   * @description: add New Client
   */
  addNewClient() {
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.addNewClient(this.newAddClient).subscribe((result:any) => {
        if(result.success) {
          this.closePopUP();
          this.getClientList();
          this.getClientListDetails();
          this._snackBar.open('Record Saved Successfully!', 'Close', {
            panelClass: 'my-custom-snackbar',
            duration: this.durationInSeconds * 1000,
          });
        } else {
          this._snackBar.open('Unsuccessfully!', 'Close', {
            panelClass: 'my-custom-snackbar',
            duration: this.durationInSeconds * 1000,
          });
        }
      this.loaderService.emitLoadingChangeEvent(false);
      this.closePopUP();
    }, (error) => {
      this.loaderService.emitLoadingChangeEvent(false);
      this.closePopUP();
    }
    );
  }
   /**
   * @name: clientUpdate
   * @description: client Update
   */
  clientUpdate(){
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.updateClient(this.newAddClient).subscribe((result: any) => {
      if(result.success) {
        this.closePopUP();
        this.getClientList();
        this.getClientListDetails();
        this._snackBar.open('Update Successfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      } else {
        this._snackBar.open('Unsuccessfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      }
      this.loaderService.emitLoadingChangeEvent(false);
      this.closePopUP();
    },(error) => {
      this.loaderService.emitLoadingChangeEvent(false);
      this.closePopUP();
    });
  }
  /**
   * @name: getClientList
   * @description: get Client List
   */
  getClientList(){
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.getClientList().subscribe((result: any) => {
      if(result.success) {
        this.clientList = result.data;
      } else {}
      this.loaderService.emitLoadingChangeEvent(false);
    },(error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    });
  }

  /**
   * @name: getClientList
   * @description: get Employee List
   */
   getEmpList(){
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.getEmployeList().subscribe((result: any) => {
      if(result.success) {
        this.clientList = result.data;
      } else {}
      this.loaderService.emitLoadingChangeEvent(false);
    },(error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    });
  }
   /**
   * @name: getClientListDetails
   * @description: get Client List Details
   */
    getClientListDetails(){
      this.clientAndProductDetails = [];
      this.loaderService.emitLoadingChangeEvent(true);
      this.projectSetupService.getClientListDetails().subscribe((result: any) => {
        if(result.success) {
          this.clientAndProductDetails = result.data;
          console.log(this.clientAndProductDetails);
        } else {}
        this.loaderService.emitLoadingChangeEvent(false);
      },(error) => {
        this.loaderService.emitLoadingChangeEvent(false);
      });
    }
}
