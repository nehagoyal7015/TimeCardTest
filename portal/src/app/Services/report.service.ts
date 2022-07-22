import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import {environment } from './../../environments/environment'
import { map, catchError } from 'rxjs/operators';
import { NgDynamicBreadcrumbService } from 'ng-dynamic-breadcrumb';



@Injectable({
    providedIn: 'root'
})

export class ReportService {

    public project_Id:any;
    public user_Id:any;
    public ProjectName :any;
    public ClientName: any;
    constructor( private http: HttpClient, private ngDynamicBreadcrumbService: NgDynamicBreadcrumbService){}
    baseUrl = environment.apiUrl;

    getEmployee(domainId:any) : Observable<any> {
        return this.http.get(this.baseUrl + 'Project/projectList' + '?domainId=' + domainId);
    }

    // leave Report 

    getLeaveDetailsAllUser(){
        return this.http.get(this.baseUrl + 'UserLeave/getLeaveDetails');
    }

    getAllEmp(): Observable<any> {
        return this.http.get(this.baseUrl + 'UserLeave/getUser');
    }

    getSearch(username:any, year:any, date:any): Observable<any> {
        return this.http.get(this.baseUrl + 'UserLeave/search' + '?userName=' + username + '&Year=' + year +'&date=' + date);
    }

    getSearchReportsTimeSheet(data:any): Observable<any> {
        debugger
        return this.http.post(this.baseUrl + 'TimeSheet/searchReports', data);
    }
    getallReportsTimeSheet(): Observable<any> {
        return this.http.get(this.baseUrl + 'TimeSheet/getReports');
    }
    getClientLists(){
        return this.http.get(this.baseUrl + 'Project/getAllClient');
    }

    getClientProject(clientId : any){
        return this.http.get(this.baseUrl + 'TimeSheet/getclientinfo?clientId='+ clientId);
    }
    
    ProjectDetail(data:any){
        return this.http.post(this.baseUrl + 'TimeSheet/emPtask', data );
    }
    setPkID(projectId:any, userId:any) {
        this.project_Id = projectId;
        this.user_Id =userId;
      }
      getPkID() {
          return this.project_Id;
      } 
      getUserID(){
    return this.user_Id;
      }

      GetRecentApprovals(): Observable<any>{
        return this.http.get(this.baseUrl + 'Approve/getrecent');
      }

      getEmployeeDetail(data:any): Observable<any>{

      
        return this.http.post(this.baseUrl + 'Approve/search', data);
      }

    

      getApprovalSheet(): Observable<any> {
        return this.http.get(this.baseUrl + 'Approve/getreport');
    }

    getSearchReport(data:any): Observable<any> {
        debugger
        return this.http.post(this.baseUrl + 'Approve/searchreport', data);
    }

    getProjectDetail(data:any){
        return this.http.post(this.baseUrl + 'Approve/appemptask', data );
    }

    searchTaskList(data:any){
        return this.http.post(this.baseUrl + 'Approve/getProjectTask', data)
    }

    employeeReportapp(taskId:any,userId:any){
        return  this.http.get(this.baseUrl + 'Approve/empReport?taskId=' + taskId + '&userId=' + userId);
    }


    updateBreadcrumb(projectName: any, clientName: any): void { 
        const breadcrumbs = [
          {
            label: 'Report',
            url: '/home/report'
          },
          {
            label: 'Approval',// pageTwoID Parameter value will be add 
            url: '/home/report/Approval'
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
        this.ClientName = clientName;
        this.ProjectName = projectName;
      }
      getprojectName() {
        return this.ProjectName;
    } 
    getclientName(){
        return this.ClientName;
    }
}