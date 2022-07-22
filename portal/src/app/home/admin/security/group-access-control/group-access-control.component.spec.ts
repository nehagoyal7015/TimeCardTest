import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupAccessControlComponent } from './group-access-control.component';

describe('GroupAccessControlComponent', () => {
  let component: GroupAccessControlComponent;
  let fixture: ComponentFixture<GroupAccessControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GroupAccessControlComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupAccessControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
