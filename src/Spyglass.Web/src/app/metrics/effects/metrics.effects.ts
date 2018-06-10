import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import {Action} from '@ngrx/store';
import {Observable, of} from 'rxjs';
import {
  MetricActionTypes,
  LoadGroups,
  LoadGroupsSuccessful,
  LoadGroupsFailure,
  LoadMetrics,
  LoadMetricsSuccessful,
  LoadMetricsFailure,
  CreateMetricGroup,
  CreateMetricGroupSuccessful,
  CreateMetricGroupFailure,
  CreateMetric,
  CreateMetricSuccessful,
  CreateMetricFailure
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
        catchError(error => of(new LoadGroupsFailure(error)))
      );
    })
  );

  @Effect()
  createGroup$: Observable<Action> = this.actions$.pipe(
    ofType<CreateMetricGroup>(MetricActionTypes.CreateGroup),
    switchMap(action => {
      return this.metricService.createGroup(action.payload).pipe(
        map((group: MetricGroup) => new CreateMetricGroupSuccessful(group)),
        catchError(error => of(new CreateMetricGroupFailure(error)))
      );
    })
  );

  @Effect()
  loadMetrics$: Observable<Action> = this.actions$.pipe(
    ofType<LoadMetrics>(MetricActionTypes.LoadMetrics),
    switchMap(action => {
      return this.metricService.getMetrics().pipe(
        map((metrics: Metric[]) => new LoadMetricsSuccessful(metrics)),
        catchError(error => of(new LoadMetricsFailure(error)))
      )
    })
  );

  @Effect()
  createMetric$: Observable<Action> = this.actions$.pipe(
    ofType<CreateMetric>(MetricActionTypes.CreateMetric),
    switchMap(action => {
      return this.metricService.createMetric(action.payload).pipe(
        map((metric: Metric) => new CreateMetricSuccessful(metric)),
        catchError(error => of(new CreateMetricFailure(error)))
      );
    })
  );

  constructor(private actions$: Actions,
              private metricService: MetricService) {}
}
