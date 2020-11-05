import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LendDialogComponent } from './lend-dialog.component';

describe('LendDialogComponent', () => {
  let component: LendDialogComponent;
  let fixture: ComponentFixture<LendDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LendDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LendDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
