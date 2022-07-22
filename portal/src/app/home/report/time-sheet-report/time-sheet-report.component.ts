import { Component, Injectable, OnInit, ViewChild } from '@angular/core';
import { ReportService } from 'src/app/Services/report.service';
import { LoaderServiceService } from 'src/app/utils/loader/loader-service.service';
import html2canvas from 'html2canvas';
import jspdf from 'jspdf';
import { LeaveServiceService } from 'src/app/Shared/leave-service.service';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Router } from '@angular/router';
import { ClientInfo, ProjectInfoList } from './timesheet';
import { SelectionModel } from '@angular/cdk/collections';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlattener } from '@angular/material/tree';
import { NgDynamicBreadcrumbService } from 'ng-dynamic-breadcrumb';
import { BehaviorSubject, ReplaySubject, Subject } from 'rxjs';
import { MatSelect } from '@angular/material/select';
import * as xlsx from 'xlsx';

export class FoodNode {
  children?: FoodNode[];
  item: string;
}


@Component({
  selector: 'app-time-sheet-report',
  templateUrl: './time-sheet-report.component.html',
  styleUrls: ['./time-sheet-report.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})


export class TimeSheetReportComponent implements OnInit {

  displayedColumns: string[] = ['clientName', 'timeEntered', 'total', 'project'];
  DetailsColumns: string[] = ['projectName', 'total'];

  
  public dataSourceTimeSheet: any = []

  public clientPojectList: any = [];
  public selectDateVal = true;
  public season = true;
  public timeSheetReport: any = { projectId: '', projectIds : [], projectName : [], userId: 0, currentdate: '', rangeStartDate: '', rangeEndtDate: 0, billable: false, nonBillable: false }
  public clientList: ClientInfo[];
  // public clientList1 : ClientInfo[]
  public projectList : ProjectInfoList[];
//   clientList = {
//     clientId: 0,
//     clientName: '',
//     selected: false,
//     disabled: false,
//     projectData=projectInfoList[],
//    }

//    projectInfoList ={
//     projectId: 0,
//     projectName: ''
// }
  public projectInfo:any;
  public peopleList: any = [{ userId: 0, userName: 'All' }];
  public EmployeeList: any = [{ userId: 0, userName: 'All' }];
  public projectID: any;
  public userID:any;
  monthList = [];
  defaultMonth:any;
  data: any;
  public searchValueEmpName = '';
  allChecked: boolean = false;
 
  dataSource: MatTableDataSource<any>;

  isExpandCategory: boolean[] = [];
  stateRecord:any = [];
  allComplete: boolean = false;

  protected _onDestroy = new Subject();
  // public dateRange = new Date();
  constructor(private loaderService: LoaderServiceService, 
    private router: Router, private reportService: ReportService,private ngDynamicBreadcrumbService: NgDynamicBreadcrumbService,private _formBuilder: FormBuilder) {
      for (var i = 0; i < 12; i++) {
        var d = new Date();
        d.setMonth(d.getMonth() - i);
        var month = d.toLocaleString("default", { month: "long" });
        var year = d.getFullYear();
        var monthNo=d.getMonth();
        if(monthNo<9){
        this.monthList.push( {month:"0"+(monthNo+1)+"-"+year, date:month+"-"+year});
        if(i === 0){
          this.timeSheetReport.currentdate = ("0"+(monthNo+1)+"-"+year);
        }
        }else{
        this.monthList.push( {month:''+ (monthNo+1) + '-'+year, date:month+"-"+year});
        if(i === 0){
          this.timeSheetReport.currentdate = (''+ (monthNo+1) + '-'+year);
        }
        }
      }

      this.data = {};
    this.data.isAllSelected = false;
    this.data.isAllCollapsed = false;
    
    }

  range = new FormGroup({
  start: new FormControl(),
  end: new FormControl(),
  });

  states = new FormControl();  

  ngOnInit(): void {
    this.timeSheetReport.billable = true;
    this.getUserList();
    this.getClientList();
    let breadcrumb : any ={customText: 'This is the Custom Text'};
    this.ngDynamicBreadcrumbService.updateBreadcrumb(breadcrumb);
  }


  removeProjectIds(id: any){
    debugger;
    // this.timeSheetReport.projectIds.push(id)
    
    if(this.timeSheetReport.projectIds.includes(id)){
      const index = this.timeSheetReport.projectIds.findIndex((item : any)=> item == id);
      this.timeSheetReport.projectIds.splice(index,1);
      console.log(index,id);
    }
    
    else{
        this.timeSheetReport.projectIds.push(id);
      }
    }

   /***
      Apply Multi select check Box
   */

 
    expandDocumentTypes(group: any) {
      debugger;
    this.isExpandCategory[group] = !this.isExpandCategory[group];
        // expand only selected parent dropdown category with that childs
      }
    
    toggleSelection(event:any,data: any) {
      debugger;
      if(event.checked){
        this.timeSheetReport.projectIds.push(data.projectId);
        this.timeSheetReport.projectName.push(data.projectName);
        this.states.setValue(this.timeSheetReport.projectName);
      }
      else {
        this.timeSheetReport.projectName =  this.timeSheetReport.projectName.filter((x:any) => x!== data.projectName);
          this.states.setValue(this.states.value.filter((x:any) => x!== data.projectName));
        this.removeProjectIds(data.projectId);
      }
    }
    
      toggleParent(event: any, data: any) {
        debugger;
        data.checked = event.checked;
        
        let states = this.states.value;
        states = states ? states : [];

        data.projectData.forEach((ele: any) => {
        if(event.checked) { debugger;          
          this.timeSheetReport.projectIds.push(ele.projectId);
          this.timeSheetReport.projectName.push(ele.projectName);
          this.states.setValue(this.timeSheetReport.projectName);
        }
        else{
          this.timeSheetReport.projectName =  this.timeSheetReport.projectName.filter((x:any) => x!== ele.projectName);
          this.states.setValue(this.states.value.filter((x:any) => x!== ele.projectName));
          this.removeProjectIds(ele.projectId);
        }
        // this.states.setValue(states);
        //   if(!event.checked) {
        //     debugger;
        //       this.states.setValue(this.states.value.filter((x:any) => !x.includes(data)))
        // }
        // this.stateRecord = this.states.value;
        })
        
      }

  updateBreadcrumb(projectName:any, clientName:any): void {
    const breadcrumbs  =  [
      {
        label: 'Report',
        url: '/home/report'
      },
      {
        label: 'TimeSheet Report',// pageTwoID Parameter value will be add 
        url: '/home/report/time-sheet-report'
      },
      {
        label: clientName,// pageTwoID Parameter value will be add 
        url: ''
      },
      {
        label: projectName,// pageTwoID Parameter value will be add 
        url: ''
      },
    ];
    this.ngDynamicBreadcrumbService.updateBreadcrumb(breadcrumbs);
  }
  
  /**
     Apply Filter on UserName
    */
  setVal(){
    this.peopleList = this.EmployeeList;
  }
  applyFilter(event: Event) { 
    const filterValue = (event.target as HTMLInputElement).value.toLowerCase();
    let res = this.select(filterValue);
    this.peopleList = res;
  }

  select(query: string):string[]{
    let result: string[] = [];
    for(let a of this.EmployeeList){
      if(a.userName.toLowerCase().indexOf(query) > -1){
        result.push(a)
      }
    }
    return result;
  }


  selectDate(date: any) {
    if (date === 1) {
      this.selectDateVal = true;
      this.timeSheetReport.currentdate = '';
      this.timeSheetReport.rangeStartDate = '';
      this.timeSheetReport.rangeEndtDate = '';
    } else if (date === 2) {
      this.selectDateVal = false;
      this.timeSheetReport.currentdate = '';
      this.timeSheetReport.rangeStartDate = '';
      this.timeSheetReport.rangeEndtDate = '';
    }

  }
  saveProjectId() {
    this.reportService.setPkID(this.projectID,this.userID);
  }

  projectDetail(projectId:any,userId:any) {debugger
    this.projectID =projectId;
    this.userID = userId;
    this.saveProjectId();
    this.router.navigate(['/home/report/timesheetprojectreport']);
  }
  /**
    * @name: searchReport
    * @description: search Report BTN
    */
  searchReport() {debugger;
    if (this.timeSheetReport.projectId || this.timeSheetReport.userId || this.timeSheetReport.currentdate || this.timeSheetReport.rangeStartDate || this.timeSheetReport.rangeEndtDate || this.timeSheetReport.billable == true || this.timeSheetReport.nonBillable == true) {
      
      this.getSearchReport();
    } else {
      this.getallReportsTimeSheet();
    }
  }

  /**
  * @name: getClientList
  * @description: get Client List
  */
  getClientList() {
    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.getClientLists().subscribe((result: any) => {
      if (result.success) {
        this.clientList = result.data;
        // this.clientList1 = result.data.projectData;
      } else { }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      this.loaderService.emitLoadingChangeEvent(false);
    });
  }
  /**
  * @name: getUserList
  * @description: get People
  */
  getUserList() {

    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.getAllEmp().subscribe((result) => {
      if (result.data) {
        this.peopleList = result.data;
        this.EmployeeList = result.data;
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      console.log(error);
      this.loaderService.emitLoadingChangeEvent(false);
    })
  }



  /**
  * @name: getallReportsTimeSheet
  * @description: get all Reports TimeSheet
  */

  getallReportsTimeSheet() {

    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.getallReportsTimeSheet().subscribe((result: any) => {
      if (result.data) {
        this.dataSourceTimeSheet = result.data;
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      console.log(error);
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }

  getClientProjecgtIfo(clientId: any) {
    
    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.getClientProject(clientId).subscribe((result: any) => {
      if (result.data) {
        this.clientPojectList = result.data;
        this.clientPojectList.projectName = result.data.projectName;
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      console.log(error);
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }
  /**
  * @name: getSearchReport
  * @description: get Search Report
  */

  getSearchReport() {
    let currentdate = this.timeSheetReport.currentdate;
    // let firstDay = new Date(currentdate.getFullYear(), currentdate.getMonth(), 1);
    // let lastDay = new Date(currentdate.getFullYear(), currentdate.getMonth() + 1, 0); 
          
    let startDate, endDate;
    if (this.selectDateVal == true) {
      startDate = '';
      endDate = '';
    } else {
      startDate = this.loaderService.getDateInMMDDYYYY(this.timeSheetReport.rangeStartDate);
      endDate = this.loaderService.getDateInMMDDYYYY(this.timeSheetReport.rangeEndtDate);
    }

    let conts = {
      'projectIds' : this.timeSheetReport.projectIds,
      'userId' : this.timeSheetReport.userId,
      'currentDate' : this.timeSheetReport.currentdate,
      'startDate' : this.timeSheetReport.startDate,
      'endDate' : this.timeSheetReport.endDate,
      'billable' : this.timeSheetReport.billable,
      'nonBillable' : this.timeSheetReport.nonBillable,
    }
    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.getSearchReportsTimeSheet(conts).subscribe((result: any) => {
      if (result.data) {
        this.dataSourceTimeSheet = result.data;
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
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


  exportExcel(): void{
    // genrate the table id
     let element = document.getElementById('leaveReport');
     const ws:xlsx.WorkSheet = xlsx.utils.table_to_sheet(element);
 
     // code genrate 
     const wb:xlsx.WorkBook = xlsx.utils.book_new();
     xlsx.utils.book_append_sheet(wb,ws,'sheet1');
 
     xlsx.writeFile(wb,'employeeReport.xlsx');
   }
}
