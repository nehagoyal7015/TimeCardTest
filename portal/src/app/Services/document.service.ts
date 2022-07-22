import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  baseUrl =  environment.apiUrl; 

  constructor(private http: HttpClient) { }
  getDocs(): Observable<any> {
    return this.http.get(this.baseUrl + 'Docs/get');    
  }
  

  getDocParent(domainId: number, parent: string): Observable<any> {
    return this.http.get(this.baseUrl + 'Docs/get-doc-cat?domainId=' + domainId + '&catname=' + parent);    
  }

  AddDocument(data: any): Observable<any> {
    return this.http.post(this.baseUrl + 'Docs/add-doc', data);    
  }
}
