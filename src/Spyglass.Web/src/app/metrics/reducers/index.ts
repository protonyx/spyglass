import {LoadSuccessful, MetricGroupsActionTypes, MetricsActionsUnion, SelectMetricGroup} from '../actions/metricGroup.actions';
import {createEntityAdapter, EntityAdapter, EntityState} from '@ngrx/entity'
import {MetricGroup} from '../models/metricGroup';
import {createFeatureSelector, createSelector} from "@ngrx/store";

export interface State {
  loading: boolean,
  groups: EntityState<MetricGroup>,
  selectedGroupId: string | null
}

export const groupAdapter: EntityAdapter<MetricGroup> = createEntityAdapter<MetricGroup>({
  selectId: (group: MetricGroup) => group.id,
  sortComparer: false,
});

export const initialState: State = {
  loading: false,
  groups: groupAdapter.getInitialState(),
  selectedGroupId: null
};

export function reducer(
  state = initialState,
  action: MetricsActionsUnion
): State {
  switch (action.type) {
    case MetricGroupsActionTypes.Load:
      return {
        ...state,
        loading: true
      };
    case MetricGroupsActionTypes.CreateGroup:
      return state;
    case MetricGroupsActionTypes.SelectGroup:
      return {
        ...state,
        selectedGroupId: (action as SelectMetricGroup).payload
      };
    case MetricGroupsActionTypes.LoadSuccessful:
      return {
        ...state,
        groups: groupAdapter.addMany((action as LoadSuccessful).payload, {...state.groups}),
        loading: false
      };
    case MetricGroupsActionTypes.LoadFailure:
      return {
        ...state,
        loading: false
      }
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
