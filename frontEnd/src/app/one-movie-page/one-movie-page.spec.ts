import { ComponentFixture, TestBed } from '@angular/core/testing';
import { OneMoviePage } from './one-movie-page';

describe('OneMoviePage', () => {
  let component: OneMoviePage;
  let fixture: ComponentFixture<OneMoviePage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OneMoviePage],
    }).compileComponents();

    fixture = TestBed.createComponent(OneMoviePage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
