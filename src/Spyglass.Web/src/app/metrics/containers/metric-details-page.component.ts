import { ChangeDetectionStrategy, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Actions, ofType } from '@ngrx/effects';
import { select, Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import * as MetricActions from '../actions/metrics.actions';
import { SaveMetricSuccessful } from '../actions/metrics.actions';
import { DeleteMetricSuccessful } from '../actions/metrics.actions';
import { MetricActionTypes } from '../actions/metrics.actions';
import { Metric } from '../models/metric';
import { MetricProvider } from '../models/metricProvider';
import * as fromMetrics from '../reducers';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <sg-metric-details
      [metric]="metric$ | async"
      [providers]="providers$ | async"
      (edit)="handleEdit($event)"
      (delete)="handleDelete($event)">
    </sg-metric-details>
  `,
  styles: []
})
export class MetricDetailsPageComponent implements OnDestroy {
  paramsSubscription: Subscription;
  actionsSubscription: Subscription;

  metric$: Observable<Metric>;
  providers$: Observable<MetricProvider[]>;

  constructor(
    private store: Store<fromMetrics.State>,
    private router: Router,
    private route: ActivatedRoute,
    private actions$: Actions
  ) {
    this.paramsSubscription = route.params
      .pipe(
        map(params => new MetricActions.SelectMetric(params.id))
      )
      .subscribe(store);
    this.actionsSubscription = actions$.pipe(
      ofType<DeleteMetricSuccessful>(
        MetricActionTypes.DeleteMetricSuccessful
      )
    )
      .subscribe(a => {
        this.router.navigate(['..'], {
          relativeTo: this.route
        });
      });

    this.metric$ = store.pipe(
      select(fromMetrics.getSelectedMetric)
    );
    this.providers$ = store.pipe(
      select(fromMetrics.getProviders)
    );
  }

  ngOnDestroy() {
    this.paramsSubscription.unsubscribe();
    this.actionsSubscription.unsubscribe();
  }

  handleEdit(id: string) {
    this.router.navigate(['edit'], {
      relativeTo: this.route
    });
  }

  handleDelete(id: string) {
    this.store.dispatch(new MetricActions.DeleteMetric(id));
  }

}
