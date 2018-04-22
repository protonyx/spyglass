import { Component } from '@angular/core';

@Component({
  selector: 'sg-app',
  templateUrl: './app.component.html'
})
export class AppComponent {
  navItems = [
    {name: 'Dashboard', route: '/dashboard', icon: 'menu'},
    {name: 'Metrics', route: '/groups', icon: 'dashboard'}
  ];

  constructor() {}
}

