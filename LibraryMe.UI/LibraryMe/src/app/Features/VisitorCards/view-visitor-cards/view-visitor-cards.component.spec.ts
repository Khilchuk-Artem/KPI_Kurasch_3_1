import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewVisitorCardsComponent } from './view-visitor-cards.component';

describe('ViewVisitorCardsComponent', () => {
  let component: ViewVisitorCardsComponent;
  let fixture: ComponentFixture<ViewVisitorCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewVisitorCardsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewVisitorCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
