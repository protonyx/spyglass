import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { Action } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { catchError, filter, map, switchMap } from 'rxjs/operators';

import {
    DeleteMetric,
    DeleteMetricFailure,
    DeleteMetricSuccessful,
    LoadMetrics,
    LoadMetricsFailure,
    LoadMetricsSuccessful,
    LoadProviders,
    LoadProvidersFailure,
    LoadProvidersSuccessful,
    MetricActionTypes,
    SaveMetric,
    SaveMetricFailure,
    SaveMetricSuccessful
} from '../actions/metrics.actions';
import { Metric } from '../models/metric';
import { MetricProvider } from '../models/metricProvider';
import { MetricService } from '../services/metric.service';

@Injectable()
export class MetricsEffects {
    @Effect() loadMetrics$: Observable<Action> = this.actions$.pipe(
        ofType<LoadMetrics>(MetricActionTypes.LoadMetrics),
        switchMap(action => {
            return this.metricService.getMetrics().pipe(
                map((metrics: Metric[]) => new LoadMetricsSuccessful(metrics)),
                catchError(error => of(new LoadMetricsFailure(error)))
            );
        })
    );

    @Effect() createMetric$: Observable<Action> = this.actions$.pipe(
        ofType<SaveMetric>(MetricActionTypes.SaveMetric),
        filter(t => !t.payload.id),
        switchMap(action => {
            return this.metricService.createMetric(action.payload).pipe(
                map((metric: Metric) => new SaveMetricSuccessful(metric)),
                catchError(error => of(new SaveMetricFailure(error)))
            );
        })
    );

    @Effect() saveMetric$: Observable<Action> = this.actions$.pipe(
        ofType<SaveMetric>(MetricActionTypes.SaveMetric),
        filter(t => !!t.payload.id),
        switchMap(action => {
            return this.metricService.updateMetric(action.payload).pipe(
                map((metric: Metric) => new SaveMetricSuccessful(metric)),
                catchError(error => of(new SaveMetricFailure(error)))
            );
        })
    );

    @Effect() deleteMetric$: Observable<Action> = this.actions$.pipe(
        ofType<DeleteMetric>(MetricActionTypes.DeleteMetric),
        switchMap(action => {
            return this.metricService.deleteMetric(action.id).pipe(
                map((metric: Metric) => new DeleteMetricSuccessful(action.id)),
                catchError(error => of(new DeleteMetricFailure(error)))
            );
        })
    );

    @Effect() loadProviders$: Observable<Action> = this.actions$.pipe(
        ofType<LoadProviders>(MetricActionTypes.LoadProviders),
        switchMap(action => {
            return this.metricService.getProviderMetadata().pipe(
                map((providers: MetricProvider[]) => new LoadProvidersSuccessful(providers)),
                catchError(error => of(new LoadProvidersFailure(error)))
            );
        })
    );

    constructor(private actions$: Actions, private metricService: MetricService) {}
}
