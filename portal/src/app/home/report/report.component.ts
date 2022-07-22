import { Component, OnInit } from '@angular/core';
import html2canvas from 'html2canvas';
import jspdf from 'jspdf';
import { LoaderServiceService } from './../../utils/loader/loader-service.service';


@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {
 
  constructor( private loaderService: LoaderServiceService) { }

  ngOnInit(): void {
  }
 

  public captureScreen()  
  {  
    var data = document.getElementById('contentToConvert');
    if(data){
    html2canvas(data).then(canvas => {

      var imgWidth = 208;   
      var pageHeight = 295;    
      var imgHeight = canvas.height * imgWidth / canvas.width;  
      var heightLeft = imgHeight;  
  
      const contentDataURL = canvas.toDataURL('image/png')  
      let pdf = new jspdf('p', 'mm', 'a4'); // A4 size page of PDF  
      var position = 0;  
      pdf.addImage(contentDataURL, 'PN   G', 0, position, imgWidth, imgHeight)  
      pdf.save('File.pdf'); // Generated PDF   
    });  
  }
  }  
}
 
   
  