import { Component, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
//import { ClientInfo, ProjectInfoList } from './timesheet';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import {ThemePalette} from '@angular/material/core';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { yearsPerPage } from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import { LoaderServiceService } from 'src/app/utils/loader/loader-service.service';
import { ReportService } from 'src/app/Services/report.service';
import { ClientInfo, ProjectInfoList } from './approval';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { NgDynamicBreadcrumbService } from 'ng-dynamic-breadcrumb';
import { Router } from '@angular/router';
import { now } from 'lodash';
import { analyzeAndValidateNgModules } from '@angular/compiler';

export class FoodNode {
  children?: FoodNode[];
  item: string;
}

// export interface Task {
//   name: string;
//   completed: boolean;
//   color: ThemePalette;
//   subtasks?: Task[];
// }

// export interface NestedRowData {
//   Client: string;
//   Project: string;
//   Date: string;
//   BillHours: number;
//   NonBillHours:number;
//   Action:any;
// }

// export interface RowData {
//   EmployeeName: string;
//   TotalSubmitted: number;
//   Approved: string;
//   TotalRejected: number;
  
// }


// const ELEMENT_DATA: RowData[]  = [
//   {
//      EmployeeName: 'X',
//     TotalSubmitted: 6,
//     Approved: 'Y',
//     TotalRejected: 7,
//     }
// ];

@Component({
  selector: 'app-approvals',
  templateUrl: './approvals.component.html',
  styleUrls: ['./approvals.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ApprovalsComponent implements OnInit {



  displayedColumns: string[] = ['clientName/projectName'];
  displayedColumns1: string[]= ['taskName'];
  displayeColumns2: string[]= ['EmployeeName', 'TotalSubmitted', 'Approved', 'TotalRejected'];
  displayeColumns3: string[]= ['Client','Project','Task','Date','BillHours','NonBillHours','Action'];
  // dataSource = ELEMENT_DATA;
  // columnsToDisplay = ['EmployeeName', 'TotalSubmitted', 'Approved', 'TotalRejected'];
  // columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];
  // expandedElement: RowData | null;

  public approval: any = { projectId: '', projectIds: [], projectName: [], taskName: [], userId: '0', currentdate: '', rangeStartDate: '', rangeEndtDate: 0, CurrentWeek: '', LastWeek: '', CurrentMonth: '', LastMonth: '', billable: false, nonBillable: false }
  isExpandCategory: boolean[] = [];
  public dataSource: MatTableDataSource<any>;
  myControl = new FormControl();
  public clientList: ClientInfo[];
  public selectDateVal = true;
  public space = true;
  public projectList: ProjectInfoList[];
  public peopleList: any = [{ userId: 0, userName: 'All' }];
  public EmployeeList: any = [{ userId: 0, userName: 'All' }];
  public clientPojectList: any = [];
  public projectInfo: any;
  public projectID: any;
  public userID: any;
  monthList = [];
  defaultMonth: any;
  data: any;
  public searchValueEmpName = '';
  allChecked: boolean = false;
  stateRecord: any = [];
  allComplete: boolean = false;
  currentweek: any;
  lastweek: any;
  currlast: any;
  projectIds: any = [];
  ProjectName: any;
  ClientName: any;
  public UserId: number[];
  public employeeReportList: any;
  public searchTask: any = [];


  @ViewChild('edit') edit: TemplateRef<any>;
  public dataSourceApproval: any = []

  ApprovalsList: any = [];
  ELEMENT_DATA: any;


  filteredOptions: Observable<string[]>;

  //  element: NestedRowData[] = [
  //   {Client: 'A', Project: 'TimeSheet', Date:'06-05-2022', BillHours: 6, NonBillHours: 4, Action: '' },
    
  // ];
 

  // displayedNestedColumns :string[] = ['Client','Project','Task','Date','BillHours','NonBillHours','Action']
  // dataSourceNested = this.element;

  
  
  // task: Task = {
  //   name: 'Indeterminate',
  //   completed: false,
  //   color: 'primary',
  //   subtasks: [
  //     {name: 'Primary', completed: false, color: 'primary'},
  //     {name: 'Accent', completed: false, color: 'accent'},
  //     {name: 'Warn', completed: false, color: 'warn'},
  //   ],
  // };

  // updateAllComplete() {
  //   this.allComplete = this.task.subtasks != null && this.task.subtasks.every(t => t.completed);
  // }

  // someComplete(): boolean {
  //   if (this.task.subtasks == null) {
  //     return false;
  //   }
  //   return this.task.subtasks.filter(t => t.completed).length > 0 && !this.allComplete;
  // }

  // setAll(completed: boolean) {
  //   this.allComplete = completed;
  //   if (this.task.subtasks == null) {
  //     return;
  //   }
  //   this.task.subtasks.forEach(t => (t.completed = completed));
  // }

  showTable: boolean = false;


  constructor(private loaderService: LoaderServiceService, private reportService: ReportService,private dialog: MatDialog,private router: Router,
    private ngDynamicBreadcrumbService: NgDynamicBreadcrumbService,private _formBuilder: FormBuilder ) { 
    for (var i = 0; i < 12; i++) {
      var d = new Date();
      d.setMonth(d.getMonth() - i);
      var month = d.toLocaleString("default", { month: "long" });
      var year = d.getFullYear();
      var monthNo=d.getMonth();
      if(monthNo<9){
      this.monthList.push( {month:"0"+(monthNo+1)+"-"+year, date:month+"-"+year});
      if(i === 0){
        this.approval.currentdate = ("0"+(monthNo+1)+"-"+year);
      }
      }else{
      this.monthList.push( {month:''+ (monthNo+1) + '-'+year, date:month+"-"+year});
      if(i === 0){
        this.approval.currentdate = (''+ (monthNo+1) + '-'+year);
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
  
    public searchData: any;
  states = new FormControl();

  ngOnInit(): void {
    // this.filteredOptions = this.myControl.valueChanges.pipe(
    //   startWith(''),
    //   map(value => this._filter(value)),
    // );
    this.getClientList();
    // this.searchTaskList();
 
    this.approval.billable = true;

    
    
    
  }

  // private _filter(value: string): string[] {
  //   const filterValue = value.toLowerCase();

  //   return this.options.filter(option => option.toLowerCase().includes(filterValue));
  // }

  updateBreadcrumb(projectName: any, clientName: any) { 
    this.reportService.updateBreadcrumb(projectName, clientName);
  }

  
  Ranges:any =[
    {id:1, value:'Current Week'},
    {id:2, value: 'Last Week'},
    {id:3, value: 'Current Month'},
    {id:4, value: 'Last Month'},

  ]
  

 
  public chosenMod: string = "";

  getWeek(data:any){
    switch(data) {
      case "1" : {
                  //Current Week
                  var curr = new Date; // get current date
                  var firstday = curr.getDate() - curr.getDay() +1; // First day is the day of the month - the day of the week
                  var lastday = firstday + 6; // last day is the first day + 6
                  var currentw = new Date(curr.setDate(firstday)).toUTCString();
                  var lastw = new Date(curr.setDate(curr.getDate()+6)).toUTCString();
                  this.currentweek =currentw + lastw;
                  alert(this.currentweek);
                  break;
      }
                  
      case "2":  {
                          //Last Week
                          var curr = new Date; 
                          var first = curr.getDate() - curr.getDay() -6; // First day is the day of the month - the day of the week
                          var last = first -7; // last day is the first day + 6
                          var current = new Date(curr.setDate(first)).toUTCString();
                          var lastw = new Date(curr.setDate(curr.getDate()+6)).toUTCString();
                          this.lastweek =current + lastw;
                          alert(this.lastweek);
                          break;
      }
      
      case "3": {
                         //Current Month
                          const currentm = new Date();
                          currentm.setMonth(currentm.getMonth());
                          const currentMonth = currentm.toLocaleString('default', { month: 'long' });
                                
                          alert(currentMonth);
                          break;
      }


        case "4":  {
                         //Last Month
                          const lastm = new Date();
                          lastm.setMonth(lastm.getMonth()-1);
                          const lastMonth = lastm.toLocaleString('default', { month: 'long' });

                          alert(lastMonth); 
                          break;
        }
        



  }
}


  expandDocumentTypes(group: any) {
    
  this.isExpandCategory[group] = !this.isExpandCategory[group];
      
    }

  toggleShowTable(): void {
    this.showTable = !this.showTable;
}

toggleSelection(event:any,data: any) {
  
  if(event.checked){
    this.approval.projectIds.push(data.projectId);
    this.approval.projectName.push(data.projectName);
    // this.approval.taskName.push(data.taskName);
    this.myControl.setValue(this.approval.projectName);
    
    this.reportService.getEmployeeDetail({"ProjectIds":this.approval.projectIds}).subscribe((result) => {
      console.log(result.data);
      if (result.data) {
        this.peopleList = result.data;
        this.EmployeeList = result.data;
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      
      this.loaderService.emitLoadingChangeEvent(false);
    })
  }
  else {
    this.approval.projectName =  this.approval.projectName.filter((x:any) => x!== data.projectName);
      this.myControl.setValue(this.myControl.value.filter((x:any) => x!== data.projectName));
    this.removeProjectIds(data.projectId);
    
  }
}

  toggleParent(event: any, data: any) {
    
    data.checked = event.checked;
    
    let myControl = this.myControl.value;
    myControl = myControl ? myControl : [];

    data.projectData.forEach((ele: any) => {
    if(event.checked) { debugger;          
      this.approval.projectIds.push(ele.projectId);
      this.approval.projectName.push(ele.projectName);
      // this.approval.taskName.PUSH(ele.taskName);
      this.myControl.setValue(this.approval.projectName);
      // this.myControl.setValue(this.approval.taskName);
      
      this.reportService.getEmployeeDetail({"ProjectIds":this.approval.projectIds}).subscribe((result) => {
        console.log(result.data);
        if (result.data) {
          this.peopleList = result.data;
          this.EmployeeList = result.data;
        } else {
  
        }
        this.loaderService.emitLoadingChangeEvent(false);
      }, (error) => {
        // console.log(error);
        this.loaderService.emitLoadingChangeEvent(false);
      })
    }
    else{
      this.approval.projectName =  this.approval.projectName.filter((x:any) => x!== ele.projectName);
      this.myControl.setValue(this.myControl.value.filter((x:any) => x!== ele.projectName));
      this.removeProjectIds(ele.projectId);
    }
  })
  }

  projectDetail(projectId: any, userId: number[]) {
 
  
    this.router.navigate(['/home/report/approval'], {queryParams:{'projectid': projectId, 'userid': userId}});
  }
    removeProjectIds(id: any){
      
      // this.approval.projectIds.push(id)
      
      if(this.approval.projectIds.includes(id)){
        const index = this.approval.projectIds.findIndex((item : any)=> item == id);
        this.approval.projectIds.splice(index,1);
        // console.log(index,id);
      }
      
      else{
          this.approval.projectIds.push(id);
        }
      }

/**
    * @name: searchReport
    * @description: search Report BTN
    */

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
  * @name: searchTaskList
  * @description: get Client List
  */

//  searchTaskList() {
//   this.loaderService.emitLoadingChangeEvent(true);
//   this.reportService.searchTaskList(this.data).subscribe((result: any) => {
//     if (result.success) {
//       this.clientList = result.data;
//       // this.clientList1 = result.data.projectData;
//     } else { }
//     this.loaderService.emitLoadingChangeEvent(false);
//   }, (error) => {
//     this.loaderService.emitLoadingChangeEvent(false);
//   });
// }


    //    Apply Filter on UserName
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

   

    editClick(){
      this.dialog.open(this.edit, {
        width: '500px'
       
     });
    }

    close(): void {
      this.dialog.closeAll();
    }

  //   approvalSearch():void{
  //       if (this.approval.projectId ) {
  //         this.reportService.getApprovalSearch(this.approval.projectIds);
  //        }
  //  }

  approvalSearch() {
    if (this.approval.projectId ||this.approval.projectName ||this.approval.taskName || this.approval.userId || this.approval.currentdate || this.approval.rangeStartDate || this.approval.rangeEndtDate || this.approval.billable == true || this.approval.nonBillable == true) {
      
      this.getSearchReport();
    } else {
      this.getApprovalSheet();
    }
  }

  /**
  * @name: getSearchReport
  * @description: get Search Report
  */

  getSearchReport() {
    
    let currentdate = this.approval.currentdate;

          
    let startDate, endDate;
    if (this.selectDateVal == true) {
      startDate = '';
      endDate = '';
    } else {
      startDate = this.loaderService.getDateInMMDDYYYY(this.approval.rangeStartDate);
      endDate = this.loaderService.getDateInMMDDYYYY(this.approval.rangeEndtDate);
    }

    let conts = {
      'projectIds' : this.approval.projectIds,
      'projectName' : this.approval.projectName,
      'taskName' : this.approval.taskName,
      'userId' : this.approval.userId,
      'currentDate' : this.approval.currentdate,
      'startDate' : this.approval.startDate,
      'endDate' : this.approval.endDate,
      'billable' : this.approval.billable,
      'nonBillable' : this.approval.nonBillable,
    }
    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.getSearchReport(conts).subscribe((result: any) => {
      debugger;
      if (result.data) {
        this.dataSourceApproval = result.data;
       
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      console.log(error);
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }

  getApprovalSheet() {

    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.getApprovalSheet().subscribe((result: any) => {
      if (result.data) {
        this.dataSource = result.data;
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      console.log(error);
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }

   
   selectDate(date: any) {
    if (date === 1) {
      this.selectDateVal = true;
      this.approval.currentdate = '';
      this.approval.rangeStartDate = '';
      this.approval.rangeEndtDate = '';
    } else if (date === 2) {
      this.selectDateVal = false;
      this.approval.currentdate = '';
      this.approval.rangeStartDate = '';
      this.approval.rangeEndtDate = '';
    }

  }

  getClientProjectIfo(clientId: any) {
    
    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.getClientProject(clientId).subscribe((result: any) => {
      if (result.data) {
        this.clientPojectList = result.data;
        this.clientPojectList.projectName = result.data.projectName;
        // this.clientPojectList.taskName =result.data.taskName;
      } else {

      }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      console.log(error);
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }



  // updateBreadcrumb(projectName:any, clientName:any, taskName:any): void {
  //   const breadcrumbs  =  [
  //     {
  //       label: 'Report',
  //       url: '/home/report'
  //     },
  //     {
  //       label: 'Approval',// pageTwoID Parameter value will be add 
  //       url: '/home/report/approval'
  //     },
  //     {
  //       label: clientName,// pageTwoID Parameter value will be add 
  //       url: ''
  //     },
  //     {
  //       label: projectName,// pageTwoID Parameter value will be add 
  //       url: ''
  //     },
  //     {
  //       label : taskName,
  //       url:''
  //     }
  //   ];
  //   this.ngDynamicBreadcrumbService.updateBreadcrumb(breadcrumbs);
  // }

  saveProjectId() {
    this.reportService.setPkID(this.projectID,this.userID);
  }

  getProjectDetail(projectId:any,userId:any) {
    this.projectID =projectId;
    this.userID = userId;
    this.saveProjectId();
    // this.router.navigate(['/home/report/approval']);
  }


  getEmployeeReport(taskId: any, userId: any) {
   
    this.loaderService.emitLoadingChangeEvent(true);
    this.reportService.employeeReportapp(taskId, userId).subscribe((result: any) => {
      if (result.success) {
        this.employeeReportList = result.data;
      } else {
      }
      this.loaderService.emitLoadingChangeEvent(false);
    }, (error) => {
      console.log(error);
      this.loaderService.emitLoadingChangeEvent(false);
    }
    )
  }
  // getSearchReport1() {
  //   debugger
  //   this.loaderService.emitLoadingChangeEvent(true);
  //   this.reportService.searchTaskList(this.data).subscribe((result: any) => {
  //     if (result.data.length > 0) {
  //       this.searchTask = result.data;
  //     } else {
  //     }
  //     this.loaderService.emitLoadingChangeEvent(false);
  //   }, (error) => {
  //     console.log(error);
  //     this.loaderService.emitLoadingChangeEvent(false);
  //   }
  //   )
  // }

  
  }
