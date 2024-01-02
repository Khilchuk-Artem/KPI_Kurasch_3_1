import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewBooksByGenreComponent } from './view-books-by-genre.component';

describe('ViewBooksByGenreComponent', () => {
  let component: ViewBooksByGenreComponent;
  let fixture: ComponentFixture<ViewBooksByGenreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewBooksByGenreComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewBooksByGenreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
