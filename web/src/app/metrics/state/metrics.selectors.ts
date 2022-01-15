import { createSelector, createFeatureSelector } from '@ngrx/store';
import { MetricState, adapter } from './index';

export const metricsKey = 'metrics';

//export const selectMetric = (state: AppState) => state.metrics;
export const selectMetricState = createFeatureSelector<MetricState>(metricsKey);

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal
} = adapter.getSelectors(selectMetricState);
