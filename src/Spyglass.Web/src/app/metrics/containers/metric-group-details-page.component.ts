import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from '@angular/core';
import {Store, select} from '@ngrx/store';
import {Observable} from 'rxjs';
import {MetricGroup} from '../models/metricGroup';
import * as MetricGroupActions from '../actions/metrics.actions';
import * as fromMetrics from '../reducers';
import {ActivatedRoute} from "@angular/router";
import {Subscription} from "rxjs/Subscription";
import {map} from "rxjs/operators";

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <sg-metric-group-details [group]="group$ | async"></sg-metric-group-details>
  `
})
export class MetricGroupDetailsPageComponent implements OnDestroy {
  actionsSubscription: Subscription;
  group$: Observable<MetricGroup>;

  constructor(private store: Store<fromMetrics.State>,
              private route: ActivatedRoute) {
    this.actionsSubscription = route.params.pipe(
      map(params => new MetricGroupActions.SelectMetricGroup(params.id))
    )
      .subscribe(store);

    this.group$ = store.pipe(
      select(fromMetrics.getSelectedGroup)
    );
  }

  ngOnDestroy() {
    this.actionsSubscription.unsubscribe();
  }
}
