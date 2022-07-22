import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReportService } from 'src/app/Services/report.service';
import { LeaveServiceService } from 'src/app/Shared/leave-service.service';
import { LoaderServiceService } from 'src/app/utils/loader/loader-service.service';
import { animate, state, style, transition, trigger } from '@angular/animations';
import html2canvas from 'html2canvas';
import jspdf from 'jspdf';
import { NgDynamicBreadcrumbService } from 'ng-dynamic-breadcrumb';

@Component({
  selector: 'app-timesheetprojectreport',
  templateUrl: './timesheetprojectreport.component.html',
  styleUrls: ['./timesheetprojectreport.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class TimesheetprojectreportComponent implements OnInit {

  public userID: any = '';
  public projectID: any = '';
  public projectId:any;
  public userId:any;
  displayedColumns1: string[] = ['employeeName','projectTotal', 'lastSubmittedOn', 'total'];
  constructor(private loaderService: LoaderServiceService,private router: Router, private reportService: ReportService, private service: LeaveServiceService) { }
 public UserInformation : any=[];

  ngOnInit(): void {
    debugger
    this.projectID = this.reportService.getPkID(); 
    this.userID = this.reportService.getUserID();
    this.getProjectDetail();
  }

  


  savePkId() {
    this.reportService.setPkID(this.projectId,this.userId);    
  }
  backPage(){
    this.router.navigate(['/home/report/time-sheet-report']);
  }
  getProjectDetail() {
    let data = {
      'ProjectId' : this.projectID,
      'userId': this.userID
    }
    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.ProjectDetail(data).subscribe((result:any) => {
      if(result.data) {
        this.UserInformation = result.data;
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    },(error) => {
      console.log(error);
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }

  captureScreen() {
    var data = document.getElementById('EmployeeReport');
    if (data) {
      html2canvas(data).then(canvas => {
        var imgWidth = 208;
        var pageHeight = 295;
        var imgHeight = canvas.height * imgWidth / canvas.width;
        var heightLeft = imgHeight;
        const contentDataURL = canvas.toDataURL('image/png')
        let pdf = new jspdf('p', 'mm', 'a4'); // A4 size page of PDF  
        var position = 0;
        pdf.addImage(contentDataURL, 'PN   G', 0, position, imgWidth, imgHeight)
        pdf.save('EmployeeReport.pdf'); // Generated PDF   
      });
    }
  }
}
