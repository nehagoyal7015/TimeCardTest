import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesheetprojectreportComponent } from './timesheetprojectreport.component';

describe('TimesheetprojectreportComponent', () => {
  let component: TimesheetprojectreportComponent;
  let fixture: ComponentFixture<TimesheetprojectreportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimesheetprojectreportComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesheetprojectreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
