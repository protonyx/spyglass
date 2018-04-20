import { Metric } from './metric';

export class MetricGroup {
  public id: string;
  public name: string;

  public metrics: Metric[];
}
