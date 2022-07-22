import { ChangeDetectorRef, Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { HolidayModel, listEmpOptHoliday, OptHolidayModel } from 'src/app/Model/holiday.model';
import { HolidayService } from 'src/app/Services/holiday.service';
import { DialogService } from 'src/app/Shared/dialog.service';

@Component({
  selector: 'app-holiday',
  templateUrl: './holiday.component.html',
  styleUrls: ['./holiday.component.scss']
})
export class HolidayComponent implements OnInit {
  holidayList: HolidayModel[];
  opt: boolean;
  opting = new OptHolidayModel;
  optFlotEmpList: listEmpOptHoliday[];
  updateOptEmp= new HolidayModel;
  holidayId: number;
  holidayDate: string;
  isFloating: any;
  holidayReason: string;
  holidayEvent: string;
  checked: boolean = false;
  defaultYr: number = new Date().getFullYear();
  accessP = 0;
  datePassConfirm = false;





  constructor(private holidayService: HolidayService, public dialog: MatDialog, private changeDetectorRefs: ChangeDetectorRef, private dialogService: DialogService, private _snackBar: MatSnackBar, public activatedRoute: ActivatedRoute) { }




  public displayedColumns = ['holidayDate', 'holidayReason', 'holidayType', 'Opting'];
  selectedRowIndex = -1;

  public OptEmpTable = ['EmpId', 'Name','Voided'];
  empSelectUserId = -1;




  highlight(row: any) {
    if(this.accessP == 1)
    this.selectedRowIndex = row;
  }
  empSelect(row: any) {
    if(this.accessP == 1)
    this.empSelectUserId = row;
  }

  ngOnInit(): void {
    
    this.accessP = history.state.access;
    console.log(history.state.access); 
    this.getHolidayList(this.defaultYr);

  }



  curYear: number = new Date().getFullYear();
  yearList: any = [
    { value: this.curYear - 1, name: this.curYear - 1 },
    { value: this.curYear, name: this.curYear },
    { value: this.curYear + 1, name: this.curYear + 1 },
    { value: this.curYear + 2, name: this.curYear + 2 }

  ];

  getHolidayList(year: any) {

    this.holidayService.holiday(year)
      .subscribe((result: any) => {
        if (result.success) {
          this.holidayList = result.data;
          this.changeDetectorRefs.detectChanges();

          console.log(this.holidayList);

        }
        else {
          console.log("Unsuccessful");
        }
      },
        (error) => {
          console.log(error.error.message, error.error.messageType);
        });
  }



  getEmpOptList(floatingHolidayId: number) {

    this.holidayService.GetEmpOptHolidayList(floatingHolidayId)
      .subscribe((result: any) => {
        if (result.success) {
          this.optFlotEmpList = result.data;
          this.changeDetectorRefs.detectChanges();

          console.log(this.optFlotEmpList);

        }
        else {
          console.log("Unsuccessful");
        }
      },
        (error) => {
          console.log(error.error.message, error.error.messageType);
        });
  }

  addHoliday(data: any) {
    if (data.isFloating == null)
      data.isFloating = false;
    this.dialog.closeAll();
    this.holidayService.addHoliday(data).subscribe((result: any) => {
      if (result.data) {
        this._snackBar.open('Record Saved Successfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: 2 * 1000,
        });
      } else {
        this._snackBar.open('Can not add holiday', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: 2 * 1000,
        });
      }
      this.getHolidayList(this.defaultYr);

    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
  }


  editHoliday(data: any) {
    data.holidayId = this.selectedRowIndex;
    this.dialog.closeAll();
    this.holidayService.editHoliday(data).subscribe((result: any) => {
      if (result.data) {
        this._snackBar.open('Record Saved Successfully!', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: 2 * 1000,
        });
      } else {
        this._snackBar.open('Invalid Data', 'OK', {
          panelClass: 'my-custom-snackbar',
          duration: 2 * 1000,
        });
      }
      this.getHolidayList(this.defaultYr);
      console.log(result.Message);
    },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });

  }


  optNCancel(element: any) {
    this.updateOptEmp = element;

    if(element.isOpting == true)
    {
      this.holidayService.editOpt(element).subscribe((result: any) => {
        this.getHolidayList(this.defaultYr);
        console.log(result.Message);
      });
    }
    this.getHolidayList(this.defaultYr);
  }

  save(){

    if(this.updateOptEmp.userOptHolidayId == 0){
      console.log((JSON.parse(JSON.stringify(this.updateOptEmp))));
      this.holidayService.opt((JSON.parse(JSON.stringify(this.updateOptEmp)))).subscribe((result: any) => {
        console.log(result.Message);
      });
    }
      if(this.updateOptEmp.userOptHolidayId != 0){
      console.log((JSON.parse(JSON.stringify(this.updateOptEmp))));
      this.holidayService.editOpt((JSON.parse(JSON.stringify(this.updateOptEmp)))).subscribe((result: any) => {
        this.getHolidayList(this.defaultYr);
        console.log(result.Message);
      });
    }
    this.getHolidayList(this.defaultYr);

      this.dialog.closeAll();
    
  }

  datePassDialogue(isOpting: boolean, templateRef: TemplateRef<any>)
  {
    if(isOpting == null || isOpting == false)
    {
      this.dialog.open(templateRef, {
        width: '500px',
      });
    }
  }


  changeValueBool(value: any) {
    this.checked = !value;
  }

  dateCheck(date: Date) {
    var elementDate = new Date(date);
    var curDate = new Date();
    if (elementDate <= curDate)
      return true;
    else
      return false;
  }

  openDialog(templateRef: TemplateRef<any>) {
    this.dialog.open(templateRef, {
      width: '500px',
    });
  }





  deleteHoliday() {
    var id = this.selectedRowIndex;
    this.selectedRowIndex = -1;
    this.dialogService.deleteConfirmDialog().afterClosed().subscribe(res => {
      if (res) {
        this.holidayService.deleteHoliday(id.toString()).subscribe((result: any) => {
          if (result.data) {
            this._snackBar.open('Deleted Successfully!', 'OK', {
              panelClass: 'my-custom-snackbar',
              duration: 2 * 1000,
            });
          } else {
            this._snackBar.open('Invalid Holiday-Id...', 'OK', {
              panelClass: 'my-custom-snackbar',
              duration: 2 * 1000,
            });
          }

          this.getHolidayList(this.defaultYr);
          console.log(result.Message);
        });
      }
    })
  }
}


