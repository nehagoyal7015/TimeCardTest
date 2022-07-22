import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  AddTimeSheet(timeSheet: any): Observable<any> {
    return this.http.post(this.baseUrl + 'TimeSheet/add', timeSheet);
  }

  GetUserProjects(): Observable<any> {
    return this.http.get(this.baseUrl + 'ProjectUser/get');
  }


  GetTimeSheet(date: string): Observable<any> {
    return this.http.get(this.baseUrl + 'TimeSheet/get?date=' + date);
  }
  DeleteTimeSheet(timeSheetId: number) {
    return this.http.delete(this.baseUrl + 'TimeSheet/delete?timeSheetId=' + timeSheetId);
  }

  GetWorkHr(date: string): Observable<any> {
    return this.http.get(this.baseUrl + 'TimeSheet/getworkinghr?date=' + date);
  }

  GetWeekHr(): Observable<any> {
    return this.http.get(this.baseUrl + 'TimeSheet/getWeekHr');
  }

  GetMonthHr(): Observable<any> {
    return this.http.get(this.baseUrl + 'TimeSheet/getMonthHr');
  }

  GetRecentProj(): Observable<any> {
    return this.http.get(this.baseUrl + 'ProjectUser/getRecent');
  }

  
}
