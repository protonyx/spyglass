import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import '@cds/core/icon/register.js';
import { ClarityIcons, bugIcon, plusIcon } from '@cds/core/icon';

ClarityIcons.addIcons(bugIcon);
ClarityIcons.addIcons(plusIcon);

import * as fromMetrics from './metrics/state/metrics.actions';
import { MetricService } from './services/metric.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Spyglass';
  navItems = [
    { name: 'Home', route: '', icon: 'home' },
    { name: 'Metrics', route: '/metrics', icon: 'assessment' },
    { name: 'Connections', route: '/connections', icon: 'lock outline' }
  ];

  constructor() {}

  ngOnInit() {}
}
