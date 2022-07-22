import { Component, OnInit } from '@angular/core';
import { ReportService } from 'src/app/Services/report.service';
import { LoaderServiceService } from 'src/app/utils/loader/loader-service.service';
import { tableData } from './project-interface';
import html2canvas from 'html2canvas';
import jspdf from 'jspdf';

@Component({
  selector: 'app-project-report',
  templateUrl: './project-report.component.html',
  styleUrls: ['./project-report.component.scss']
})
export class ProjectReportComponent implements OnInit {

  public domainId = '';
  displayedColumns: string[] = ['clientName', 'projectName', 'asignTo', 'managar', 'totalHourSpent', 'currentMonthSpent'];
  public dataSource: any = []
  constructor(private loaderService: LoaderServiceService, private reportService: ReportService) {
    let currentUser = JSON.parse(localStorage.getItem('user') as string);
    this.domainId = currentUser.data.domainId;
  }

  ngOnInit(): void {
    this.getProjectReport();
  }

  /**
  * @name: getProjectReport
  * @description: get Project Report
  */

  getProjectReport() {
    
    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.getEmployee(this.domainId).subscribe((result) => {
      if (result.data) {
        this.dataSource = result.data;
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error: any) => {
      console.log(error);
      this.loaderService.emitLoadingChangeEvent(false);
    }
    );
  }

  /**
  * @name: captureScreen
  * @description: capture Screen PDF Download
  */
  captureScreen() {
    var data = document.getElementById('projectReport');
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
        pdf.save('projectReport.pdf'); // Generated PDF   
      });
    }
  }
}
