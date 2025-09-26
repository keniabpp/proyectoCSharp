import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Register } from './register';


import { Login } from '../login/login';


describe('Register', () => {
  let component: Register;
  let fixture: ComponentFixture<Register>;


  beforeEach(async () => {
    
    await TestBed.configureTestingModule({
      imports: [
        Register,
        Login,
        
      ],

      
    })
      .compileComponents();

    fixture = TestBed.createComponent(Register);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  

  
});
