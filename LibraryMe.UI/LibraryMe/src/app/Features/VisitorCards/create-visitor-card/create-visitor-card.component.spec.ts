import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateVisitorCardComponent } from './create-visitor-card.component';

describe('CreateVisitorCardComponent', () => {
  let component: CreateVisitorCardComponent;
  let fixture: ComponentFixture<CreateVisitorCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateVisitorCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateVisitorCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
