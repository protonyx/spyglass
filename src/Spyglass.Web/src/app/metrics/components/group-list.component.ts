import {Component, OnInit} from '@angular/core';
import {Store, select} from '@ngrx/store';
import {Observable} from 'rxjs';
import {MetricGroup} from '../models/metricGroup';
import * as MetricGroupActions from '../metricGroup.actions';
import * as fromMetrics from '../reducers';

@Component({
  templateUrl: './group-list.component.html'
})
export class GroupListComponent implements OnInit {
  groups$: Observable<MetricGroup[]>;

  constructor(private store: Store<fromMetrics.State>) {
    this.groups$ = store.pipe(select(fromMetrics.getGroups))
  }

  createGroup() {
    this.store.dispatch(new MetricGroupActions.CreateMetricGroup())
  }

  ngOnInit() {
    this.store.dispatch(new MetricGroupActions.LoadMetricGroups())
  }
}
