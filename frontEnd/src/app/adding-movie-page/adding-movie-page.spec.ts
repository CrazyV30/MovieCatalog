import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AddingMoviePage } from './adding-movie-page';

describe('AddingMoviePage', () => {
  let component: AddingMoviePage;
  let fixture: ComponentFixture<AddingMoviePage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddingMoviePage],
    }).compileComponents();

    fixture = TestBed.createComponent(AddingMoviePage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
