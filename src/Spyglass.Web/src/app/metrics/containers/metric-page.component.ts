import { Component, OnInit } from '@angular/core';
import * as MetricActions from "../actions/metrics.actions";
import {select, Store} from "@ngrx/store";
import {MetricGroup} from "../models/metricGroup";
import {Observable} from "rxjs";
import * as fromMetrics from "../reducers";
import {Metric} from "../models/metric";
import {MatDialog} from "@angular/material";
import {MetricGroupEditorDialogComponent} from "./metric-group-editor-dialog.component";

@Component({
  selector: 'sg-metric-page',
  template: `
    <mat-sidenav-container>
      <mat-sidenav mode="side" [opened]="true">
        <sg-group-list [groups]="groups$ | async"
          (createGroup)="handleCreateGroup()">
          
        </sg-group-list>
      </mat-sidenav>
      <mat-sidenav-content>
        <sg-metric-list [metrics]="metrics$ | async"
          (createMetric)="handleCreateMetric()"></sg-metric-list>
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

  constructor(
    public dialog: MatDialog,
    private store: Store<fromMetrics.State>
  ) {
    this.groups$ = store.pipe(
      select(fromMetrics.getAllGroups)
    );
    this.selectedGroup$ = store.pipe(
      select(fromMetrics.getSelectedGroup)
    );
    this.metrics$ = store.pipe(
      select(fromMetrics.getAllMetrics)
    );
    this.loading$ = store.pipe(select(fromMetrics.getMetricsLoading));
  }

  handleSelectGroup(selection: MetricGroup) {
    this.store.dispatch(new MetricActions.SelectMetricGroup(selection.id));
  }

  handleCreateGroup() {
    const dialogRef = this.dialog.open(MetricGroupEditorDialogComponent, {
      data: { group: new MetricGroup() }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.store.dispatch(new MetricActions.CreateMetricGroup(result));
    });
  }

  handleCreateMetric() {

  }

  ngOnInit() {
    this.store.dispatch(new MetricActions.LoadGroups());
    this.store.dispatch(new MetricActions.LoadMetrics());
  }
}
