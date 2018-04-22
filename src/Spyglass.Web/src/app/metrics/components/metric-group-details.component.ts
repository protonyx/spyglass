import {Component, Input, OnInit} from '@angular/core';
import {MetricGroup} from '../models/metricGroup';

@Component({
  selector: 'sg-metric-group-details',
  template: `
    <mat-card>
      <mat-card-title>{{ group.name }}</mat-card-title>
      <mat-card-subtitle>{{ group.description }}</mat-card-subtitle>
    </mat-card>
  `,
  styles: []
})
export class MetricGroupDetailsComponent implements OnInit {
  @Input() group: MetricGroup;

  constructor() { }

  ngOnInit() {
  }

}
