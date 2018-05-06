import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Metric} from '../models/metric';

@Component({
  selector: 'sg-metric-list',
  template: `
    <mat-nav-list>
      <a mat-list-item *ngFor="let metric of metrics"
         [routerLink]="['/metrics', metric.id]">
        <h3 matLine>{{ metric.name }}</h3>
        <p matLine>{{ metric.description }}</p>
        <mat-divider inset></mat-divider>
      </a>
      <a mat-list-item (click)="createMetric.emit()">
        <mat-icon matLine>add</mat-icon>
        <h3 matLine>Create Metric</h3>
      </a>
    </mat-nav-list>
  `,
  styles: []
})
export class MetricListComponent implements OnInit {
  @Input() metrics: Metric[];
  @Output() createMetric: EventEmitter<any>;

  constructor() {
    this.createMetric = new EventEmitter<any>();
  }

  ngOnInit() {
  }

}
