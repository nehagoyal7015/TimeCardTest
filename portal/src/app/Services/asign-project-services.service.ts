import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AsignProjectServicesService {

  baseUrl = environment.apiUrl;
  public userId :any;
  public pkId:any;
  constructor(private http: HttpClient) { }

  getProjectList(userId:any){
    return this.http.get(this.baseUrl + 'UserDetail/projectListInfo?userId='+ userId);
  }
  getEmployeeInfo(pkId: number){
    return this.http.get(this.baseUrl + 'UserDetail/empInformation?pkId='+ pkId);
  }
  getAsignProInfo(){
    return this.http.get(this.baseUrl + 'UserDetail/asignProject');
  }
  AsignProject(data:any) : Observable<any> {
    
    return this.http.post(this.baseUrl + 'UserDetail/addProject', data);
  }
  setPkID(pk:any) {
    this.pkId = pk;
  }
  getPkID() {
      return this.pkId;
  }
  setUserID(user : any){
    this.userId = user;
  }
  getUserID(){
    return this.userId;
  }
}
