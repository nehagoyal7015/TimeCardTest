import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActiveDialogComponent } from '../components/active-dialog/active-dialog.component';
import { DeleteConfirmDialogeComponent } from '../components/delete-confirm-dialoge/delete-confirm-dialoge.component';
import { InactiveDialogComponent } from '../components/inactive-dialog/inactive-dialog.component';
import { MatConfirmDialogComponent } from '../Material/mat-confirm-dialog/mat-confirm-dialog.component';


@Injectable({
  providedIn: 'root'
})
export class DialogService { 

  constructor(private dialog : MatDialog) { }

  openConfirmDialog(){
    return this.dialog.open(MatConfirmDialogComponent,{
       width: '390px',
       panelClass: 'confirm-dialog-container',
       disableClose: true,
       position: { top: "10px" },
      
     });
  }

  deleteConfirmDialog(){
    return this.dialog.open(DeleteConfirmDialogeComponent,{
       width: '390px',
       panelClass: 'confirm-dialog-container',
       disableClose: true,
       position: { top: "10px" },
      
     });
  }
  ActiveDialog(){
    return this.dialog.open(ActiveDialogComponent,{
       width: '390px',
       panelClass: 'confirm-dialog-container',
       disableClose: true,
       position: { top: "10px" },
      
     });
  }
  InactiveDialog(){
    return this.dialog.open(InactiveDialogComponent,{
       width: '390px',
       panelClass: 'confirm-dialog-container',
       disableClose: true,
       position: { top: "10px" },
      
     });
  }
}
