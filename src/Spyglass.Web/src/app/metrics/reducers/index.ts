import {
  DeleteMetricSuccessful,
  LoadMetricsSuccessful,
  LoadProvidersSuccessful,
  MetricActionTypes,
  MetricsActionsUnion, SaveMetricSuccessful,
  SelectMetric
} from '../actions/metrics.actions';
import {createEntityAdapter, EntityAdapter, EntityState} from '@ngrx/entity'
import {createFeatureSelector, createSelector} from '@ngrx/store';
import {Metric} from '../models/metric';
import {MetricProvider} from '../models/metricProvider';

export interface State {
  newMetric: Metric | null,
  metricsLoading: boolean,
  metrics: EntityState<Metric>,
  selectedMetricId: string | null,
  providersLoading: boolean,
  providers: MetricProvider[]
}

export const metricAdapter: EntityAdapter<Metric> = createEntityAdapter<Metric>({
  selectId: (group: Metric) => group.id,
  sortComparer: false,
});

export const initialState: State = {
  newMetric: null,
  metricsLoading: false,
  metrics: metricAdapter.getInitialState(),
  selectedMetricId: null,
  providersLoading: false,
  providers: []
};

export function reducer(
  state = initialState,
  action: MetricsActionsUnion
): State {
  switch (action.type) {
    case MetricActionTypes.LoadMetrics:
      return {
        ...state,
        metricsLoading: true
      };
    case MetricActionTypes.LoadMetricsSuccessful:
      return {
        ...state,
        metrics: metricAdapter.addMany((action as LoadMetricsSuccessful).payload, {...state.metrics}),
        metricsLoading: false
      };
    case MetricActionTypes.LoadMetricsFailure:
      return {
        ...state,
        metricsLoading: false
      };
    case MetricActionTypes.SelectMetric:
      return {
        ...state,
        selectedMetricId: (action as SelectMetric).payload
      };
    case MetricActionTypes.SelectNewMetric:
      return {
        ...state,
        newMetric: new Metric()
      };
    case MetricActionTypes.SaveMetricSuccessful:
      return {
        ...state,
        metrics: metricAdapter.upsertOne((action as SaveMetricSuccessful).payload, {...state.metrics})
      }
    case MetricActionTypes.DeleteMetricSuccessful:
      return {
        ...state,
        metrics: metricAdapter.removeOne((action as DeleteMetricSuccessful).id, {...state.metrics})
      };
    case MetricActionTypes.LoadProviders:
      return {
        ...state,
        providersLoading: true
      };
    case MetricActionTypes.LoadProvidersSuccessful:
      return {
        ...state,
        providersLoading: false,
        providers: (action as LoadProvidersSuccessful).payload
      };
    case MetricActionTypes.LoadProvidersFailure:
      return {
        ...state,
        providersLoading: false,
        providers: []
      };

    default:
      return state;
  }
}

export const getMetricState = createFeatureSelector<State>('metrics');

export const getMetricsLoading = createSelector(
  getMetricState,
  (state: State) => state.metricsLoading
);

export const getMetricEntitiesState = createSelector(
    getMetricState,
    (state: State) => state.metrics
  );

export const getNewMetric = createSelector(
  getMetricState,
  (state: State) => state.newMetric
);

export const {
  selectIds: getMetricIds,
  selectEntities: getMetricEntities,
  selectAll: getAllMetrics,
  selectTotal: getTotalMetrics
} = metricAdapter.getSelectors(getMetricEntitiesState);

export const getSelectedMetricId =
  createSelector(
    getMetricState,
    (state: State) => state.selectedMetricId
  );

export const getSelectedMetric = createSelector(
  getMetricEntities,
  getSelectedMetricId,
  getNewMetric,
  (entities, selectedId, newMetric) => {
    return selectedId ? entities[selectedId] : newMetric;
  }
);

export const getProvidersLoading = createSelector(
  getMetricState,
  (state: State) => state.providersLoading
);

export const getProviders = createSelector(
  getMetricState,
  (state: State) => state.providers
);
