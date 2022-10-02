import { createAction, props } from '@ngrx/store';

import { Monitor } from '../../models/monitor.model';

export const loadMonitors = createAction('[Monitors Page] Load Monitors');

export const retrievedMetricsList = createAction(
  '[Monitors API] Retrieved Monitors Success',
  props<{ monitors: Array<Monitor> }>()
);

export const selectMetric = createAction(
  '[Monitors Page] Select Monitor',
  props<{ monitor: Monitor }>()
);

export enum MonitorActionTypes {
  LoadMonitorsFailure = '[Monitors API] Load Failure',
  SaveMonitor = '[Monitors Page] Save Monitor',
  SaveMonitorSuccessful = '[Monitors API] Save Monitor Successful',
  SaveMonitorFailure = '[Monitors API] Save Monitor Failure',
  SelectMonitor = '[Monitor Details Page] Select Monitor',
  SelectNewMonitor = '[New Monitor Page] New Monitor',
  DeleteMonitor = '[Monitors Page] Delete Monitor',
  DeleteMonitorSuccessful = '[Monitors API] Delete Monitor Success',
  DeleteMonitorFailure = '[Monitors API] Delete Monitor Failure'
}
