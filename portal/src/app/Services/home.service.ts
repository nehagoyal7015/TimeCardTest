import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class HomeService {
  baseUrl =  environment.apiUrl;
  constructor(private http: HttpClient) { }

  getCurrentUser(userId : any): Observable<any> {
    return this.http.get(this.baseUrl + 'UserAccount/getCurrentUser?userId=' + userId);
  }
  getEmployeeInfo(userId:any):Observable<any>
  {
    return this.http.get(this.baseUrl +'UserAccount/ProfileInfo?userId=' + userId)
  }
  editProfile(data:any) {
    return this.http.put(this.baseUrl + 'UserAccount/updateProfile',data);
  }
}
