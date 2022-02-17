import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import * as metricSelectors from '../state/metrics.selectors';
import * as connectionSelectors from '../state/connections.selectors';
import * as metricActions from '../state/metrics.actions';
import * as connectionsActions from '../state/connections.actions';
import { MetricService } from '../../services/metric.service';
import { Metric } from '../../models/metric.model';

@Component({
  selector: 'app-metric-page',
  templateUrl: './metric-page.component.html'
})
export class MetricPageComponent implements OnInit {
  metrics$ = this.store.select(metricSelectors.selectAll);
  connections$ = this.store.select(connectionSelectors.selectAll);

  metric: Metric = {} as Metric;

  constructor(private metricService: MetricService, private store: Store) {}

  ngOnInit(): void {
    this.handleRefresh();
  }

  handleAddNew(): void {
    this.metric = {} as Metric;
  }

  handleEdit(metric: Metric): void {
    this.metric = metric;
  }

  handleRefresh(): void {
    this.metricService
      .getMetrics()
      .subscribe(metrics =>
        this.store.dispatch(metricActions.retrievedMetricsList({ metrics }))
      );
    this.metricService.getConnections().subscribe(connections => {
      this.store.dispatch(
        connectionsActions.retrievedConnectionList({ connections })
      );
    });
  }
}
