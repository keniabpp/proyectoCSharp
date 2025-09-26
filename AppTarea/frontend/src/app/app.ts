import { Component, inject, signal } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Footer } from './layout/footer/footer';
import { Header } from './layout/header/header';
import { filter } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,CommonModule,Footer,Header],
  templateUrl: './app.html',
  
})

export class App {
  protected readonly title = signal('frontend');

  showMainLayout = true;
  showUserLayout = false;
  private router = inject(Router);

  constructor() {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: any) => {
        // Solo mostrar header/footer principal en login y register
        this.showMainLayout = ['/login', '/register'].includes(event.urlAfterRedirects);
      });
  }
  
}
