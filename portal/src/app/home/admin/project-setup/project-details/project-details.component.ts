import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProjectSetupService } from 'src/app/Services/project-setup.service';
import { LoaderServiceService } from 'src/app/utils/loader/loader-service.service';


@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.scss']
})
export class ProjectDetailsComponent implements OnInit {

  @ViewChild('empEnteredDetails') empEnteredDetails: TemplateRef<any>;

  public projectId = '0';
  public prjectDetails:any = [];

  displayedColumns: string[] = ['emp', 'hours', 'total', 'lastenteredtime'];

  empDetailsColumns: string[] = ['taskDate', 'billableHours', 'billableHoursNote',];
  public empTableList = [];

  constructor( 
    private projectSetupService: ProjectSetupService, 
    private dialog: MatDialog, 
    private loaderService: LoaderServiceService) { 
      this.projectId = this.projectSetupService.getProjIdID();
      if(!this.projectId) {
        this.projectId = '0';
      }
    
      this.getProjectDetails();
     
    }

  ngOnInit(): void {
  }

  /**
   * @name: empEnteredDetailsPopUp
   * @description: add Client PopUp
   */
   addClientPopUp(): void {
    this.dialog.open(this.empEnteredDetails, {
       width: '800px'
    });
  }

  /**
   * @name: closePopUP
   * @description: closePopUP
   */
   closePopUP(): void {
    this.dialog.closeAll();
  }

  getProjectDetails(){
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.getProjectDetails(this.projectId).subscribe((result: any) => {
      if (result.success){
        this.prjectDetails = result.data;
        console.log(this.prjectDetails);
      } else {}
      this.loaderService.emitLoadingChangeEvent(false);
    },
    (error:any) =>{
      this.loaderService.emitLoadingChangeEvent(false);
       console.log(error.error.message); 
    });
  }

  empDetailsTime(TaskId:number,userId:number) {
    this.addClientPopUp();
    this.loaderService.emitLoadingChangeEvent(true);
    this.projectSetupService.empHourList(userId,TaskId).subscribe((result: any) => {
      if (result.success){
        this.empTableList = result.data;
       
      } else {}
      this.loaderService.emitLoadingChangeEvent(false);
    },
    (error:any) =>{
      this.loaderService.emitLoadingChangeEvent(false);
       console.log(error.error.message); 
    });
  }
}
