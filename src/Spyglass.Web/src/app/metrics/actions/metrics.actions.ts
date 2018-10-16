import { Action } from '@ngrx/store';
import {Metric} from '../models/metric';
import {MetricGroup} from '../models/metricGroup';
import {MetricProvider} from '../models/metricProvider';

export enum MetricActionTypes {
  LoadGroups = '[Metrics Page] Load Groups',
  LoadGroupsSuccessful = '[Metric Group API] Load Successful',
  LoadGroupsFailure = '[Metric Group API] Load Failure',
  CreateGroup = '[Metrics Page] Create Group',
  CreateGroupSuccessful = '[Metric Group API] Create Group Successful',
  CreateGroupFailure = '[Metric Group API] Create Group Failure',
  SelectGroup = '[Metrics Page] Select Group',
  LoadMetrics = '[Metrics Page] Load Metrics',
  LoadMetricsSuccessful = '[Metrics API] Load Successful',
  LoadMetricsFailure = '[Metrics API] Load Failure',
  CreateMetric = '[Metrics Page] Create Metric',
  CreateMetricSuccessful = '[Metrics API] Create Metric Successful',
  CreateMetricFailure = '[Metrics API] Create Metric Failure',
  SelectMetric = '[Metric Details Page] Select Metric',
  SelectNewMetric = '[New Metric Page] New Metric',
  LoadProviders = '[Metrics Page] Load Providers',
  LoadProvidersSuccessful = '[Provider API] Load Successful',
  LoadProvidersFailure = '[Provider API] Load Failure'
}

export class LoadGroups implements Action {
  readonly type = MetricActionTypes.LoadGroups;
}

export class LoadGroupsSuccessful implements Action {
  readonly type = MetricActionTypes.LoadGroupsSuccessful;

  constructor(public payload: MetricGroup[]) {}
}

export class LoadGroupsFailure implements Action {
  readonly type = MetricActionTypes.LoadGroupsFailure;

  constructor(public payload: any) {}
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

export class CreateMetricGroup implements Action {
  readonly type = MetricActionTypes.CreateGroup;

  constructor(public payload: MetricGroup) {}
}

export class CreateMetricGroupSuccessful implements Action {
  readonly type = MetricActionTypes.CreateGroupSuccessful;

  constructor(public payload: MetricGroup) {}
}

export class CreateMetricGroupFailure implements Action {
  readonly type = MetricActionTypes.CreateGroupFailure;

  constructor(public payload: any) {}
}

export class SelectMetricGroup implements Action {
  readonly type = MetricActionTypes.SelectGroup;

  constructor(public payload: any) {}
}

export class CreateMetric implements Action {
  readonly type = MetricActionTypes.CreateMetric;

  constructor(public payload: Metric) {}
}

export class CreateMetricSuccessful implements Action {
  readonly type = MetricActionTypes.CreateMetricSuccessful;

  constructor(public payload: Metric) {}
}

export class CreateMetricFailure implements Action {
  readonly type = MetricActionTypes.CreateMetricFailure;

  constructor(public payload: any) {}
}

export class SelectMetric implements Action {
  readonly type = MetricActionTypes.SelectMetric;

  constructor(public payload: string) {}
}

export class SelectNewMetric implements Action {
  readonly type = MetricActionTypes.SelectNewMetric;
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
  | LoadGroups
  | LoadGroupsSuccessful
  | LoadGroupsFailure
  | CreateMetricGroup
  | CreateMetricGroupSuccessful
  | CreateMetricGroupFailure
  | SelectMetricGroup
  | LoadMetrics
  | LoadMetricsSuccessful
  | LoadMetricsFailure
  | CreateMetric
  | CreateMetricSuccessful
  | CreateMetricFailure
  | SelectMetric
  | SelectNewMetric
  | LoadProviders
  | LoadProvidersSuccessful
  | LoadProvidersFailure;
