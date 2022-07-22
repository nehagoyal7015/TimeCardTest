import { Component, OnInit, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { parentCat, categoryList, AddNewDoc } from 'src/app/Model/docs.model';
import { DocumentService } from 'src/app/Services/document.service';

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.scss']
})
export class DocumentComponent implements OnInit {

  docList: parentCat[];
  catList: categoryList[];
  documentName: string;
  details: string;
  description: string;
  catName: string;
  parentCatName: string;
  parentCatId: number;
  userId: number;
  domainId:number;
  addNewDoc= new AddNewDoc();

  constructor(private documentService: DocumentService,public dialog: MatDialog,private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    let currentUser = JSON.parse(localStorage.getItem('user') as string);
    this.userId = currentUser.data.userId;
    this.domainId = currentUser.data.domainId;
    this.getDocs();
  }

  openDialog(templateRef: TemplateRef<any>) {
    this.dialog.open(templateRef, {
      width: '500px',
    });
  }

  getDocs() {

    this.documentService.getDocs()
      .subscribe((result: any) => {
        this.docList = result.data;
        console.log(this.docList);
      },
        (error) => {
          console.log(error.error.message, error.error.messageType);
        });
  }

  addDocs(categoryParentId: number, parentCategoryName: string)
  {
      this.parentCatName = parentCategoryName;
      this.parentCatId = categoryParentId;

      this.documentService.getDocParent(1, parentCategoryName)
      .subscribe((result: any) => {
        this.catList = result.data;
        console.log(this.catList);
      },
        (error) => {
          console.log(error.error.message, error.error.messageType);
        });

  }

  submitDoc(doc: any) 
  {
    this.addNewDoc.categoryParentId = this.parentCatId;
    this.addNewDoc.description = doc.description;
    this.addNewDoc.details = doc.details;
    this.addNewDoc.documentName = doc.documentName;
    this.addNewDoc.domainId = this.domainId;
    this.addNewDoc.userId = this.userId;
    this.dialog.closeAll();
    if (this.parentCatName == "Domain")
    {
      this.addNewDoc.categoryName = doc.catName.name;
      console.log(this.addNewDoc);
      this.documentService.AddDocument((JSON.parse(JSON.stringify(this.addNewDoc)))).subscribe((result: any) => {
        if (result.data) {
          this._snackBar.open('Record Saved Successfully!', 'OK', {
            panelClass: 'my-custom-snackbar',
            duration: 2 * 1000,
          });
        } else {
          this._snackBar.open('Can not add record', 'OK', {
            panelClass: 'my-custom-snackbar',
            duration: 2 * 1000,
          });
        }
      },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
    }

    if (this.parentCatName == "Client")
    {
      this.addNewDoc.categoryName = doc.catName.name;
      this.addNewDoc.clientId = doc.catName.id;

      this.documentService.AddDocument((JSON.parse(JSON.stringify(this.addNewDoc)))).subscribe((result: any) => {
        if (result.data) {
          this._snackBar.open('Document saved successfully!', 'OK', {
            panelClass: 'my-custom-snackbar',
            duration: 2 * 1000,
          });
        } else {
          this._snackBar.open('Can not add record', 'OK', {
            panelClass: 'my-custom-snackbar',
            duration: 2 * 1000,
          });
        }
      },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
    }

    if (this.parentCatName == "Project")
    {
      this.addNewDoc.categoryName = doc.catName.name;
      this.addNewDoc.projectId = doc.catName.id;

      this.documentService.AddDocument((JSON.parse(JSON.stringify(this.addNewDoc)))).subscribe((result: any) => {
        if (result.data) {
          this._snackBar.open('Document saved successfully!', 'OK', {
            panelClass: 'my-custom-snackbar',
            duration: 2 * 1000,
          });
        } else {
          this._snackBar.open('Can not add document', 'OK', {
            panelClass: 'my-custom-snackbar',
            duration: 2 * 1000,
          });
        }
      },
      (error) => {
        console.log(error.error.message, error.error.messageType);
      });
    }
    this.getDocs();
  }

}
