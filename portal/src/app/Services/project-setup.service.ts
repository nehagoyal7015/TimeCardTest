import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {environment } from './../../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class ProjectSetupService {

  projId = '';
  clientId = '';
  
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  postAddNewproject(data:any) : Observable<any> {
    
    return this.http.post(this.baseUrl + 'Project/add',  data);
  }
  getEmployee() : Observable<any> {
    return this.http.get(this.baseUrl + 'Project/getEmployee');
  }
  putUpdateproject(data:any) : Observable<any> {
    
    return this.http.put(this.baseUrl + 'Project/update',  data);
  }
  addNewClient(data:any) : Observable<any> {
    return this.http.post(this.baseUrl + 'Client/add', data);
  }
  updateClient(data:any) {
    return this.http.put(this.baseUrl + 'Client/update', data);
  }
  getClientList(){
    return this.http.get(this.baseUrl + 'Client/get');
  }
  getEmployeeListAll(){
    return this.http.get(this.baseUrl + 'Project/getAllEmployee');
  }
  getClientListDetails(){
    return this.http.get(this.baseUrl + 'Client/getClient');
  }
  getEmployeList(){
    return this.http.get(this.baseUrl + 'Project/getEmployee');
  }
  getProjectList(projectId:any){
    return this.http.get(this.baseUrl + 'Project/getProjectList?projectId=' + projectId);
  }

  getProjectDetails(projectId:any){
    return this.http.get(this.baseUrl + 'Project/projectDetails?ProjectId=' + projectId);
  }
  CancelArcheive(projectId:any,isArcheive:boolean){
    return this.http.get(this.baseUrl + 'Project/archieve?projectId=' + projectId+'&isArcheive'+ isArcheive);
  }
  empHourList(TaskId:any,userId:any){
    return this.http.get(this.baseUrl + 'Project/getTaskList?TaskId=' + TaskId + '&userId=' + userId);
  }


  setProjIdID(projId:any, clientId: any) {
    this.projId = projId;
    this.clientId = clientId;
  }
  getProjIdID() {
      return this.projId;
  }
  getClientID() {
    return this.clientId;
}
}
