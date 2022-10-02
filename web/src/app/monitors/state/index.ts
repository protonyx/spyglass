import { Monitor } from '../../models/monitor.model';
import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';
import { Connection } from '../../models/connection.model';

export interface MonitorState extends EntityState<Monitor> {
  selectedId: string | null;
  newMonitor: Monitor | null;
}

export function selectId(a: any): string {
  return a.id;
}

export function sortByName(a: any, b: any): number {
  return a.name.localeCompare(b.name);
}

export const adapter: EntityAdapter<Monitor> = createEntityAdapter<Monitor>({
  selectId: selectId,
  sortComparer: sortByName
});

export interface ConnectionsState extends EntityState<Connection> {}

export const connectionAdapter: EntityAdapter<Connection> = createEntityAdapter<
  Connection
>({
  selectId: selectId,
  sortComparer: sortByName
});
