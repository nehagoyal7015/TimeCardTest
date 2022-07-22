import { Component, OnInit } from '@angular/core';
import { ReportService } from 'src/app/Services/report.service';
import { LoaderServiceService } from 'src/app/utils/loader/loader-service.service';
import html2canvas from 'html2canvas';
import jspdf from 'jspdf';
import { result } from 'lodash';
import { LeaveServiceService } from 'src/app/Shared/leave-service.service';


@Component({
  selector: 'app-leave-details',
  templateUrl: './leave-details.component.html',
  styleUrls: ['./leave-details.component.scss']
})
export class LeaveDetailsComponent implements OnInit {
  // public domainId = '';
  displayedColumns: string[] = ['date', 'employee', 'leaveType', 'reason','total'];
  public dataSource: any = [ ]
  
  public yearList:any = [];
  public employeeList:any = [{userId: 0, userName: 'All'}];


  public selectLeaveReport:any = {yearVal: 0, userName: '', date: '' }

  constructor(private loaderService: LoaderServiceService, private reportService: ReportService, private service: LeaveServiceService) {
    // let currentUser = JSON.parse(localStorage.getItem('user') as string);
    // this.domainId = currentUser.data.domainId;
   }

  ngOnInit(): void {
    this.getLeaveReport();
    this.getUserList();
    this.generateArrayOfYear();
  }

  getUserList() {
    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.getAllEmp().subscribe((result) => {
      if(result.data) {
        this.employeeList = result.data;
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    },(error)=> {
      console.log(error);
      this.loaderService.emitLoadingChangeEvent(false);
    })
  }

  generateArrayOfYear(){
    
    let max = new Date().getFullYear();
    let min = max -25;
    for(let i = max; i >=min; i--){
      this.yearList.push(i);
    }
  }
  /**
  * @name: getLeaveReport
  * @description: get Leave Report
  */

   getSearchReport() {
    
    const date  = this.loaderService.getDateInMMDDYYYY(this.selectLeaveReport.date);
    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.getSearch(this.selectLeaveReport.userName, this.selectLeaveReport.yearVal, date).subscribe((result:any) => {
      if(result.data) {
        this.dataSource = result.data;
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    },(error) => {
      console.log(error);
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }

  /**
  * @name: getLeaveReport
  * @description: get Leave Report
  */

  getLeaveReport() {
    
    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.getLeaveDetailsAllUser().subscribe((result:any) => {
      if(result.data) {
        this.dataSource = result.data;
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    },(error) => {
      console.log(error);
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }
  

  /**
  * @name: captureScreen
  * @description: capture Screen PDF Download
  */
  captureScreen() {
    var data = document.getElementById('leaveReport');
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
        pdf.save('leaveReport.pdf'); // Generated PDF   
      });
    }
  }


}
