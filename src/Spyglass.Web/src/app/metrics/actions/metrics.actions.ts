import { Action } from '@ngrx/store';
import {Metric} from '../models/metric';
import {MetricGroup} from '../models/metricGroup';

export enum MetricActionTypes {
  LoadGroups = '[Metrics Page] Load Groups',
  LoadGroupsSuccessful = '[Metric Group API] Load Successful',
  LoadGroupsFailure = '[Metric Group API] Load Failure',
  LoadMetrics = '[Metrics Page] Load Metrics',
  LoadMetricsSuccessful = '[Metrics API] Load Successful',
  LoadMetricsFailure = '[Metrics API] Load Failure',
  CreateGroup = '[Metrics Page] Create Group',
  SelectGroup = '[Metrics Page] Select Group',
  CreateMetric = '[Metrics Page] Create Metric',
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
}

export class SelectMetricGroup implements Action {
  readonly type = MetricActionTypes.SelectGroup;

  constructor(public payload: any) {}
}

export class CreateMetric implements Action {
  readonly type = MetricActionTypes.CreateMetric;

  constructor(public payload: Metric) {}
}



export type MetricsActionsUnion =
  | LoadGroups
  | LoadGroupsSuccessful
  | LoadGroupsFailure
  | LoadMetrics
  | LoadMetricsSuccessful
  | LoadMetricsFailure
  | CreateMetricGroup
  | SelectMetricGroup
  | CreateMetric;
