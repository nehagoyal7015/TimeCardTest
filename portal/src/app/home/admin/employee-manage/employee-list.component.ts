import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit, Output, EventEmitter, ViewChild, TemplateRef, OnChanges, SimpleChanges } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { DialogService } from 'src/app/Shared/dialog.service';
import { EmployeeService } from './../../../Services/employee.service';
import { LoaderServiceService } from './../../../utils/loader/loader-service.service';
import { asignProject, employeeListModel } from './employee.model';
import html2canvas from 'html2canvas';
import jspdf from 'jspdf';
import * as xlsx from 'xlsx';
import { AsignProjectServicesService } from 'src/app/Services/asign-project-services.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit{


  public UserId: any;
  public pkId: any;
  displayedColumns = ['name', 'empId', 'joiningDate', 'emailId', 'phoneNumber', 'iconList'];
  empListData: employeeListModel[];
  empListIsActive: employeeListModel[];
  asignProjectInfo: asignProject[];
  isChecked: boolean = false;
  statusLable: any;
  isAllSelected: boolean = false;
  status ='Active';

  public asignProjectTable = ['clientName', 'projectName'];
  constructor(private router: Router, private _snackBar: MatSnackBar, public dialog: MatDialog,
    private dialogService: DialogService, public empService: EmployeeService,private asignServices:AsignProjectServicesService, private loaderService: LoaderServiceService) { }
 


  ngOnInit(): void {
    this.empList();
    this.statusLable = 'InActive';
    this.isAllSelected = false;
  }

  

  openDialog(templateRef: TemplateRef<any>) {
    {
      this.dialog.open(templateRef, {
        width: '500px',
      });
    }
  }


  /**
   * @name: empDetails
   * @description: emp Details Row
   */
  empDetails(pk_Id: number) {
    this.pkId = pk_Id;
    this.savePkId();
    this.router.navigate(['/home/Admin/empManag']);

  }
  asignedProject(pk_Id: number,userId :number) {
    this.pkId = pk_Id;
    this.UserId = userId
    this.savePkId(); 
    this.router.navigate(['/home/Admin/asignProject']);
  }
  /**
   * @name: addNewEmp
   * @description: add New Emp btn
   */
  addNewEmp() {
    this.pkId = '';
    this.savePkId();
    this.router.navigate(['/home/Admin/empManag']);
  }
  /**
   * @name: savePkId
   * @description: save Pk Id
   */
  savePkId() {
    this.empService.setPkID(this.pkId);
    this.asignServices.setPkID(this.pkId);
    this.asignServices.setUserID(this.UserId);
  }

  /**
   * @name: empList
   * @description: emp List Table Data
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
    }, (error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }
  emp(val: any) { 
    this.empListIsActive = [];
    if (val == 0) {
      this.status = 'Active';
      this.statusLable = 'InActive';
      this.isAllSelected = false;
      this.empListData.forEach(element => {
        if (element.isActive === true) {
          this.empListIsActive.push(element);
        }
      });
    }
    else if (val == 1) {   
      this.status = 'InActive';
      this.statusLable = 'Active';
      this.isAllSelected = false;
      this.empListData.forEach(element => {
        if (element.isActive === false) {
          this.empListIsActive.push(element);
        }
      });
    }
    else if (val == 2) {
      this.status = 'All';
      this.statusLable = 'All';
      this.empListIsActive = this.empListData;
    }
    
  }


  activeInActiveEvent(pkId: number, type: any) {
    if (type === 'Active') {
      this.infoActive(pkId)
    } else if (type === 'InActive') {
      this.InactiveDialog(pkId)
    }
  }

  InactiveDialog(pkId: number) {
    this.dialogService.InactiveDialog().afterClosed().subscribe(res => {
      if (res) {
        this.empService.inActiveInfo(pkId)
          .subscribe((result: any) => {
            if (result.data) {
              this._snackBar.open('Employee InActive successfully!', 'Close', {
                panelClass: 'my-custom-snackbar',
                duration: 2 * 1000,
              });
              this.empList();
            } else {
              this._snackBar.open('Can not be InActive', 'Close', {
                panelClass: 'my-custom-snackbar',
                duration: 2 * 1000,
              });
            }
          },
            (error) => {
              console.log(error.error.message, error.error.messageType);
            });
      }
    })
  }

  infoActive(pkId: number) {
    this.dialogService.ActiveDialog().afterClosed().subscribe(res => {
      if (res) {
        this.empService.activeInfo(pkId)
          .subscribe((result: any) => {
            if (result.data) {
              this._snackBar.open('Employee Active successfully!', 'Close', {
                panelClass: 'my-custom-snackbar',
                duration: 2 * 1000,
              });
              this.empList();
            } else {
              this._snackBar.open('Can not be Active', 'Close', {
                panelClass: 'my-custom-snackbar',
                duration: 2 * 1000,
              });
            }
          },
            (error) => {
              console.log(error.error.message, error.error.messageType);
            });
      }
    })
  }

  getAsignProject(pkId: number) {
    this.empService.asignProjectInfo(pkId).subscribe((result: any) => {
      if (result.success) {
        this.asignProjectInfo = result.data;
        console.log(this.asignProjectInfo);
      }
    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
  }

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

