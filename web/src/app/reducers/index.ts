import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { environment } from '../../environments/environment';
import * as fromMetrics from '../monitors/state/monitors.reducer';
import * as fromConnections from '../monitors/state/connections.reducer';
import { AppState } from '../state/app.state';

export const reducers: ActionReducerMap<AppState> = {
  monitors: fromMetrics.monitorsReducer,
  connections: fromConnections.connectionsReducer
};

export const metaReducers: MetaReducer<AppState>[] = !environment.production
  ? []
  : [];
