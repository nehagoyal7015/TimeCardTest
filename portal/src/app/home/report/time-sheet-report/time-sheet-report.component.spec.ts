import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeSheetReportComponent } from './time-sheet-report.component';

describe('TimeSheetReportComponent', () => {
  let component: TimeSheetReportComponent;
  let fixture: ComponentFixture<TimeSheetReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimeSheetReportComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeSheetReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
