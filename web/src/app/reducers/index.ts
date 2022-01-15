import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { environment } from '../../environments/environment';
import * as fromMetrics from '../metrics/state/metrics.reducer';
import { MetricState } from '../metrics/state';
import { AppState } from '../state/app.state';

export const reducers: ActionReducerMap<AppState> = {
  metrics: fromMetrics.metricsReducer
};

export const metaReducers: MetaReducer<AppState>[] = !environment.production
  ? []
  : [];
