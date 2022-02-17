import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { environment } from '../../environments/environment';
import * as fromMetrics from '../metrics/state/metrics.reducer';
import * as fromConnections from '../metrics/state/connections.reducer';
import { AppState } from '../state/app.state';

export const reducers: ActionReducerMap<AppState> = {
  metrics: fromMetrics.metricsReducer,
  connections: fromConnections.connectionsReducer
};

export const metaReducers: MetaReducer<AppState>[] = !environment.production
  ? []
  : [];
