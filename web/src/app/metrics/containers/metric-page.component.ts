import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import * as selectors from '../state/metrics.selectors';
import * as actions from '../state/metrics.actions';
import { MetricService } from '../../services/metric.service';

@Component({
  selector: 'app-metric-page',
  templateUrl: './metric-page.component.html'
})
export class MetricPageComponent implements OnInit {
  metrics$ = this.store.select(selectors.selectAll);

  constructor(private metricService: MetricService, private store: Store) {}

  ngOnInit(): void {
    this.metricService
      .getMetrics()
      .subscribe(metrics =>
        this.store.dispatch(actions.retrievedMetricsList({ metrics }))
      );
  }
}
