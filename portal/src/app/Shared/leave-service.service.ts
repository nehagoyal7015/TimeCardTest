import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Leave } from '../Types/leave';
import { HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LeaveServiceService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getLeave(userId : any,year:any): Observable<any> {
    return this.http.get(this.baseUrl + 'UserLeave/get?userId=' +userId+'&year='+year);
  }
  addLeave(leave: Leave): Observable<Leave> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Leave>(this.baseUrl + 'UserLeave/add', leave, httpOptions);
  }
  addNewLeave(leave: Leave, userId:any): Observable<Leave> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Leave>(this.baseUrl + 'UserLeave/addNewUser?userId=' + userId, leave, httpOptions);
  }
  cancelLeave(LeaveId: number) {
    return this.http.get(this.baseUrl + 'UserLeave/cancel?LeaveId=' + LeaveId);
  }
  getUser(userId : any): Observable<any> {
    return this.http.get(this.baseUrl + 'UserLeave/getUser?userId=' + userId);
  }
  getUserLeaveInfo(userId: any): Observable<any> {
   
    return this.http.get(this.baseUrl + 'UserLeave/CountLeaves?userId=' + userId);
  }
  getAllUpcomingLeave(): Observable<any> {
    return this.http.get(this.baseUrl + 'UserLeave/getAllUpcomingLeave');
  }

}
