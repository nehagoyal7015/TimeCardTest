import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, EventEmitter } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmployeeMange } from '../Model/employee-mange';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  baseUrl =  environment.apiUrl;
  public pkId:any;

  

  constructor(private http: HttpClient) { }
  protected get headers(): HttpHeaders {
    const headers: HttpHeaders = new HttpHeaders();
    // check authenticationToken w.r.t time
    // if ((authenticationToken && authenticationToken.token && authenticationToken.token.trim() !== '') && (authenticationToken.expiryTime > new Date().getTime())) {
    //   // headers = headers.append('Authorization', 'Bearer ' + authenticationToken.token);
    // } else {
    //   this.router.navigate(['/login']);
    // }
    return headers;
  }
 
  addEmployee(profile:File, data:any) : Observable<any> {   
    const formData = new FormData();
    formData.append('Profile', profile);
    formData.append('data', JSON.stringify(data));
    return this.http.post(this.baseUrl + 'UserDetail/add',formData, { headers: this.headers });
  }
  editHoliday(profile:File, data:any) {
    const formData = new FormData();
    formData.append('Profile', profile);
    formData.append('data', JSON.stringify(data));
    return this.http.put(this.baseUrl + 'UserDetail/editEmployee',formData, { headers: this.headers });
  }

  empDetailList(){
    return this.http.get(this.baseUrl + 'UserDetail/get');
  }

  getEmpDetails(data: any){
    return this.http.get(this.baseUrl + 'UserDetail/userDetailInfo'+ '?pkId=' + data);
  }
  inActiveInfo(pkId: any) {
    return this.http.get(this.baseUrl + 'UserDetail/inActive?pkId=' + pkId);
  }
  activeInfo(pkId: any) {
    return this.http.get(this.baseUrl + 'UserDetail/active?pkId=' + pkId);
  }
  asignProjectInfo(pkId: any) {
    return this.http.get(this.baseUrl + 'UserDetail/asignProject?pkId=' + pkId);
  }
  setPkID(pk:any) {
    this.pkId = pk;
  }
  getPkID() {
      return this.pkId;
  }

}
