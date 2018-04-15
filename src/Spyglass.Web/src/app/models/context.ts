import { Metric } from './metric';

export class Context {
  public id: string;
  public name: string;

  public metrics: Metric[];
}
