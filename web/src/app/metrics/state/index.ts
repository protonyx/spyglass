import { Metric } from '../../models/metric.model';
import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';

export interface MetricState extends EntityState<Metric> {
  selectedId: string | null;
  newMetric: Metric | null;
}

export function selectMetricId(a: Metric): string {
  return a.id;
}

export function sortByName(a: Metric, b: Metric): number {
  return a.name.localeCompare(b.name);
}

export const adapter: EntityAdapter<Metric> = createEntityAdapter<Metric>({
  selectId: selectMetricId,
  sortComparer: sortByName
});
