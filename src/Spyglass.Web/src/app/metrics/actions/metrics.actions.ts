import { Action } from '@ngrx/store';

import { Metric } from '../models/metric';
import { MetricProvider } from '../models/metricProvider';

export enum MetricActionTypes {
  LoadMetrics = '[Metrics Page] Load Metrics',
  LoadMetricsSuccessful = '[Metrics API] Load Successful',
  LoadMetricsFailure = '[Metrics API] Load Failure',
  SaveMetric = '[Metrics Page] Save Metric',
  SaveMetricSuccessful = '[Metrics API] Save Metric Successful',
  SaveMetricFailure = '[Metrics API] Save Metric Failure',
  SelectMetric = '[Metric Details Page] Select Metric',
  SelectNewMetric = '[New Metric Page] New Metric',
  DeleteMetric = '[Metrics Page] Delete Metric',
  DeleteMetricSuccessful = '[Metrics API] Delete Metric Success',
  DeleteMetricFailure = '[Metrics API] Delete Metric Failure',
  LoadProviders = '[Metrics Page] Load Providers',
  LoadProvidersSuccessful = '[Provider API] Load Successful',
  LoadProvidersFailure = '[Provider API] Load Failure'
}

export class LoadMetrics implements Action {
  readonly type = MetricActionTypes.LoadMetrics;
}

export class LoadMetricsSuccessful implements Action {
  readonly type = MetricActionTypes.LoadMetricsSuccessful;

  constructor(public payload: Metric[]) {}
}

export class LoadMetricsFailure implements Action {
  readonly type = MetricActionTypes.LoadMetricsFailure;

  constructor(public payload: any) {}
}

export class SaveMetric implements Action {
  readonly type = MetricActionTypes.SaveMetric;

  constructor(public payload: Metric) {}
}

export class SaveMetricSuccessful implements Action {
  readonly type = MetricActionTypes.SaveMetricSuccessful;

  constructor(public payload: Metric) {}
}

export class SaveMetricFailure implements Action {
  readonly type = MetricActionTypes.SaveMetricFailure;

  constructor(public payload: any) {}
}

export class SelectMetric implements Action {
  readonly type = MetricActionTypes.SelectMetric;

  constructor(public payload: string) {}
}

export class SelectNewMetric implements Action {
  readonly type = MetricActionTypes.SelectNewMetric;
}

export class DeleteMetric implements Action {
  readonly type = MetricActionTypes.DeleteMetric;

  constructor(public id: string) {}
}

export class DeleteMetricSuccessful implements Action {
  readonly type = MetricActionTypes.DeleteMetricSuccessful;

  constructor(public id: string) {}
}

export class DeleteMetricFailure implements Action {
  readonly type = MetricActionTypes.DeleteMetricFailure;

  constructor(public payload: any) {}
}

export class LoadProviders implements Action {
  readonly type = MetricActionTypes.LoadProviders;
}

export class LoadProvidersSuccessful implements Action {
  readonly type = MetricActionTypes.LoadProvidersSuccessful;

  constructor(public payload: MetricProvider[]) {}
}

export class LoadProvidersFailure implements Action {
  readonly type = MetricActionTypes.LoadProvidersFailure;

  constructor(public payload: any) {}
}


export type MetricsActionsUnion =
  | LoadMetrics
  | LoadMetricsSuccessful
  | LoadMetricsFailure
  | SaveMetric
  | SaveMetricSuccessful
  | SaveMetricFailure
  | SelectMetric
  | SelectNewMetric
  | DeleteMetric
  | DeleteMetricSuccessful
  | DeleteMetricFailure
  | LoadProviders
  | LoadProvidersSuccessful
  | LoadProvidersFailure;
