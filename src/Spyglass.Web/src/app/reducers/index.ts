import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import * as fromMetrics from '../metrics/reducers';
import { environment } from '../../environments/environment';

export interface State {
  metrics: fromMetrics.State
}

export const reducers: ActionReducerMap<State> = {
  metrics: fromMetrics.reducer
};


export const metaReducers: MetaReducer<State>[] = !environment.production ? [] : [];

export const getMetricState = createFeatureSelector<fromMetrics.State>('metrics');

export const getLoading = createSelector(
  getMetricState,
  fromMetrics.getLoading
);

export const getGroups = createSelector(
  getMetricState,
  fromMetrics.getGroups
);
