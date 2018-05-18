import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from '@angular/core';
import {Observable, Subscription} from 'rxjs';
import {Metric} from '../models/metric';
import * as MetricActions from '../actions/metrics.actions';
import * as fromMetrics from '../reducers';
import {select, Store} from '@ngrx/store';
import {ActivatedRoute} from '@angular/router';
import {map, tap} from 'rxjs/operators';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <sg-metric-details [metric]="metric$ | async"></sg-metric-details>
  `,
  styles: []
})
export class MetricDetailsPageComponent implements OnDestroy {
  actionsSubscription: Subscription;
  metric$: Observable<Metric>;

  constructor(
    private store: Store<fromMetrics.State>,
    private route: ActivatedRoute) {
    this.actionsSubscription = route.params
      .pipe(map(params => new MetricActions.SelectMetric(params.id)))
      .subscribe(store);

    this.metric$ = store.pipe(
      select(fromMetrics.getSelectedMetric)
    );
  }

  ngOnDestroy() {
    this.actionsSubscription.unsubscribe();
  }

}
