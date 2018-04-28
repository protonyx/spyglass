import { Component, OnInit } from '@angular/core';
import * as MetricActions from "../actions/metrics.actions";
import {select, Store} from "@ngrx/store";
import {MetricGroup} from "../models/metricGroup";
import {Observable} from "../../../../node_modules/rxjs";
import * as fromMetrics from "../reducers";
import {Metric} from "../models/metric";

@Component({
  selector: 'sg-metric-page',
  template: `
    <mat-sidenav-container>
      <mat-sidenav mode="side" [opened]="true">
        <sg-group-list [groups]="groups$ | async"></sg-group-list>
        

        <a mat-fab color="accent" [routerLink]="['/metrics/new']">
          <mat-icon>add</mat-icon>
        </a>
      </mat-sidenav>
      <mat-sidenav-content>
        <sg-metric-group-details [group]="selectedGroup$ | async"></sg-metric-group-details>
      </mat-sidenav-content>
    </mat-sidenav-container>
  `,
  styles: []
})
export class MetricPageComponent implements OnInit {
  groups$: Observable<MetricGroup[]>;
  selectedGroup$: Observable<MetricGroup>;
  metrics$: Observable<Metric[]>;
  loading$: Observable<boolean>;

  constructor(private store: Store<fromMetrics.State>) {
    this.groups$ = store.pipe(
      select(fromMetrics.getAllGroups)
    );
    this.selectedGroup$ = store.pipe(
      select(fromMetrics.getSelectedGroup)
    );
    // this.metrics$ = store.pipe(
    //   select(fromMetrics.getAllMetrics)
    // );
    this.loading$ = store.pipe(select(fromMetrics.getLoading));
  }

  handleSelectGroup(selection: MetricGroup) {
    this.store.dispatch(new MetricActions.SelectMetricGroup(selection.id));
  }

  handleCreateGroup() {
    this.store.dispatch(new MetricActions.CreateMetricGroup())
  }

  ngOnInit() {
    this.store.dispatch(new MetricActions.LoadGroups());
    //this.store.dispatch(new MetricActions.LoadMetrics());
  }
}
