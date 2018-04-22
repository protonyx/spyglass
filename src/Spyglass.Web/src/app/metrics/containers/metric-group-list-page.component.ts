import {Component, OnInit} from '@angular/core';
import {Store, select} from '@ngrx/store';
import {Observable} from 'rxjs';
import {MetricGroup} from '../models/metricGroup';
import * as MetricGroupActions from '../metricGroup.actions';
import * as fromMetrics from '../../reducers';

@Component({
  template: `
    <mat-sidenav-container>
      <mat-sidenav>
        <sg-group-list 
          [groups]="groups$ | async" 
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
      select(fromMetrics.getGroups)
    );
    this.loading$ = store.pipe(select(fromMetrics.getLoading));
  }

  handleCreateGroup() {
    this.store.dispatch(new MetricGroupActions.CreateMetricGroup())
  }

  ngOnInit() {
    this.store.dispatch(new MetricGroupActions.LoadMetricGroups())
  }
}
