import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  navItems = [
    {name: 'Metric Groups', route: '/groups'}
  ];

  constructor() {}
}

