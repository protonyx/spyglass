import {
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import * as fromLayout from '../core/reducers/layout.reducer';
import * as fromMetrics from '../metrics/reducers';
import * as fromRouter from '@ngrx/router-store';
import {RouterStateUrl} from '../shared/utils';
import { environment } from '../../environments/environment';

export interface State {
  layout: fromLayout.State,
  metrics: fromMetrics.State,
  router: fromRouter.RouterReducerState<RouterStateUrl>
}

export const reducers: ActionReducerMap<State> = {
  layout: fromLayout.reducer,
  metrics: fromMetrics.reducer,
  router: fromRouter.routerReducer
};


export const metaReducers: MetaReducer<State>[] = !environment.production
  ? []
  : [];

/**
 * Layout Reducers
 */
export const getLayoutState = createFeatureSelector<fromLayout.State>('layout');

export const getShowSidenav = createSelector(
  getLayoutState,
  fromLayout.getShowSidenav
);
