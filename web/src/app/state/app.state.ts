import { ConnectionsState, MetricState } from '../metrics/state';

export interface AppState {
  metrics: MetricState;
  connections: ConnectionsState;
}
