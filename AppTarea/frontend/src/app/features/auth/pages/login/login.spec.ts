import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Login } from './login';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

describe('Login', () => {
  let component: Login;
  let fixture: ComponentFixture<Login>;


  beforeEach(async () => {
    
    
    await TestBed.configureTestingModule({
      imports: [
        Login,
        CommonModule,
        FormsModule,
        

      ],
      
      providers: [
        
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Login);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  

  

 
});
