import {Component, OnDestroy, OnInit} from '@angular/core';
import {Observable} from 'rxjs/internal/Observable';
import {MetricProvider} from '../models/metricProvider';
import * as fromMetrics from "../reducers";
import {select, Store} from "@ngrx/store";
import {Metric} from '../models/metric';
import {map, tap} from 'rxjs/operators';
import * as MetricActions from '../actions/metrics.actions';
import {Subscription} from 'rxjs/index';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'sg-metric-editor-page',
  template: `
    <sg-metric-editor 
      [metric]="metric$ | async" 
      [providers]="providers$ | async">
    </sg-metric-editor>
  `
})
export class MetricEditorPageComponent implements OnInit, OnDestroy {
  actionsSubscription: Subscription;
  providers$: Observable<MetricProvider[]>;
  metric$: Observable<Metric>;

  constructor(
    private store: Store<fromMetrics.State>,
    private route: ActivatedRoute
  ) {
    this.actionsSubscription = route.params
      .pipe(
        map(params => {
          if (params.id) {
            return new MetricActions.SelectMetric(params.id);
          } else {
            return new MetricActions.SelectNewMetric();
          }
        })
      )
      .subscribe(store);
    this.providers$ = store.pipe(
      select(fromMetrics.getProviders)
    );
    this.metric$ = store.pipe(
      select(fromMetrics.getSelectedMetric)
    );
  }

  ngOnInit() {
    this.store.dispatch(new MetricActions.LoadProviders());
  }

  ngOnDestroy() {
    this.actionsSubscription.unsubscribe();
  }
}
