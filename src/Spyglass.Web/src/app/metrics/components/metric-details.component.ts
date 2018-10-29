import {Component, Input, OnInit} from '@angular/core';
import {Metric} from '../models/metric';

@Component({
  selector: 'sg-metric-details',
  template: `
    <div class="card">
      <div class="card-header">
        {{ metric.name }}
      </div>
      <div class="card-block">
        <div class="card-title">
          {{ metric.description }}
        </div>
        <div class="card-text">
          <h3>{{ metric.providerType }}</h3>
          <p>Metric Type</p>
        </div>
      </div>
    </div>
  `,
  styles: []
})
export class MetricDetailsComponent implements OnInit {
  @Input() metric: Metric;

  constructor() { }

  ngOnInit() {
  }

}
