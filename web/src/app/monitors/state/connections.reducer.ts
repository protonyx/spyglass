import { createReducer, on } from '@ngrx/store';
import { retrievedConnectionList } from './connections.actions';
import { connectionAdapter, ConnectionsState } from './index';

export const connectionsInitialState: ConnectionsState = connectionAdapter.getInitialState();

export const connectionsReducer = createReducer(
  connectionsInitialState,
  on(retrievedConnectionList, (state, { connections }) => {
    return connectionAdapter.setAll(connections, { ...state });
  })
);
