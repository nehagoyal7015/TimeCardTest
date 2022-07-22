import { Injectable, Output, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoaderServiceService {

  public lodingSpinner = new EventEmitter<boolean>();

  constructor() { }

  emitLoadingChangeEvent(boln: any){
 
    if(boln) {
      this.lodingSpinner.emit(boln);
    } else {
      this.lodingSpinner.emit(boln);
    }
  }

  spinner(){

    return this.lodingSpinner;
  }

  getDateInMMDDYYYY(dateStr: string) {
    let _dateStr;
    if (dateStr == null || dateStr === '') {
      return '';
    } else {
      _dateStr = new Date(dateStr);
      return ('0' + (_dateStr.getMonth() + 1)).slice(-2) + '/' + ('0' + _dateStr.getDate()).slice(-2) + '/' + _dateStr.getFullYear();
    }
  }
}
