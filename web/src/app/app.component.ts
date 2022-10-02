import { Component, OnInit } from '@angular/core';
import '@cds/core/icon/register.js';
import { ClarityIcons, bugIcon, plusIcon, refreshIcon } from '@cds/core/icon';

ClarityIcons.addIcons(bugIcon);
ClarityIcons.addIcons(plusIcon);
ClarityIcons.addIcons(refreshIcon);

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Spyglass';
  navItems = [
    { name: 'Home', route: '', icon: 'home' },
    { name: 'Monitors', route: '/monitors', icon: 'assessment' },
    { name: 'Connections', route: '/connections', icon: 'lock outline' }
  ];

  constructor() {}

  ngOnInit() {}
}
