import { connectionAdapter, ConnectionsState } from './index';
import { createFeatureSelector } from '@ngrx/store';

export const connectionsKey = 'connections';

export const selectConnectionsState = createFeatureSelector<ConnectionsState>(
  connectionsKey
);

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal
} = connectionAdapter.getSelectors(selectConnectionsState);
