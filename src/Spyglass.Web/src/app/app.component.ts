import {Component, OnInit} from '@angular/core';
import { navigationConfig } from './nav.config';
import * as MetricActions from './metrics/actions/metrics.actions';
import {Store} from '@ngrx/store';
import * as fromMetrics from "./metrics/reducers";

@Component({
  selector: 'sg-app',
  template: `
    <sg-layout [appName]="appName" [navItems]="navItems"></sg-layout>`,
  styles: [`
    
  `]
})
export class AppComponent implements OnInit {
  appName = 'Spyglass';
  navItems = navigationConfig;

  constructor(
    private store: Store<fromMetrics.State>
  ) {}

  ngOnInit() {
    this.store.dispatch(new MetricActions.LoadProviders());
  }
}

