import { createReducer, on } from '@ngrx/store';

import { retrievedMetricsList, selectMetric } from './metrics.actions';
import { MetricState, adapter } from './index';

export const initialState: MetricState = adapter.getInitialState({
  selectedId: null,
  newMetric: null
});

export const metricsReducer = createReducer(
  initialState,
  on(retrievedMetricsList, (state, { metrics }) => {
    return adapter.setAll(metrics, { ...state, selectedId: null });
  }),
  on(selectMetric, (state, { metric }) => {
    return { ...state, selectedId: metric.id };
  })
);
