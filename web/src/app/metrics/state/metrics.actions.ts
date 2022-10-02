import { createAction, props } from '@ngrx/store';

import { Metric } from '../../models/metric.model';

export const loadMetrics = createAction('[Metrics Page] Load Metrics');

export const retrievedMetricsList = createAction(
  '[Metrics API] Retrieved Metrics Success',
  props<{ metrics: Array<Metric> }>()
);

export const selectMetric = createAction(
  '[Metrics Page] Select Metric',
  props<{ metric: Metric }>()
);

export enum MetricActionTypes {
  LoadMetricsFailure = '[Metrics API] Load Failure',
  SaveMetric = '[Metrics Page] Save Metric',
  SaveMetricSuccessful = '[Metrics API] Save Metric Successful',
  SaveMetricFailure = '[Metrics API] Save Metric Failure',
  SelectMetric = '[Metric Details Page] Select Metric',
  SelectNewMetric = '[New Metric Page] New Metric',
  DeleteMetric = '[Metrics Page] Delete Metric',
  DeleteMetricSuccessful = '[Metrics API] Delete Metric Success',
  DeleteMetricFailure = '[Metrics API] Delete Metric Failure'
}
