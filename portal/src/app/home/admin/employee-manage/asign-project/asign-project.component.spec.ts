import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsignProjectComponent } from './asign-project.component';

describe('AsignProjectComponent', () => {
  let component: AsignProjectComponent;
  let fixture: ComponentFixture<AsignProjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AsignProjectComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AsignProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
