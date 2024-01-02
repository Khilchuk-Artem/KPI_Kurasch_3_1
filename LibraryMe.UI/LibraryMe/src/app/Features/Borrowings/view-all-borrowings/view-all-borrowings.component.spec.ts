import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAllBorrowingsComponent } from './view-all-borrowings.component';

describe('ViewAllBorrowingsComponent', () => {
  let component: ViewAllBorrowingsComponent;
  let fixture: ComponentFixture<ViewAllBorrowingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewAllBorrowingsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewAllBorrowingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
