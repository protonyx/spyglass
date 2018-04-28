import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import {Action} from '@ngrx/store';
import {Observable} from 'rxjs';
import 'rxjs/add/observable/of';
import {
  MetricActionTypes,
  LoadGroups,
  LoadGroupsSuccessful,
  LoadGroupsFailure,
  LoadMetrics,
  LoadMetricsSuccessful,
  LoadMetricsFailure
} from '../actions/metrics.actions';
import {map, switchMap, catchError} from 'rxjs/operators';
import {MetricService} from '../services/metric.service';
import {MetricGroup} from '../models/metricGroup';
import {Metric} from "../models/metric";


@Injectable()
export class MetricsEffects {
  @Effect()
  loadGroups$: Observable<Action> = this.actions$.pipe(
    ofType<LoadGroups>(MetricActionTypes.LoadGroups),
    switchMap(action => {
      return this.metricService.getGroups().pipe(
        map((groups: MetricGroup[]) => new LoadGroupsSuccessful(groups)),
        catchError(error => Observable.of(new LoadGroupsFailure(error)))
      );
    })
  );

  @Effect()
  loadMetrics$: Observable<Action> = this.actions$.pipe(
    ofType<LoadMetrics>(MetricActionTypes.LoadMetrics),
    switchMap(action => {
      return this.metricService.getMetrics().pipe(
        map((metrics: Metric[]) => new LoadMetricsSuccessful(metrics)),
        catchError(error => Observable.of(new LoadMetricsFailure(error)))
      )
    })
  );

  constructor(private actions$: Actions,
              private metricService: MetricService) {}
}
