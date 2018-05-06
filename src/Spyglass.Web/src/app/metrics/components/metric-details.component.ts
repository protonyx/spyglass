import {Component, Input, OnInit} from '@angular/core';
import {Metric} from '../models/metric';

@Component({
  selector: 'sg-metric-details',
  template: `
    <mat-card>
      <mat-card-title>{{ metric.name }}</mat-card-title>
      <mat-card-subtitle>{{ metric.description }}</mat-card-subtitle>
      <mat-list>
        <mat-list-item>
          <h3 matLine>{{ metric.providerType }}</h3>
          <p matLine>Metric Type</p>
        </mat-list-item>
      </mat-list>
    </mat-card>
  `,
  styles: []
})
export class MetricDetailsComponent implements OnInit {
  @Input() metric: Metric;

  constructor() { }

  ngOnInit() {
  }

}
