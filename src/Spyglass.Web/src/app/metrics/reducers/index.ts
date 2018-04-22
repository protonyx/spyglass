import {MetricGroupsActionTypes, MetricsActionsUnion} from '../metricGroup.actions';
import {createEntityAdapter} from '@ngrx/entity'
import {MetricGroup} from '../models/metricGroup';

// const adapter = createEntityAdapter<MetricGroup>();

export interface State {
  loading: boolean,
  groups: MetricGroup[],
  selectedGroupId: string
}


export const initialState: State = { // adapter.getInitialState();
  loading: false,
  groups: [],
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
    case MetricGroupsActionTypes.LoadSuccessful:
      return {
        ...state,
        groups: action.payload,
        loading: false
      };
    case MetricGroupsActionTypes.LoadFailure:
      return {
        ...state,
        loading: false
      }
  }
}

export const getLoading = (state: State) => state.loading;

export const getGroups = (state: State) => state.groups;

export const getSelectedGroup = (state: State) => state.selectedGroupId;
