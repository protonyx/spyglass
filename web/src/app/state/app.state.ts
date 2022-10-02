import { ConnectionsState, MonitorState } from '../monitors/state';

export interface AppState {
  monitors: MonitorState;
  connections: ConnectionsState;
}
