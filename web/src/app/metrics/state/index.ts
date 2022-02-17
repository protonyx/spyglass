import { Metric } from '../../models/metric.model';
import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';
import { Connection } from '../../models/connection.model';

export interface MetricState extends EntityState<Metric> {
  selectedId: string | null;
  newMetric: Metric | null;
}

export function selectId(a: any): string {
  return a.id;
}

export function sortByName(a: any, b: any): number {
  return a.name.localeCompare(b.name);
}

export const adapter: EntityAdapter<Metric> = createEntityAdapter<Metric>({
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
