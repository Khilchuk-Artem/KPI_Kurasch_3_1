import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewVisitorCardComponent } from './view-visitor-card.component';

describe('ViewVisitorCardComponent', () => {
  let component: ViewVisitorCardComponent;
  let fixture: ComponentFixture<ViewVisitorCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewVisitorCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewVisitorCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
