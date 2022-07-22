import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserGroupService {
  baseUrl = environment.apiUrl;

  public groupDataObj = {};
  public empListDetails:any = {};
  constructor(private http: HttpClient) { }

  AddGroup(data: any, domainId: number): Observable<any> {
    return this.http.post(this.baseUrl + 'Groups/add?domainId=' + domainId, data);    
  }

  GetGroup(domainId: number): Observable<any> {
    return this.http.get(this.baseUrl + 'Groups/get?domainId=' + domainId);    
  }

  EditGroup(data: any): Observable<any> {
    return this.http.put(this.baseUrl + 'Groups/edit' , data);    
  }

  DeleteGroup(groupId: any): Observable<any> {
    return this.http.delete(this.baseUrl + 'Groups/delete' + '?groupId=' +  groupId);    
  }

  // AddAccessControl(data: any): Observable<any> {
  //   return this.http.post(this.baseUrl + 'AccessControl/add', data);    
  // }

  // UpdateAccessControl(data: any): Observable<any> {
  //   return this.http.put(this.baseUrl + 'AccessControl/update', data);    
  // }

  // GetAccessControl(): Observable<any> {
  //   return this.http.get(this.baseUrl + 'AccessControl/get');    
  // }

  // DeleteAccessControl(accessControlId: any): Observable<any> {
  //   return this.http.delete(this.baseUrl + 'AccessControl/delete', accessControlId);    
  // }

  getGroupAccessControAssigned(groupId:any): Observable<any> {
    return this.http.get(this.baseUrl + 'GroupAccessControl/getAssigned' + '?groupId=' + groupId);    
  }

  AddGroupAccessControl(groupId:any, data: any): Observable<any> {
    return this.http.post(this.baseUrl + 'GroupAccessControl/add?groupId=' + groupId, data);    
  }
  
  
  
  getGroupAccessControlAvialable(groupId: any): Observable<any> {
    return this.http.get(this.baseUrl + 'GroupAccessControl/getAvialable' + '?groupId=' + groupId);    
  }
  
  GetUserGroup(): Observable<any> {
    return this.http.get(this.baseUrl + 'UserGroup/get');    
  }

  AddUserGroup(data: any): Observable<any> {
    return this.http.post(this.baseUrl + 'UserGroup/add', data);    
  }
  EditUserGroup(data: any): Observable<any> {
    return this.http.put(this.baseUrl + 'UserGroup/edit', data);    
  }
  DeleteUserGroup(userGroupId: any): Observable<any> {
    return this.http.delete(this.baseUrl + 'UserGroup/delete');    
  }



  getUserGroupAvailable(userId: any, domainId:any): Observable<any> {
    return this.http.get(this.baseUrl + 'Groups/getAvailable' + '?userId=' + userId + '&domainId=' + domainId);    
  }
  getUserGroupAssigned(userId: any): Observable<any> {
    return this.http.get(this.baseUrl + 'Groups/getAssigned' + '?userId=' + userId);    
  }
  postUserGroupSave(data: any): Observable<any> {
    return this.http.post(this.baseUrl + 'UserGroup/add', data);    
  }
  


  setGroupData(groupData:any) {
    this.groupDataObj = groupData;
  }
  getGroupData() {
      return this.groupDataObj;
  }

  setEmpData(empData:any) {
    this.empListDetails = empData;
  }
  getEmpData() {
      return this.empListDetails;
  }
}
