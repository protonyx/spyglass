import { Component } from '@angular/core';

@Component({
  selector: 'sg-app',
  templateUrl: './app.component.html',
  styles: [`
    mat-sidenav-container {
      background: rgba(0, 0, 0, 0.03);
    }

    mat-sidenav {
      width: 300px;
    }
  `]
})
export class AppComponent {
  navItems = [
    {name: 'Home', route: '', icon: 'home'},
    {name: 'Dashboard', route: '/dashboard', icon: 'dashboard'},
    {name: 'Metrics', route: '/groups', icon: 'assessment'},
    {name: 'Credentials', route: '/credentials', icon: 'lock outline'}
  ];

  constructor() {}
}

