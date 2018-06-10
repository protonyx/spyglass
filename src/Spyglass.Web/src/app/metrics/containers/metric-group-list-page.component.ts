import {Component, OnInit} from '@angular/core';
import {Store, select} from '@ngrx/store';
import {Observable} from 'rxjs';
import {MetricGroup} from '../models/metricGroup';
import * as MetricGroupActions from '../actions/metrics.actions';
import * as fromMetrics from '../reducers';

@Component({
  template: `
    <mat-sidenav-container>
      <mat-sidenav style="display: flex">
        <sg-group-list 
          [groups]="groups$ | async" 
          (selectGroup)="handleSelectGroup($event)"
          (createGroup)="handleCreateGroup()">
        </sg-group-list>
      </mat-sidenav>
      <mat-sidenav-content>
        <router-outlet></router-outlet>
      </mat-sidenav-content>
    </mat-sidenav-container>
      
  `
})
export class MetricGroupListPageComponent implements OnInit {
  groups$: Observable<MetricGroup[]>;
  loading$: Observable<boolean>;

  constructor(private store: Store<fromMetrics.State>) {
    this.groups$ = store.pipe(
      select(fromMetrics.getAllGroups)
    );
    this.loading$ = store.pipe(select(fromMetrics.getGroupsLoading));
  }

  handleSelectGroup(selection: MetricGroup) {
    this.store.dispatch(new MetricGroupActions.SelectMetricGroup(selection.id));
  }

  handleCreateGroup() {
    // this.store.dispatch(new MetricGroupActions.CreateMetricGroup())
  }

  ngOnInit() {
    this.store.dispatch(new MetricGroupActions.LoadGroups())
  }
}
