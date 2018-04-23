import { Action } from '@ngrx/store';
import {MetricGroup} from '../models/metricGroup';

export enum MetricGroupsActionTypes {
  Load = '[Metric Groups Page] Load Groups',
  CreateGroup = '[Metric Group Page] Create Group',
  SelectGroup = '[Metric Group Page] Select Group',
  LoadSuccessful = '[Metric Group API] Load Successful',
  LoadFailure = '[Metric Group API] Load Failure'
}

export class LoadMetricGroups implements Action {
  readonly type = MetricGroupsActionTypes.Load;
}

export class SelectMetricGroup implements Action {
  readonly type = MetricGroupsActionTypes.SelectGroup;

  constructor(public payload: any) {}
}

export class CreateMetricGroup implements Action {
  readonly type = MetricGroupsActionTypes.CreateGroup;
}

export class LoadSuccessful implements Action {
  readonly type = MetricGroupsActionTypes.LoadSuccessful;

  constructor(public payload: MetricGroup[]) {}
}

export class LoadFailure implements Action {
  readonly type = MetricGroupsActionTypes.LoadFailure;

  constructor(public payload: any) {}
}

export type MetricsActionsUnion =
  | LoadMetricGroups
  | CreateMetricGroup
  | SelectMetricGroup
  | LoadSuccessful
  | LoadFailure;
