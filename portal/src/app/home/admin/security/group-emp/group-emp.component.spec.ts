import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupEmpComponent } from './group-emp.component';

describe('GroupEmpComponent', () => {
  let component: GroupEmpComponent;
  let fixture: ComponentFixture<GroupEmpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GroupEmpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
