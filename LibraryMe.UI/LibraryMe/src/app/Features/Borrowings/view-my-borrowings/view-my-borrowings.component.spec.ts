import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewMyBorrowingsComponent } from './view-my-borrowings.component';

describe('ViewMyBorrowingsComponent', () => {
  let component: ViewMyBorrowingsComponent;
  let fixture: ComponentFixture<ViewMyBorrowingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewMyBorrowingsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewMyBorrowingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
