import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from '@angular/core';
import {Observable, Subscription} from 'rxjs';
import {Metric} from '../models/metric';
import * as MetricActions from '../actions/metrics.actions';
import * as fromMetrics from '../reducers';
import {select, Store} from '@ngrx/store';
import {ActivatedRoute, Router} from '@angular/router';
import {map} from 'rxjs/operators';
import {MetricProvider} from '../models/metricProvider';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <sg-metric-details 
      [metric]="metric$ | async"
      [providers]="providers$ | async"
      (edit)="handleEdit()">
    </sg-metric-details>
  `,
  styles: []
})
export class MetricDetailsPageComponent implements OnDestroy {
  actionsSubscription: Subscription;
  metric$: Observable<Metric>;
  providers$: Observable<MetricProvider[]>;

  constructor(
    private store: Store<fromMetrics.State>,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.actionsSubscription = route.params
      .pipe(
        map(params => new MetricActions.SelectMetric(params.id))
      )
      .subscribe(store);

    this.metric$ = store.pipe(
      select(fromMetrics.getSelectedMetric)
    );
    this.providers$ = store.pipe(
      select(fromMetrics.getProviders)
    );
  }

  ngOnDestroy() {
    this.actionsSubscription.unsubscribe();
  }

  handleEdit() {
    this.router.navigate(['edit'], {
      relativeTo: this.route
    })
  }

}
