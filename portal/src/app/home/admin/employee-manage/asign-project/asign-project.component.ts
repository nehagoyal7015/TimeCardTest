import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AsignProjectServicesService } from 'src/app/Services/asign-project-services.service';
import { LoaderServiceService } from 'src/app/utils/loader/loader-service.service';
import { addProject, Clients, Projects } from './asignprojet';

@Component({
  selector: 'app-asign-project',
  templateUrl: './asign-project.component.html',
  styleUrls: ['./asign-project.component.scss']
})
export class AsignProjectComponent implements OnInit {
  public editable: boolean = false;
  panelOpenState = false;
  durationInSeconds = 2;
  public addAsignProjectForm:any = {
    userId:'',
    employeeName: '',
    empCode: '',
    email : '',
    phoneNumber : '',
  }
  public userID:any ='';
  public userId:any;
  public pkID: any = '';
  public pkId: any;
  projectList: any = [];
  asignProject : Clients[];
  allProject : Projects[];
  addProjectList:any = {
    userId:'',
    projectId:''
  }
  public projectId : any = [];
  constructor(private router: Router,private _snackBar: MatSnackBar,private loaderService: LoaderServiceService, private asignServices:AsignProjectServicesService ) { 
  }
  
  ngOnInit(): void {
    this.pkID = this.asignServices.getPkID(); 
    this.userID = this.asignServices.getUserID(); 
    this.getEmployee();
    this.getProject();
  }

  selecetedProject(){    
    this.asignProject.forEach((a:any) => {
      let count = 0;
      a.projectdata.forEach((b:any) => {
        this.projectList.forEach((m:any) => {
          if(b.projectId === m.projectId){
            count = (count + 1);
            b.isSelected = true;
          }
        });        
      });
      a.asignTotal = count;
    });  
  }

  savePkId() {
    this.asignServices.setPkID(this.pkId);    
  }
  saveUserId() {
    this.asignServices.setUserID(this.userId);
  }

  backPage(){
    this.router.navigate(['/home/Admin/empList']);
  }
  projectevent(event: any, projectId :any)
  {
    if(this.projectId.includes(projectId)){
      const index = this.projectId.findIndex((item : any)=> item == projectId);
      this.projectId.splice(index,1);
      console.log(index,projectId);
  }
 else{
      this.projectId.push(projectId);
    }
    console.log("projectItem",this.projectId);
  }
  
getEmployee(){ 
    this.loaderService.emitLoadingChangeEvent(true);
    this.asignServices.getEmployeeInfo(this.pkID).subscribe((result: any) => {
      if(result.success) {
        this.addAsignProjectForm = result.data; 
      } else {}
      this.loaderService.emitLoadingChangeEvent(false);
    },(error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    });
  }


  getProject(){ 
    this.loaderService.emitLoadingChangeEvent(true);
    this.asignServices.getProjectList(this.userID).subscribe((result: any) => {
      if(result.success) {
      this.projectList = result.data; 
      this.getAsignProject();
      } else {}
      this.loaderService.emitLoadingChangeEvent(false);
    },(error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    });
  }

  getAsignProject(){ 
    this.loaderService.emitLoadingChangeEvent(true);
    this.asignServices.getAsignProInfo().subscribe((result: any) => {
      if(result.success) {
        this.asignProject = result.data;
        this.allProject = result.data; 
        this.selecetedProject();
      } else {}
      this.loaderService.emitLoadingChangeEvent(false);
    },(error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    });
  }

  AsignNewProject(){ 
    this.addProjectList = [];
    this.asignProject.forEach((a:any) => {
      a.projectdata.forEach((b:any) => {
        if(b.isSelected === true){
          this.addProjectList.push({ userId: this.userID, projectId: b.projectId });
        }
      });
    }); 
    this.loaderService.emitLoadingChangeEvent(true);
    
    this.asignServices.AsignProject(this.addProjectList).subscribe((result: any) => {
      if (result.success){
        this._snackBar.open('Project Added Successfully!', 'Close', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      } else {
        this._snackBar.open('Record Unsuccessfully!', 'Close', {
          panelClass: 'my-custom-snackbar',
          duration: this.durationInSeconds * 1000,
        });
      } 
      this.getEmployee();
      this.getAsignProject();
      this.selecetedProject();
      this.getProject();
      this.loaderService.emitLoadingChangeEvent(false);
    },
    (error:any) =>{
      this.loaderService.emitLoadingChangeEvent(false);
       console.log(error.error.message); 
    });
  }


}
