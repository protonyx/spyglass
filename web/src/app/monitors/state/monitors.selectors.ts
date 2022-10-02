import { createSelector, createFeatureSelector } from '@ngrx/store';
import { MonitorState, adapter } from './index';

export const monitorsKey = 'monitors';

//export const selectMetric = (state: AppState) => state.monitors;
export const selectMetricState = createFeatureSelector<MonitorState>(monitorsKey);

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal
} = adapter.getSelectors(selectMetricState);
