import {
  CreateMetricGroupSuccessful,
  LoadGroupsSuccessful,
  LoadMetricsSuccessful,
  LoadProvidersSuccessful,
  MetricActionTypes,
  MetricsActionsUnion,
  SelectMetric,
  SelectMetricGroup
} from '../actions/metrics.actions';
import {createEntityAdapter, EntityAdapter, EntityState} from '@ngrx/entity'
import {MetricGroup} from '../models/metricGroup';
import {createFeatureSelector, createSelector} from '@ngrx/store';
import {Metric} from '../models/metric';
import {MetricProvider} from '../models/metricProvider';

export interface State {
  groupsLoading: boolean,
  groups: EntityState<MetricGroup>,
  selectedGroupId: string | null,
  newMetric: Metric | null,
  metricsLoading: boolean,
  metrics: EntityState<Metric>,
  selectedMetricId: string | null,
  providers: MetricProvider[]
}

export const groupAdapter: EntityAdapter<MetricGroup> = createEntityAdapter<MetricGroup>({
  selectId: (group: MetricGroup) => group.id,
  sortComparer: false,
});

export const metricAdapter: EntityAdapter<Metric> = createEntityAdapter<Metric>({
  selectId: (group: Metric) => group.id,
  sortComparer: false,
});

export const initialState: State = {
  groupsLoading: false,
  groups: groupAdapter.getInitialState(),
  selectedGroupId: null,
  newMetric: null,
  metricsLoading: false,
  metrics: metricAdapter.getInitialState(),
  selectedMetricId: null,
  providers: []
};

export function reducer(
  state = initialState,
  action: MetricsActionsUnion
): State {
  switch (action.type) {
    case MetricActionTypes.LoadGroups:
      return {
        ...state,
        groupsLoading: true
      };
    case MetricActionTypes.LoadGroupsSuccessful:
      return {
        ...state,
        groups: groupAdapter.addMany((action as LoadGroupsSuccessful).payload, {...state.groups}),
        groupsLoading: false
      };
    case MetricActionTypes.LoadGroupsFailure:
      return {
        ...state,
        groupsLoading: false
      };
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
    case MetricActionTypes.CreateGroupSuccessful:
      return {
        ...state,
        groups: groupAdapter.addOne((action as CreateMetricGroupSuccessful).payload, {...state.groups})
      };
    case MetricActionTypes.SelectGroup:
      return {
        ...state,
        selectedGroupId: (action as SelectMetricGroup).payload
      };
    case MetricActionTypes.CreateMetric:
      return state;
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
    case MetricActionTypes.LoadProvidersSuccessful:
      return {
        ...state,
        providers: (action as LoadProvidersSuccessful).payload
      };

    default:
      return state;
  }
}

export const getMetricState = createFeatureSelector<State>('metrics');

export const getGroupsLoading = createSelector(
  getMetricState,
  (state: State) => state.groupsLoading
);

export const getGroupEntitiesState =
  createSelector(
    getMetricState,
    (state: State) => state.groups
  );

export const getSelectedGroupId =
  createSelector(
    getMetricState,
    (state: State) => state.selectedGroupId
  );

export const {
  selectIds: getGroupIds,
  selectEntities: getGroupEntities,
  selectAll: getAllGroups,
  selectTotal: getTotalGroups
} = groupAdapter.getSelectors(getGroupEntitiesState);

export const getSelectedGroup = createSelector(
  getGroupEntities,
  getSelectedGroupId,
  (entities, selectedId) => {
    return selectedId && entities[selectedId]
  }
);

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

export const getProviders = createSelector(
  getMetricState,
  (state: State) => state.providers
);
