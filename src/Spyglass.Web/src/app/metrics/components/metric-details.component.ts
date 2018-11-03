import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {Metric} from '../models/metric';
import {MetricProvider} from '../models/metricProvider';

@Component({
  selector: 'sg-metric-details',
  template: `
    <a [routerLink]="['/metrics']">
      <clr-icon shape="caret" dir="left"></clr-icon>
      Return to Metrics
    </a>
    <div class="card">
      <div class="card-header">
        {{ metric.name }}
      </div>
      <div class="card-block">
        <div class="card-title">
          <button type="button"
                  class="btn btn-outline"
                  (click)="handleEdit()">
            <clr-icon shape="pencil"></clr-icon>
            Edit Metric
          </button>
          {{ metric.description }}
        </div>
        <div class="card-text">
          <h3>{{ metric.providerType }}</h3>
          <table class="table">
            <thead>
              <tr>
                <th class="left">Property</th>
                <th class="left">Value</th>
              </tr>
            </thead>
            <tbody>
              <tr *ngFor="let prop of currentProvider.properties">
                <td class="left">{{ prop.propertyName }}</td>
                <td class="left">{{ metric.provider[prop.propertyName] }}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  `,
  styles: []
})
export class MetricDetailsComponent implements OnInit, OnChanges {
  @Input() metric: Metric;
  @Input() providers: MetricProvider[];
  @Output() edit: EventEmitter = new EventEmitter();

  currentProvider: MetricProvider;

  constructor() { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['metric'] || changes['providers']) {
      this.currentProvider = this.providers.find(t => t.name === this.metric.providerType);
    }
  }

  handleEdit() {
    this.edit.emit();
  }

}
