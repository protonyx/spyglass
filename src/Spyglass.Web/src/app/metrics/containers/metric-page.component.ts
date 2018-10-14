import { Component, OnInit } from '@angular/core';
import * as MetricActions from "../actions/metrics.actions";
import {select, Store} from "@ngrx/store";
import {MetricGroup} from "../models/metricGroup";
import {Observable} from "rxjs";
import * as fromMetrics from "../reducers";
import {Metric} from "../models/metric";
import {MatDialog} from "@angular/material";
import {MetricGroupEditorDialogComponent} from "./metric-group-editor-dialog.component";
import {Router} from '@angular/router';

@Component({
  selector: 'sg-metric-page',
  template: `
    <sg-metric-list [metrics]="metrics$ | async"
                    (createMetric)="handleCreateMetric()"></sg-metric-list>
  `,
  styles: []
})
export class MetricPageComponent implements OnInit {
  metrics$: Observable<Metric[]>;
  loading$: Observable<boolean>;

  constructor(
    public dialog: MatDialog,
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
    this.store.dispatch(new MetricActions.LoadMetrics());
    this.store.dispatch(new MetricActions.LoadProviders());
  }
}
