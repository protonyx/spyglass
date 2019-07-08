import { Component, Input, OnInit } from '@angular/core';

import { Metric } from '../models/metric';

@Component({
  selector: 'sg-metric-list',
  template: `
    <table class="table">
      <thead>
        <tr>
          <th>Name</th>
          <th>Description</th>
          <th>Type</th>
        </tr>
      </thead>
      <tbody>
        <tr *ngFor="let metric of metrics">
          <td><a [routerLink]="['/metrics', metric.id]">{{ metric.name }}</a></td>
          <td>{{ metric.description }}</td>
          <td>{{ metric.providerType }}</td>
        </tr>
      </tbody>
    </table>
  `,
  styles: []
})
export class MetricListComponent implements OnInit {
  @Input() metrics: Metric[];

  constructor() {
  }

  ngOnInit() {
  }

}
