import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewBookmarksComponent } from './view-bookmarks.component';

describe('ViewBookmarksComponent', () => {
  let component: ViewBookmarksComponent;
  let fixture: ComponentFixture<ViewBookmarksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewBookmarksComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewBookmarksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
