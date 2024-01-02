import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewMyReservationsComponent } from './view-my-reservations.component';

describe('ViewMyReservationsComponent', () => {
  let component: ViewMyReservationsComponent;
  let fixture: ComponentFixture<ViewMyReservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewMyReservationsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewMyReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
