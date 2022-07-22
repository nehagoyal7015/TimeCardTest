import { Component, OnInit,Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-mat-confirm-dialog',
  templateUrl: './mat-confirm-dialog.component.html',
  styleUrls: ['./mat-confirm-dialog.component.scss']
})
export class MatConfirmDialogComponent implements OnInit {

  constructor(
    
    public dialogRef: MatDialogRef<MatConfirmDialogComponent>) { }

ngOnInit() {
}

closeDialog() {
  this.dialogRef.close(false);
}


}
