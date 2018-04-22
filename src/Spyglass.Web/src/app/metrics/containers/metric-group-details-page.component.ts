import {Component, OnInit} from '@angular/core';
import {Store, select} from '@ngrx/store';
import {Observable} from 'rxjs';
import {MetricGroup} from '../models/metricGroup';
import * as MetricGroupActions from '../metricGroup.actions';
import * as fromMetrics from '../../reducers';

@Component({
  template: `
    <sg-metric-group-details [group]="group$ | async"></sg-metric-group-details>
  `
})
export class MetricGroupDetailsPageComponent implements OnInit {
  group$: Observable<MetricGroup>;

  constructor(private store: Store<fromMetrics.State>) {
    this.group$ = store.pipe(
      select(fromMetrics.getSelectedGroup)
    );
  }

  ngOnInit() {

  }
}
