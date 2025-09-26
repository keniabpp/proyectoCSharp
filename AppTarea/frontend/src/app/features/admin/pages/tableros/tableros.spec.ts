import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Tableros } from './tableros';

describe('Tableros', () => {
  let component: Tableros;
  let fixture: ComponentFixture<Tableros>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Tableros]
    }).compileComponents();

    fixture = TestBed.createComponent(Tableros);
    component = fixture.componentInstance;

    
    
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  
});
