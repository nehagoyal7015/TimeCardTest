import { Component, OnInit } from '@angular/core';
import { ReportService } from 'src/app/Services/report.service';
import { LoaderServiceService } from 'src/app/utils/loader/loader-service.service';
import html2canvas from 'html2canvas';
import jspdf from 'jspdf';
import { EmployeeService } from 'src/app/Services/employee.service';
import * as xlsx from 'xlsx';

@Component({
  selector: 'app-employee-report',
  templateUrl: './employee-report.component.html',
  styleUrls: ['./employee-report.component.scss']
})
export class EmployeeReportComponent implements OnInit {

  
  public pkId: any;
  displayedColumns = ['name', 'empId', 'joiningDate', 'emailId', 'phoneNumber', 'address1', 'address2'];
  empListData: any;
  empListIsActive: any;
  isChecked: boolean = false;
  constructor(private loaderService: LoaderServiceService, private reportService: ReportService, public empService: EmployeeService) { }

  ngOnInit(): void {
    this.empList();
  }

   /**
   * @name: emp
   * @description: emp filter
   */
  emp(val: any) {
    this.empListIsActive = [];
    if (val == 0) {
      this.empListData.forEach(element => {
        if (element.isActive === true) {
          this.empListIsActive.push(element);
        }
      });
    }
    else if (val == 1) {
      this.empListIsActive = [];
      this.empListData.forEach(element  => {
        if (element.isActive === false) {
          this.empListIsActive.push(element);
        }
      });
    }
    else if (val == 2) {
      this.empListIsActive = [];
      this.empListIsActive = this.empListData;
    }
  }

  /**
   * @name: empList
   * @description: get emp List
   */
  empList() {
    this.loaderService.emitLoadingChangeEvent(true);
    this.empService.empDetailList().subscribe((result: any) => {
      if (result.success) {
        this.empListData = result.data;
        this.empListIsActive = [];
        this.empListData.forEach(element => {
          if (element.isActive === true) {
            this.empListIsActive.push(element);
          }
        });
      } else { }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error: any) => {
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }

  /**
   * @name: captureScreen
   * @description: capture Screen PDF Download
   */
  captureScreen() {
    var data = document.getElementById('employeeReport');
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
        pdf.save('employeeReport.pdf'); // Generated PDF   
      });
    }
  }

  exportExcel(): void{
    
   // genrate the table id
    let element = document.getElementById('employeeReport');
    const ws:xlsx.WorkSheet = xlsx.utils.table_to_sheet(element);

    // code genrate 
    const wb:xlsx.WorkBook = xlsx.utils.book_new();
    xlsx.utils.book_append_sheet(wb,ws,'sheet1');

    xlsx.writeFile(wb,'employeeReport.xlsx');
  }
}