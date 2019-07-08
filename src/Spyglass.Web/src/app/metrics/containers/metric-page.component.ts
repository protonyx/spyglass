import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import * as MetricActions from '../actions/metrics.actions';
import { Metric } from '../models/metric';
import * as fromMetrics from '../reducers';

@Component({
  selector: 'sg-metric-page',
  template: `
    <h1>Metrics</h1>
    <div>
      <button class="btn btn-outline" (click)="handleCreateMetric()">Create Metric</button>
      <button class="btn btn-link" (click)="refreshMetrics()">Refresh</button>
    </div>
    <sg-metric-list [metrics]="metrics$ | async"
                    (createMetric)="handleCreateMetric()"></sg-metric-list>
  `,
  styles: []
})
export class MetricPageComponent implements OnInit {
  metrics$: Observable<Metric[]>;
  loading$: Observable<boolean>;

  constructor(
    private store: Store<fromMetrics.State>,
    private router: Router
  ) {
    this.metrics$ = store.pipe(
      select(fromMetrics.getAllMetrics)
    );
    this.loading$ = store.pipe(select(fromMetrics.getMetricsLoading));
  }

  handleCreateMetric() {
    this.router.navigate(['metrics', 'new']);
  }

  ngOnInit() {
    this.store.dispatch(new MetricActions.LoadProviders());
    this.refreshMetrics();
  }

  refreshMetrics() {
    this.store.dispatch(new MetricActions.LoadMetrics());
  }
}
