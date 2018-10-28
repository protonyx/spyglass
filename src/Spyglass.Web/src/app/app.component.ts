import { Component } from '@angular/core';
import { navigationConfig } from './nav.config';

@Component({
  selector: 'sg-app',
  template: `
    <sg-layout [appName]="appName" [navItems]="navItems"></sg-layout>`,
  styles: [`
    
  `]
})
export class AppComponent {
  appName = 'Spyglass';
  navItems = navigationConfig;

  constructor() {}
}

