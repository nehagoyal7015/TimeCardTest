import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  public errorMessages:any = new EventEmitter<any>();
  constructor() { }
  errorMsg(error:any) {
    this.errorMessages.emit(error);
  }

  errorAlt() {
    return this.errorMessages;
  }

}
