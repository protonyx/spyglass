import {
  CreateMetricGroup,
  CreateMetricGroupSuccessful,
  LoadGroupsSuccessful,
  LoadMetricsSuccessful,
  MetricActionTypes,
  MetricsActionsUnion,
  SelectMetricGroup
} from '../actions/metrics.actions';
import {createEntityAdapter, EntityAdapter, EntityState} from '@ngrx/entity'
import {MetricGroup} from '../models/metricGroup';
import {createFeatureSelector, createSelector} from "@ngrx/store";
import {Metric} from "../models/metric";

export interface State {
  loading: boolean,
  groups: EntityState<MetricGroup>,
  selectedGroupId: string | null,
  metrics: EntityState<Metric>
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
  loading: false,
  groups: groupAdapter.getInitialState(),
  selectedGroupId: null,
  metrics: metricAdapter.getInitialState()
};

export function reducer(
  state = initialState,
  action: MetricsActionsUnion
): State {
  switch (action.type) {
    case MetricActionTypes.LoadGroups:
      return {
        ...state,
        loading: true
      };
    case MetricActionTypes.LoadGroupsSuccessful:
      return {
        ...state,
        groups: groupAdapter.addMany((action as LoadGroupsSuccessful).payload, {...state.groups}),
        loading: false
      };
    case MetricActionTypes.LoadGroupsFailure:
      return {
        ...state,
        loading: false
      };
    case MetricActionTypes.LoadMetrics:
      return {
        ...state,
        loading: true
      };
    case MetricActionTypes.LoadMetricsSuccessful:
      return {
        ...state,
        metrics: metricAdapter.addMany((action as LoadMetricsSuccessful).payload, {...state.metrics}),
        loading: false
      };
    case MetricActionTypes.LoadMetricsFailure:
      return {
        ...state,
        loading: false
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

    default:
      return state;
  }
}

export const getMetricState = createFeatureSelector<State>('metrics');

export const getLoading = createSelector(
  getMetricState,
  (state: State) => state.loading
);

export const getGroupEntitiesState =
  createSelector(
    getMetricState,
    (state: State) => state.groups
  );

export const getSelectedGroupId = (state: State) => state.selectedGroupId;

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
)
