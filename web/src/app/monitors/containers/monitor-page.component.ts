import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import * as metricSelectors from '../state/monitors.selectors';
import * as connectionSelectors from '../state/connections.selectors';
import * as metricActions from '../state/monitors.actions';
import * as connectionsActions from '../state/connections.actions';
import { MonitorService } from '../../services/monitor.service';
import { Monitor } from '../../models/monitor.model';

@Component({
  selector: 'app-monitor-page',
  templateUrl: './monitor-page.component.html'
})
export class MonitorPageComponent implements OnInit {
  monitors$ = this.store.select(metricSelectors.selectAll);
  connections$ = this.store.select(connectionSelectors.selectAll);

  monitor: Monitor = {} as Monitor;

  constructor(private monitorService: MonitorService, private store: Store) {}

  ngOnInit(): void {
    this.handleRefresh();
  }

  handleAddNew(): void {
    this.monitor = {} as Monitor;
  }

  handleEdit(monitor: Monitor): void {
    this.monitor = monitor;
  }

  handleRefresh(): void {
    this.monitorService
      .getMonitors()
      .subscribe(monitors =>
        this.store.dispatch(metricActions.retrievedMetricsList({ monitors }))
      );
    this.monitorService.getConnections().subscribe(connections => {
      this.store.dispatch(
        connectionsActions.retrievedConnectionList({ connections })
      );
    });
  }
}
