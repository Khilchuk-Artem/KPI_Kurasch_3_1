import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditVisitorCardComponent } from './edit-visitor-card.component';

describe('EditVisitorCardComponent', () => {
  let component: EditVisitorCardComponent;
  let fixture: ComponentFixture<EditVisitorCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditVisitorCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditVisitorCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
