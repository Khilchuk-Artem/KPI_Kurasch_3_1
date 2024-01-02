import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewBookbagComponent } from './view-bookbag.component';

describe('ViewBookbagComponent', () => {
  let component: ViewBookbagComponent;
  let fixture: ComponentFixture<ViewBookbagComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewBookbagComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewBookbagComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
