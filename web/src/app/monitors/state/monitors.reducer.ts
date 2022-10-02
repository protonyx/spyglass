import { createReducer, on } from '@ngrx/store';

import { retrievedMetricsList, selectMetric } from './monitors.actions';
import { MonitorState, adapter } from './index';

export const initialState: MonitorState = adapter.getInitialState({
  selectedId: null,
  newMonitor: null
});

export const monitorsReducer = createReducer(
  initialState,
  on(retrievedMetricsList, (state, { monitors }) => {
    return adapter.setAll(monitors, { ...state, selectedId: null });
  }),
  on(selectMetric, (state, { monitor }) => {
    return { ...state, selectedId: monitor.id };
  })
);
