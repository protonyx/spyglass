import { Component } from '@angular/core';

@Component({
  selector: 'sg-app',
  template: `
    <sg-layout [navItems]="navItems"></sg-layout>`,
  styles: [`
    
  `]
})
export class AppComponent {
  navItems = [
    {name: 'Home', route: '', icon: 'home'},
    {name: 'Dashboard', route: '/dashboard', icon: 'dashboard'},
    {name: 'Metrics', route: '/metrics', icon: 'assessment'},
    {name: 'Credentials', route: '/credentials', icon: 'lock outline'}
  ];

  constructor() {}
}

