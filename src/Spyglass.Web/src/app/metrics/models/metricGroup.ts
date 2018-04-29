import { Metric } from './metric';

export class MetricGroup {
  public id: string;
  public name: string;
  public description: string;

  public metrics: Metric[];
}
